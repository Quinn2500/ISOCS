  using System;
using System.Collections.Generic;
  using System.Data;
  using System.Text;
using DataModels;
using DAL;

namespace Business
{
    public class UserLogic
    {
        private readonly DataBaseCallsUsers _databaseCalls = new DataBaseCallsUsers();

        public List<string> GetAllUsersEmailAdresses(string companyname)
        {
            List<string> result = new List<string>();
            DataTable dataTable = _databaseCalls.GetAllUsersFromCompany(companyname);
            foreach (DataRow dr in dataTable.Rows)
            {
                result.Add(dr[3].ToString());

            }

            return result;
        }

    }
}
