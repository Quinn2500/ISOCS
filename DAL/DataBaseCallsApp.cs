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
    }
}
