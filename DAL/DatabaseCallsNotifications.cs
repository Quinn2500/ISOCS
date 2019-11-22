using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DatabaseCallsNotifications
    {
        private readonly DatabaseCalls _databaseCalls = new DatabaseCalls();

        public DataTable GetUnexecutedActionsIds(DateTime dateToNotify)
        {
            string query = "SELECT actionhistory.ID FROM actionhistory WHERE actionhistory.dateToExecute = @pDateToNotify AND actionhistory.executedOn IS NULL ";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pDateToNotify", dateToNotify)
            };
            return _databaseCalls.Select(query, parameters);
        }

        public void CreateActionHistoryToken(int actionHistoryid, string token)
        {
            string query = "INSERT INTO `actiontokens` (`actionHistoryID`, `token`, `dateCreated`) VALUES (@pId, @pToken, @pDate);";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pId", actionHistoryid),
                new MySqlParameter("@pToken", token),
                new MySqlParameter("@pDate", DateTime.Today)
            };
            _databaseCalls.Command(query, parameters);
        }

        public string GetActionHistoryToken(int actionHistoryid)
        {
            string query = "SELECT actiontokens.token FROM `actiontokens` WHERE actiontokens.actionHistoryID = @pId";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pId", actionHistoryid)
            };
            return _databaseCalls.GetOneString(query, parameters);
        }

        public int GetActionIdFromHistoryAction(int historyActionId)
        {
            string query = "SELECT actionhistory.actionID FROM `actionhistory` WHERE actionhistory.ID = @pId";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pId", historyActionId)
            };
            return _databaseCalls.GetOneInt(query, parameters).GetValueOrDefault();
        }

        public string GetCertificateResponsible(int actionId)
        {
            string querty = "SELECT certificate.createdBy FROM `certificate` INNER JOIN action ON action.certificateID = certificate.ID WHERE action.ID = @pActionid";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pActionid", actionId)
            };
            return _databaseCalls.GetOneString(querty, parameters);
        }

        public void DeleteActionHistoryToken(DateTime dateToDelete)
        {
            string query = "DELETE FROM `actiontokens` WHERE actiontokens.dateCreated = @pDate ";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pDate", dateToDelete)
            };
            _databaseCalls.Command(query, parameters);
        }

        public void DeleteActionHistoryToken(int actionHistoryId)
        {
            string query = "DELETE FROM `actiontokens` WHERE actiontokens.actionHistoryID = @pId ";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pId", actionHistoryId)
            };
            _databaseCalls.Command(query, parameters);
        }
    }
}
