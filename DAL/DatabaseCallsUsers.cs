using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DataBaseCallsUsers
    {
        private DatabaseCalls _databaseCalls = new DatabaseCalls();

        public DataTable GetAllUsersFromCompany(string companyname)
        {
            string query = "SELECT * FROM `aspnetusers` WHERE aspnetusers.companyName = @p1 ";
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@p1", companyname));
            return _databaseCalls.Select(query, parameters);
        }

    }
}
