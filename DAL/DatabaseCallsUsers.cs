using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataModels;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class DataBaseCallsUsers
    {
        private DatabaseCalls dbCalls = new DatabaseCalls();

        public void AddUser(UserModel user, string password)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@p1", user.Email));
            parameters.Add(new MySqlParameter("@p2", user.Firstname));
            parameters.Add(new MySqlParameter("@p3", user.Lastname));
            parameters.Add(new MySqlParameter("@p4", user.Preposition));
            parameters.Add(new MySqlParameter("@p5", password));
            string command = "INSERT INTO user (email, firstname, lastname, prepositions, password) VALUES (@p1, @p2, @p3, @p4, @p5)";
            dbCalls.Command(command, parameters);
        }

        public UserModel GetUser(string email)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@p1", email));
            string command = "SELECT * FROM user WHERE user.email = @p1";
            DataTable dbuser = dbCalls.Select(command, parameters);
            UserModel user = new UserModel
            {
                Email = dbuser.Rows[0][0].ToString(),
                Firstname = dbuser.Rows[0][1].ToString(),
                Lastname = dbuser.Rows[0][2].ToString(),
                Preposition = dbuser.Rows[0][3].ToString()
            };

            return user;
        }

        public string GetPassword(string email)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@p1", email));
            string command = "SELECT password FROM user WHERE user.email = @p1";
            return dbCalls.GetOneString(command, parameters);
        }
    }
}
