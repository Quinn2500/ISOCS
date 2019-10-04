﻿using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace DAL
{
    class DatabaseCalls
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private MySqlConnection _msConn = new MySqlConnection("Server=localhost; database=isocsdb; UID=root; password=; Sslmode=none;");

        private bool testConnection()
        {
            try
            {
                _msConn.Open();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
            finally
            {
                _msConn.Close();
            }
            return true;
        }



        public void Command(string query, List<MySqlParameter> sqlParameters)
        {
            if (!testConnection()) return;
            MySqlCommand mySqlCommand = new MySqlCommand(query, _msConn);
            foreach (MySqlParameter param in sqlParameters)
                mySqlCommand.Parameters.Add(param);
            _msConn.Open();
            mySqlCommand.ExecuteNonQuery();
            _msConn.Close();
        }

        public DataTable Select(string query, List<MySqlParameter> sqlParameters)
        {
            if (!testConnection()) return null;
            DataTable dtResult = new DataTable();
            MySqlCommand cmd = new MySqlCommand(query, _msConn);
            foreach (MySqlParameter param in sqlParameters)
                cmd.Parameters.Add(param);
            _msConn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            dtResult.Load(reader);
            _msConn.Close();
            return dtResult;
        }

        public string GetOneString(string query, List<MySqlParameter> sqlParameters)
        {
            if (!testConnection()) return null;
            string response = "empty";
            MySqlCommand cmd = new MySqlCommand(query, _msConn);
            foreach (MySqlParameter param in sqlParameters)
                cmd.Parameters.Add(param);
            _msConn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                response = reader.GetString(0);
            }
            _msConn.Close();
            return response;
        }

        public int? LastID()
        {
            if (!testConnection()) return null;
            int response = 0;
            MySqlCommand cmd = new MySqlCommand("SELECT LAST_INSERT_ID()", _msConn);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                response = Convert.ToInt32(reader.GetString(0));
            }

            return response;
        }
    }
}