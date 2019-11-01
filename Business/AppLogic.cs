using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataModels;
using DAL;

namespace Business
{
    public class AppLogic
    {
        readonly DataBaseCallsApp _dataBaseCallsApp = new DataBaseCallsApp();

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

        private List<ActionModel> GetAllActionsFromCertificate(string certificatename, string companyname)
        {
            List<ActionModel> result = new List<ActionModel>();

            DataTable allActionIDs = _dataBaseCallsApp.GetAllActionsFromCertificate(certificatename, companyname);

            foreach (DataRow dr in allActionIDs.Rows )
            {
                DataTable action = _dataBaseCallsApp.GetAllFromAction(Convert.ToInt32(dr[0].ToString()));
                foreach (DataRow dataRow in action.Rows)
                {
                    ActionModel actionModel = new ActionModel()
                    {
                        Name = dataRow[1].ToString(),
                        Description = dataRow[2].ToString(),
                        BeginDateTime = Convert.ToDateTime(dataRow[3]),
                        CreatedOn = Convert.ToDateTime(dataRow[4]),
                        CreatedByEmail = dataRow[5].ToString(),
                        Occurence = (OccurenceEnum)Enum.Parse(typeof(OccurenceEnum), dataRow[7].ToString(), true),
                        ResponsibleUserEmail = dataRow[8].ToString(),
                        EnableNotifications = Convert.ToBoolean(dataRow[9].ToString()),
                        Comments = null,
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

        public void CreateActionInDatabase(ActionModel actionModel, string companyName, string certificateName)
        {
            _dataBaseCallsApp.SaveAction(actionModel, certificateName, companyName);
        }

        public void DeleteAction(string actionName, string certificateName, string companyName)
        {
            _dataBaseCallsApp.DeleteActionWithId(_dataBaseCallsApp.GetActionId(actionName, _dataBaseCallsApp.GetCertificateId(certificateName, companyName).GetValueOrDefault()).GetValueOrDefault());
        }

        public void DeleteCertificate(string certificateName, string companyName)
        {           
            DataTable test = _dataBaseCallsApp.GetAllActionsFromCertificate(certificateName, companyName);
            foreach (DataRow row in test.Rows)
            {
                _dataBaseCallsApp.DeleteActionWithId(Convert.ToInt32(row[0].ToString()));
            }
            _dataBaseCallsApp.DeleteCertificate(_dataBaseCallsApp.GetCertificateId(certificateName, companyName).GetValueOrDefault());
        }
    }
}
