using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataModels;
using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DataBaseCallsApp
    {
        private readonly DatabaseCalls _databaseCalls = new DatabaseCalls();

        public DataTable GetAllCertificatesFromCompany(string companyname)
        {
            string query = "SELECT * FROM `certificate` WHERE certificate.companyName = @p1 ";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@p1", companyname)
            };
            return _databaseCalls.Select(query, parameters);
        }

        public void SaveCertificate(CertificateModel certificate)
        {
            string queryCertificate = "INSERT INTO `certificate` (`name`, `description`, `createdOn`, `createdBy`, `companyName`) VALUES (@pName, @pDesc, @pDate, @pUID, @pCname);";
            List<MySqlParameter> parametersCertificate = new List<MySqlParameter>
            {
                new MySqlParameter("@pName", certificate.Name),
                new MySqlParameter("@pDesc", certificate.Description),
                new MySqlParameter("@pDate", certificate.CreatedOn),
                new MySqlParameter("@pUID", certificate.CreatedByEmail),
                new MySqlParameter("@pCname", certificate.CompanyName)
            };
            int? certificateId = _databaseCalls.CommandWithLastId(queryCertificate, parametersCertificate);

            string query = "INSERT INTO `usertocertificate` (`userName`, `certificateID`, `enableNotifications`) VALUES (@pUID, @pCerfID, @pEnableNotifications);";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pUID", certificate.ResponsibleUserEmail),
                new MySqlParameter("@pCerfID", certificateId),
                new MySqlParameter("@PEnableNotifications", certificate.EnableNotifications),
            };
            _databaseCalls.Command(query, parameters);
        }

        public DataTable GetAllFromAction(int actionid)
        {
            string query = "SELECT action.*, usertoaction.userName FROM `action` INNER JOIN usertoaction on usertoaction.actionID = action.ID WHERE action.ID = @p1";
            List<MySqlParameter> parameters = new List<MySqlParameter> { new MySqlParameter("@p1", actionid) };
            return _databaseCalls.Select(query, parameters);
        }

        public DataTable GetAllActionsFromCertificate(string certificateName, string companyName)
        {
            string query = "SELECT action.ID FROM `certificate` INNER JOIN action on certificate.ID = action.certificateID WHERE certificate.name = @p1 AND certificate.companyName = @p2";
            List<MySqlParameter> parameters = new List<MySqlParameter> { new MySqlParameter("@p1", certificateName), new MySqlParameter("@p2", companyName) };
            return _databaseCalls.Select(query, parameters);
            
        }

        public DataTable GetAllFromCertificate(string certificateName, string companyName)
        {
            string query = "SELECT * FROM `certificate` WHERE certificate.name = @p1 AND certificate.companyName = @p2";
            List<MySqlParameter> parameters = new List<MySqlParameter> { new MySqlParameter("@p1", certificateName), new MySqlParameter("@p2", companyName) };
            return _databaseCalls.Select(query, parameters);
        }

        public void SaveAction(ActionModel action, string certificateName, string companyName)
        {
            int? certificateId = GetCertificateId(certificateName, companyName);

            string queryAction = "INSERT INTO `action`(`name`, `description`, `startOn`, `createdOn`, `createdBy`, `certificateID`, `Occurence`, `enableNotifications`, `enableComments`, `enableFileUpload`) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10);";
            List<MySqlParameter> parametersAction = new List<MySqlParameter>
            {
                new MySqlParameter("@p1", action.Name),
                new MySqlParameter("@p2", action.Description),
                new MySqlParameter("@p3", action.StartDate.Date),
                new MySqlParameter("@p4", action.CreatedOn),
                new MySqlParameter("@p5", action.CreatedByEmail),
                new MySqlParameter("@p6", certificateId),
                new MySqlParameter("@p7", action.Occurence.ToString()),
                new MySqlParameter("@p8", action.EnableNotifications),
                new MySqlParameter("@p9", action.EnableComments),
                new MySqlParameter("@p10", action.EnableFileUpload)
            };
            int? actionId = _databaseCalls.CommandWithLastId(queryAction, parametersAction);

            string query = "INSERT INTO `usertoaction` (`userName`, `actionID`) VALUES (@pUID, @pCerfID);";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pUID", action.ResponsibleUserEmail),
                new MySqlParameter("@pCerfID", actionId)
            };
            _databaseCalls.Command(query, parameters);


        }

        public int? GetCertificateId(string certificateName, string companyName)
        {
            string queryCertificateId = "SELECT certificate.ID FROM `certificate` WHERE certificate.name = @p1 AND certificate.companyName = @p2";
            List<MySqlParameter> parametersCertificateId = new List<MySqlParameter> { new MySqlParameter("@p1", certificateName), new MySqlParameter("@p2", companyName) };
            return _databaseCalls.GetOneInt(queryCertificateId, parametersCertificateId);
        }

        public int? GetActionId(string actionName, int certificateId)
        {
            string queryCertificateId = "SELECT action.ID FROM `action` WHERE action.name = @p1 AND action.certificateID = @p2";
            List<MySqlParameter> parametersCertificateId = new List<MySqlParameter> { new MySqlParameter("@p1", actionName), new MySqlParameter("@p2", certificateId) };
            return _databaseCalls.GetOneInt(queryCertificateId, parametersCertificateId);
        }

        public void DeleteActionWithId(int actionId)
        {
            string queryDeleteUserToAction = "DELETE FROM `usertoaction` WHERE usertoaction.actionID = @pActionId";
            List<MySqlParameter> parametersActionId = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionID", actionId),
            };
            _databaseCalls.Command(queryDeleteUserToAction, parametersActionId);

            string queryDeleteAction = "DELETE FROM `action` WHERE action.ID = @pActionId";
            _databaseCalls.Command(queryDeleteAction, parametersActionId);

        }

        public void DeleteAllActionHistory(int actionId)
        {
            string queryDeleteUserToAction = "DELETE FROM `actionhistory` WHERE actionhistory.actionID = @pActionId";
            List<MySqlParameter> parametersActionId = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionID", actionId),
            };
            _databaseCalls.Command(queryDeleteUserToAction, parametersActionId);
        }

        public void DeleteCertificate(int certificateId)
        {
            string queryResUser = "DELETE FROM `usertocertificate` WHERE usertocertificate.certificateID = @pCerId";
            List<MySqlParameter> parametersCer = new List<MySqlParameter>
            {
                new MySqlParameter("@pCerId", certificateId)
            };
            _databaseCalls.Command(queryResUser, parametersCer);

            string queryCertificate = "DELETE FROM `certificate` WHERE certificate.ID = @pCerId";
            _databaseCalls.Command(queryCertificate, parametersCer);

        }

        public DataTable GetAllFromActionToExecute(int actionId)
        {
            string query = "SELECT * FROM `actionhistory` WHERE actionhistory.actionID = @pActionId AND actionhistory.executedOn IS NULL";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionId", actionId)
            };
            return _databaseCalls.Select(query, parameters);
        }

        public int? SaveActionToComplete(int actionId, DateTime dateToExecute)
        {
            string query = "INSERT INTO `actionhistory` (`actionID`, `dateToExecute`) VALUES (@pActionId , @pDateToExecute);";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionId", actionId),
                new MySqlParameter("@pDateToExecute", dateToExecute)
            };
            return _databaseCalls.CommandWithLastId(query, parameters);


        }

        public int GetRecentActionHistoryId(int actionId)
        {
            string query = "SELECT actionhistory.ID FROM `actionhistory` WHERE actionhistory.actionID = @pActionId AND actionhistory.executedOn IS NULL";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionId", actionId)
            };
            return _databaseCalls.GetOneInt(query, parameters).GetValueOrDefault();
        }

        public void ExecuteAction(int actionHistoryId, bool executionSucces, string userEmail, DateTime dateExecution)
        {
            string query =
                "UPDATE `actionhistory` SET `executedOn` = @pExecutedOn, `executedBy` = @pExecutedBy, `executionSucceeded` = @pExeSucces WHERE `actionhistory`.`ID` = @pActionHId;";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionHId", actionHistoryId),
                new MySqlParameter("@pExeSucces", executionSucces),
                new MySqlParameter("@pExecutedBy", userEmail),
                new MySqlParameter("@pExecutedOn", dateExecution)
            };
            _databaseCalls.Command(query, parameters);
        }

        public void PostComment(string commentContent, DateTime creationDate, string ownerEmail, int actionHistoryId)
        {
            string query = "INSERT INTO `comments` (`content`, `commentedBy`, `actionHistoryID`, `createdOn`) VALUES (@pContent, @pOwner, @pActHId, @pCreatedOn);";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pContent", commentContent),
                new MySqlParameter("@pOwner", ownerEmail),
                new MySqlParameter("@pActHId", actionHistoryId),
                new MySqlParameter("@pCreatedOn", creationDate)
            };
            _databaseCalls.Command(query, parameters);
        }

        public DataTable GetAllComments(int actionHistoryId)
        {
            string query = "SELECT * FROM `comments` WHERE comments.actionHistoryID = @pActionHId";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionHId", actionHistoryId)
            };
            return _databaseCalls.Select(query, parameters);
        }

        public void DeleteAllCommentsFromHistoryAction(int actionHistoryId)
        {
            string query = "DELETE FROM `comments` WHERE comments.actionHistoryID = @pActionHId";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionHId", actionHistoryId)
            };
            _databaseCalls.Command(query, parameters);
        }

        public DataTable GetAllHistoryActionsId(int actionId)
        {
            string query = "SELECT actionhistory.ID FROM `actionhistory` WHERE actionhistory.actionID = @pActionId";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionId", actionId)
            };
            return _databaseCalls.Select(query, parameters);
        }

        public int GetOldestHistoryEntry(int actionId)
        {
            string query = "SELECT actionhistory.ID FROM `actionhistory` WHERE actionhistory.executedOn = (SELECT MIN(actionhistory.executedOn) FROM actionhistory WHERE actionhistory.actionID = @pActionId) AND actionhistory.actionID = @pActionId";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionId", actionId)
            };
            return _databaseCalls.GetOneInt(query, parameters).GetValueOrDefault();
        }

        public void DeleteHistoryAction(int actionHistoryId)
        {
            string query = "DELETE FROM `actionhistory` WHERE actionhistory.ID = @pActionHId";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionHId", actionHistoryId)
            };
            _databaseCalls.Command(query, parameters);
        }

        public DataTable GetAllFromHistoryAction(int actionHistoryId)
        {
            string query = "SELECT * FROM `actionhistory` WHERE actionhistory.ID = @pActionHistoryId";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionHistoryId", actionHistoryId)
            };
            return _databaseCalls.Select(query, parameters);
        }

        public string GetCertificateName(int certificateId)
        {
            string query = "SELECT certificate.name FROM `certificate` WHERE certificate.ID = @pCertificateId";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pCertificateId", certificateId)
            };
            return _databaseCalls.GetOneString(query, parameters);
        }

        public DataTable Get5ComingActionIdsForUser(string userEmail)
        {
            string query =
                "SELECT actionhistory.actionID, actionhistory.dateToExecute FROM `usertoaction` INNER JOIN actionhistory on actionhistory.actionID = usertoaction.actionID WHERE usertoaction.userName = '@pUserEmail' AND actionhistory.executedOn IS NULL ORDER By actionhistory.dateToExecute ASC LIMIT 5";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pUserEmail", userEmail)
            };
            return _databaseCalls.Select(query, parameters);
        }
    }
}
