using System;
using System.Collections.Generic;
using System.Text;
using DataModels;
using DAL;

namespace Business
{
    public class UserLogic
    {
        private DataBaseCallsUsers dbCalls = new DataBaseCallsUsers();

        public void RegisterUser(UserModel user, string password)
        {
            dbCalls.AddUser(user, password);
        }

        public UserModel LoginUser(string email, string password)
        {
            if (Security.CheckPassword(dbCalls.GetPassword(email), password))
            {
                return dbCalls.GetUser(email);
            }

            return null;
        }

        public bool CheckPassword(string email, string password)
        {
            return Security.CheckPassword(dbCalls.GetPassword(email), password);
        }
    }
}
