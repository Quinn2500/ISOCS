using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataModels;
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

        public DataTable GetAllActionsFromCertificate(string certificatename)
        {
            string query = "SELECT * FROM `action` WHERE action.certificateName = @p1 ";
            List<MySqlParameter> parameters = new List<MySqlParameter> {new MySqlParameter("@p1", certificatename)};
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
            string query = "SELECT action.*, usertoaction.userName, usertoaction.enableNotifications FROM `action` INNER JOIN usertoaction on usertoaction.actionID = action.ID WHERE action.ID = @p1";
            List<MySqlParameter> parameters = new List<MySqlParameter> { new MySqlParameter("@p1", actionid) };
            return _databaseCalls.Select(query, parameters);
        }

        public DataTable GetAllCommentsOnAction(int actionid)
        {
            string query = "SELECT * FROM `comments` WHERE comments.actionID = @p1";
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

            string queryAction = "INSERT INTO `action` (`name`, `description`, `startOn`, `createdOn`, `createdBy`, `certificateID`, `Occurence`) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7);";
            List<MySqlParameter> parametersAction = new List<MySqlParameter>
            {
                new MySqlParameter("@p1", action.Name),
                new MySqlParameter("@p2", action.Description),
                new MySqlParameter("@p3", action.BeginDateTime.Date),
                new MySqlParameter("@p4", action.CreatedOn),
                new MySqlParameter("@p5", action.CreatedByEmail),
                new MySqlParameter("@p6", certificateId),
                new MySqlParameter("@p7", action.Occurence.ToString()),
            };
            int? actionId = _databaseCalls.CommandWithLastId(queryAction, parametersAction);

            string query = "INSERT INTO `usertoaction` (`userName`, `actionID`, `enableNotifications`) VALUES (@pUID, @pCerfID, @pEnableNotifications);";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pUID", action.ResponsibleUserEmail),
                new MySqlParameter("@pCerfID", actionId),
                new MySqlParameter("@PEnableNotifications", action.EnableNotifications),
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
    }
}
