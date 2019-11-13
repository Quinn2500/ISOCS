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

        private readonly EmailLogic _emailLogic = new EmailLogic();


        public void DailyChecks()
        {
            Send3DayNotifications();
            SendTodayNotifications();
            SendUncompletedTaskNotifications();
            CheckActionTokens();
        }

        public void Send3DayNotifications()
        {
            foreach (int id in Check3DayNotifications())
            {
                ActionToComplete action = _appLogic.GetActionToComplete(_databaseCallsNotifications.GetActionIdFromHistoryAction(id));
                _emailLogic.Send3DayNotificationEmail(action);
            }
        }

        public void SendTodayNotifications()
        {
            foreach (int id in CheckTodayNotifications())
            {
                ActionToComplete action =
                    _appLogic.GetActionToComplete(_databaseCallsNotifications.GetActionIdFromHistoryAction(id));
                string token = _databaseCallsNotifications.GetActionHistoryToken(id);
                _emailLogic.SendTodayNotificationEmail(action, token, id);
            }
        }

        public void SendUncompletedTaskNotifications()
        {
            foreach (int id in CheckNotCompletedTasks())
            {
                ActionToComplete action = _appLogic.GetActionToComplete(_databaseCallsNotifications.GetActionIdFromHistoryAction(id));
                _emailLogic.SendUncompletedTaskEmail(action);
            }
        }

        public void CheckActionTokens()
        {
            DateTime dateToDelete = DateTime.Today.AddDays(-1);
            _databaseCallsNotifications.DeleteActionHistoryToken(dateToDelete);
        }

        public bool ExecuteTask(string token, int actionHistoryId, string actionName,string certificateName ,bool executionSucces, ApplicationUser user)
        {
            string dbToken = _databaseCallsNotifications.GetActionHistoryToken(actionHistoryId);
            if (!dbToken.Equals(token))
            {
                return false;
            }

            _appLogic.CompleteAction(executionSucces, actionName, certificateName, user);
            return true;
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

        private List<int> Check3DayNotifications()
        {
            DateTime todayDate = DateTime.Today;
            DateTime dateToNotify = todayDate.AddDays(3);
            return GetUnexecutedHistoryActionIds(dateToNotify);
        }

        private List<int> CheckTodayNotifications()
        {
            DateTime dateToNotify = DateTime.Today;
            return GetUnexecutedHistoryActionIds(dateToNotify);
        }

        private List<int> CheckNotCompletedTasks()
        {
            DateTime todayDate = DateTime.Today;
            DateTime dateToNotify = todayDate.AddDays(-1);
            return GetUnexecutedHistoryActionIds(dateToNotify);
        }

    }
}
