using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DataBaseCallsOwner
    {
        private readonly DatabaseCalls _databaseCalls = new DatabaseCalls();

        public void AddCompany(string name, string inviteCode)
        {
            string query = "INSERT INTO `company`(`name`, `inviteCode`) VALUES (@pName, @pInviteCode)";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pName", name),
                new MySqlParameter("@pInviteCode", inviteCode)
            };
            _databaseCalls.Command(query, parameters);
        }

        public void DeleteCompany(string name)
        {
            string query = "DELETE FROM `company` WHERE company.name = @pName";
            List<MySqlParameter> parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@pName", name)
            };
            _databaseCalls.Command(query, parameters);
        }

        public DataTable GetAllCompanies()
        {
            string query = "SELECT * FROM `company` WHERE 1";
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            return _databaseCalls.Select(query, parameters);
        }
    }
}
