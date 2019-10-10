using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DatabaseCallsAdmin
    {
        private readonly DatabaseCalls _databaseCalls = new DatabaseCalls();

        public DataTable GetAllUsers(string companyname)
        {
            string query = "SELECT * FROM `aspnetusers` WHERE aspnetusers.companyName = @p1 ";
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@p1", companyname));
            return _databaseCalls.Select(query, parameters);
        }
    }
}
