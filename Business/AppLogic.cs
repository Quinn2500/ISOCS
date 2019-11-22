using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataModels;
using DAL;
using Microsoft.AspNetCore.Identity;

namespace Business
{
    public class AppLogic
    {
        private readonly DataBaseCallsApp _dataBaseCallsApp = new DataBaseCallsApp();
        private readonly DatabaseCallsNotifications _databaseCallsNotifications = new DatabaseCallsNotifications();
        private readonly EmailLogic _emailLogic = new EmailLogic();


        #region Certificate
        public CertificateModel GetCertificateModel(string certificatename, string companyname)
        {
            CertificateModel result = null;
            foreach (DataRow dr in _dataBaseCallsApp.GetAllFromCertificate(certificatename, companyname).Rows)
            {
                result = new CertificateModel()
                {
                    Actions = GetAllActionsFromCertificate(certificatename, companyname),
                    Description = dr[2].ToString(),
                    Name = dr[1].ToString(),
                    CreatedOn = Convert.ToDateTime(dr[3]),
                    CreatedByEmail = dr[4].ToString(),
                    CompanyName = dr[5].ToString()
                };
            }

            return result;
        }

        public List<string> GetAllCertificateNamesFromCompany(string companyname)
        {
            List<string> result = new List<string>();
            DataTable request = _dataBaseCallsApp.GetAllCertificatesFromCompany(companyname);

            foreach (DataRow dr in request.Rows)
            {
                result.Add(dr[1].ToString());
            }

            return result;
        }

        private List<ActionModel> GetAllActionsFromCertificate(string certificatename, string companyname)
        {
            List<ActionModel> result = new List<ActionModel>();

            DataTable allActionIDs = _dataBaseCallsApp.GetAllActionsFromCertificate(certificatename, companyname);

            foreach (DataRow dr in allActionIDs.Rows)
            {
                DataTable action = _dataBaseCallsApp.GetAllFromAction(Convert.ToInt32(dr[0].ToString()));
                foreach (DataRow dataRow in action.Rows)
                {
                    ActionModel actionModel = new ActionModel()
                    {
                        Name = dataRow[1].ToString(),
                        Description = dataRow[2].ToString(),
                        StartDate = Convert.ToDateTime(dataRow[3]),
                        CreatedOn = Convert.ToDateTime(dataRow[4]),
                        CreatedByEmail = dataRow[5].ToString(),
                        Occurence = (OccurenceEnum)Enum.Parse(typeof(OccurenceEnum), dataRow[7].ToString(), true),
                        ResponsibleUserEmail = dataRow[8].ToString(),
                        EnableNotifications = Convert.ToBoolean(dataRow[9].ToString()),
                        CertificateName = certificatename
                    };
                    result.Add(actionModel);
                }
            }

            return result;
        }

        public void CreateCertificateinDatabase(CertificateModel certificateModel)
        {
            _dataBaseCallsApp.SaveCertificate(certificateModel);
        }

        public void DeleteCertificate(string certificateName, string companyName)
        {
            DataTable test = _dataBaseCallsApp.GetAllActionsFromCertificate(certificateName, companyName);
            foreach (DataRow row in test.Rows)
            {
                DeleteAction(Convert.ToInt32(row[0].ToString()));
            }
            _dataBaseCallsApp.DeleteCertificate(_dataBaseCallsApp.GetCertificateId(certificateName, companyName).GetValueOrDefault());
        }
        #endregion

        #region Action
        public void CreateActionInDatabase(ActionModel actionModel, string certificateName, string companyName)
        {
            _dataBaseCallsApp.SaveAction(actionModel, certificateName, companyName);
            int id = _dataBaseCallsApp.SaveActionToComplete(GetActionId(actionModel.Name, companyName, certificateName), actionModel.StartDate).GetValueOrDefault();
            _databaseCallsNotifications.CreateActionHistoryToken(id, Guid.NewGuid().ToString());
        }

        public ActionToComplete GetActionToComplete(string actionName, string certificateName, string companyName)
        {
            DataTable dataTable = _dataBaseCallsApp.GetAllFromActionToExecute(GetActionId(actionName, companyName, certificateName));
            ActionToComplete result = new ActionToComplete
            {
                Action = GetAction(actionName, certificateName, companyName),
                DateToExecute = Convert.ToDateTime(dataTable.Rows[0][2]),
                Comments = GetAllComments(Convert.ToInt32(dataTable.Rows[0][0]))
            };

            return result;
        }

        public ActionToComplete GetActionToComplete(int actionid)
        {
            DataTable dataTable = _dataBaseCallsApp.GetAllFromActionToExecute(actionid);
            ActionToComplete result = new ActionToComplete
            {
                Action = GetAction(actionid),
                DateToExecute = Convert.ToDateTime(dataTable.Rows[0][2]),
                Comments = GetAllComments(Convert.ToInt32(dataTable.Rows[0][0]))
            };

            return result;
        }


        private List<CommentModel> GetAllComments(int actionHistoryId)
        {
            List<CommentModel> comments = new List<CommentModel>();

            foreach (DataRow row in _dataBaseCallsApp.GetAllComments(actionHistoryId).Rows)
            {
                CommentModel comment = new CommentModel
                {
                    Contents = row[1].ToString(),
                    CreateDateTime = Convert.ToDateTime(row[4]),
                    Owner = row[2].ToString()
                };
                comments.Add(comment);
            }

            return comments;
        }

        public void CompleteAction(bool executionSucces, string actioName, string certificateName, ApplicationUser userThatCompleted)
        {
            int actionId = GetActionId(actioName, userThatCompleted.CompanyName, certificateName);
            ActionToComplete action = GetActionToComplete(actioName, certificateName, userThatCompleted.CompanyName);
            int actionHistoryId = _dataBaseCallsApp.GetRecentActionHistoryId(actionId);
            _dataBaseCallsApp.ExecuteAction(actionHistoryId, executionSucces, userThatCompleted.Email, DateTime.Now);
            _databaseCallsNotifications.DeleteActionHistoryToken(actionHistoryId);
            DateTime newDateToExecute;
            switch (action.Action.Occurence)
            {
                case OccurenceEnum.Daily:
                    newDateToExecute = action.DateToExecute.AddDays(1);
                    break;
                case OccurenceEnum.Weekly:
                    newDateToExecute = action.DateToExecute.AddDays(7);
                    break;
                case OccurenceEnum.Monthly:
                    newDateToExecute = action.DateToExecute.AddMonths(1);
                    break;
                case OccurenceEnum.Quarterly:
                    newDateToExecute = action.DateToExecute.AddMonths(3);
                    break;
                case OccurenceEnum.Yearly:
                    newDateToExecute = action.DateToExecute.AddYears(1);
                    break;
                default:
                    newDateToExecute = DateTime.MinValue;
                    break;
            }

            if (!CheckHistoryCount(actionId))
            {
                _dataBaseCallsApp.DeleteHistoryAction(_dataBaseCallsApp.GetOldestHistoryEntry(actionId));
            }

            int id = _dataBaseCallsApp.SaveActionToComplete(actionId, newDateToExecute).GetValueOrDefault();
            _databaseCallsNotifications.CreateActionHistoryToken(id, Guid.NewGuid().ToString());
        }

        private bool CheckHistoryCount(int actionId)
        {
            int maxCount = 10;
            return _dataBaseCallsApp.GetAllHistoryActionsId(actionId).Rows.Count <= maxCount;
        }

        public void DeleteAction(string actionName, string certificateName, string companyName)
        {
            int actionid = GetActionId(actionName, companyName, certificateName);
            DeleteAction(actionid);
        }

        public void DeleteAction(int id)
        {
            foreach (DataRow row in _dataBaseCallsApp.GetAllHistoryActionsId(id).Rows)
            {
                _dataBaseCallsApp.DeleteAllCommentsFromHistoryAction(Convert.ToInt32(row[0].ToString()));
                _databaseCallsNotifications.DeleteActionHistoryToken(Convert.ToInt32(row[0].ToString()));
            }
            _dataBaseCallsApp.DeleteActionWithId(id);
            _dataBaseCallsApp.DeleteAllActionHistory(id);

        }

        public ActionModel GetAction(string actionName, string certificateName, string companyName)
        {
            ActionModel result = new ActionModel();
            DataTable actionDataTable = _dataBaseCallsApp.GetAllFromAction(_dataBaseCallsApp.GetActionId(actionName,
                    _dataBaseCallsApp.GetCertificateId(certificateName, companyName).GetValueOrDefault())
                .GetValueOrDefault());

            foreach (DataRow dataRow in actionDataTable.Rows)
            {
                ActionModel actionModel = new ActionModel()
                {
                    Name = dataRow[1].ToString(),
                    Description = dataRow[2].ToString(),
                    StartDate = Convert.ToDateTime(dataRow[3]),
                    CreatedOn = Convert.ToDateTime(dataRow[4]),
                    CreatedByEmail = dataRow[5].ToString(),
                    Occurence = (OccurenceEnum)Enum.Parse(typeof(OccurenceEnum), dataRow[7].ToString(), true),
                    ResponsibleUserEmail = dataRow[8].ToString(),
                    EnableNotifications = Convert.ToBoolean(dataRow[9].ToString()),
                    CertificateName = certificateName
                };
                result = actionModel;
            }

            return result;
        }

        public ActionModel GetAction(int actionId)
        {
            ActionModel result = new ActionModel();
            DataTable actionDataTable = _dataBaseCallsApp.GetAllFromAction(actionId);

            foreach (DataRow dataRow in actionDataTable.Rows)
            {
                ActionModel actionModel = new ActionModel()
                {
                    Name = dataRow[1].ToString(),
                    Description = dataRow[2].ToString(),
                    StartDate = Convert.ToDateTime(dataRow[3]),
                    CreatedOn = Convert.ToDateTime(dataRow[4]),
                    CreatedByEmail = dataRow[5].ToString(),
                    CertificateName = _dataBaseCallsApp.GetCertificateName(Convert.ToInt32(dataRow[6])),
                    Occurence = (OccurenceEnum)Enum.Parse(typeof(OccurenceEnum), dataRow[7].ToString(), true),
                    ResponsibleUserEmail = dataRow[8].ToString(),
                    EnableNotifications = Convert.ToBoolean(dataRow[9].ToString())                   
                };
                result = actionModel;
            }

            return result;
        }


        public void PostComment(CommentModel comment, string actionName, string certificateName, string companyName)
        {
            int actionHistoryId =
                _dataBaseCallsApp.GetRecentActionHistoryId(GetActionId(actionName, companyName,
                    certificateName));
            _dataBaseCallsApp.PostComment(comment.Contents, comment.CreateDateTime, comment.Owner, actionHistoryId);
        }

        private int GetActionId(string actionName, string companyName, string certificateName)
        {
            return _dataBaseCallsApp.GetActionId(actionName,
                _dataBaseCallsApp.GetCertificateId(certificateName, companyName).GetValueOrDefault()).GetValueOrDefault();

        }

        public List<CompletedAction> GetActionHistory(string actionName, string companyName, string certificateName)
        {
            List<CompletedAction> result = new List<CompletedAction>();           
            foreach (DataRow dataRow in _dataBaseCallsApp.GetAllHistoryActionsId(GetActionId(actionName, companyName, certificateName)).Rows)
            {
                DataRow row = _dataBaseCallsApp.GetAllFromHistoryAction(Convert.ToInt32(dataRow[0])).Rows[0];
                if (row[3] != DBNull.Value)
                {
                    CompletedAction completedAction = new CompletedAction
                    {
                        ActionHistoryId = Convert.ToInt32(dataRow[0]),
                        Action = GetAction(actionName, companyName, certificateName),
                        Comments = GetAllComments(Convert.ToInt32(dataRow[0])),
                        CompletedByEmail = row[4].ToString(),
                        CompletedOn = Convert.ToDateTime(row[3]),
                        ExecutionSucces = Convert.ToBoolean(row[5]),
                        DateToExecute = Convert.ToDateTime(row[2])
                    };
                    result.Add(completedAction);
                }
            }

            return result;
        }

        public CompletedAction GetCompletedAction(int actionHistoryId)
        {
            DataRow row = _dataBaseCallsApp.GetAllFromHistoryAction(actionHistoryId).Rows[0];
                CompletedAction completedAction = new CompletedAction
                {
                    ActionHistoryId = Convert.ToInt32(row[0]),
                    Action = GetAction(Convert.ToInt32(row[1])),
                    Comments = GetAllComments(actionHistoryId),
                    CompletedByEmail = row[4].ToString(),
                    CompletedOn = Convert.ToDateTime(row[3]),
                    ExecutionSucces = Convert.ToBoolean(row[5]),
                    DateToExecute = Convert.ToDateTime(row[2])
                };                
            return completedAction;
        }

        public List<ActionToComplete> GetFiveComingActionsForUser(string userMailAdress)
        {
            List<ActionToComplete> result = new List<ActionToComplete>();
            foreach (DataRow row in _dataBaseCallsApp.Get5ComingActionIdsForUser(userMailAdress).Rows)
            {
                result.Add(GetActionToComplete(Convert.ToInt32(row[0])));
            }
            return result;
        }

        #endregion
    }
}
