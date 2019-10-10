using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataModels;
using DAL;

namespace Business
{
    public class AdminLogic
    {
        private DatabaseCallsAdmin databaseCalls = new DatabaseCallsAdmin();

        public List<string> GetAllUsersEmailAdresses(string companyname)
        {
            List<string> result = new List<string>();
            DataTable dataTable = databaseCalls.GetAllUsers(companyname);
            foreach (DataRow dr in dataTable.Rows)
            {
                result.Add(dr[3].ToString());

            }

            return result;
        }
    }
}
