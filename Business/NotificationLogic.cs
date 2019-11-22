using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataModels;
using DAL;

namespace Business
{
    public class NotificationLogic
    {
        private readonly DatabaseCallsNotifications _databaseCallsNotifications = new DatabaseCallsNotifications();

        private readonly AppLogic _appLogic = new AppLogic();

        public void CheckActionTokens()
        {
            DateTime dateToDelete = DateTime.Today.AddDays(-1);
            _databaseCallsNotifications.DeleteActionHistoryToken(dateToDelete);
        }

        public void ExecuteTask(string actionName,string certificateName ,bool executionSucces, ApplicationUser user)
        {
            _appLogic.CompleteAction(executionSucces, actionName, certificateName, user);
        }

        private List<int> GetUnexecutedHistoryActionIds(DateTime dateToNotify)
        {
            List<int> result = new List<int>();
            foreach (DataRow row in _databaseCallsNotifications.GetUnexecutedActionsIds(dateToNotify).Rows)
            {
                int id = Convert.ToInt32(row[0]);
                result.Add(id);
            }
            return result;
        }

        public List<int> Check3DayNotifications()
        {
            DateTime todayDate = DateTime.Today;
            DateTime dateToNotify = todayDate.AddDays(3);
            return GetUnexecutedHistoryActionIds(dateToNotify);
        }

        public List<int> CheckTodayNotifications()
        {
            DateTime dateToNotify = DateTime.Today;
            return GetUnexecutedHistoryActionIds(dateToNotify);
        }

        public List<int> CheckNotCompletedTasks()
        {
            DateTime todayDate = DateTime.Today;
            DateTime dateToNotify = todayDate.AddDays(-1);
            return GetUnexecutedHistoryActionIds(dateToNotify);
        }

        public ActionToComplete GetAction(int actionHistoryId)
        {
            int id = _databaseCallsNotifications.GetActionIdFromHistoryAction(actionHistoryId);
            return _appLogic.GetActionToComplete(id);
        }

        public string GetToken(int actionHistoryId)
        {
           return _databaseCallsNotifications.GetActionHistoryToken(actionHistoryId);
        }

        public string GetCertificateResponsibleUserEmail(int actionHistoryId)
        {
            int actionId = _databaseCallsNotifications.GetActionIdFromHistoryAction(actionHistoryId);
            return _databaseCallsNotifications.GetCertificateResponsible(actionId);
        }
    }
}
