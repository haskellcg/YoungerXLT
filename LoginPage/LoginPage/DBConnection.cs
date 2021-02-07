using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySQLDriverCS;

namespace ConsoleDB
{
    class DBConnection
    {
        private string server = "localhost";
        private string database = "youngerxlt";
        private string login = "root";
        private string pass = " ";
        public MySQLConnection connection;
        public MySQLCommand command;
        public MySQLDataReader dataReader;

        public MySQLConnection GetConnection()
        {
            connection = new MySQLConnection(new MySQLConnectionString(server, database, login, pass).AsString);
            return connection;
        }

        public MySQLDataReader GetDataReader(string commandStr)
        {
            connection.Open();

            MySQLCommand cmdTemp = (MySQLCommand)connection.CreateCommand();
            cmdTemp.CommandText = "set names gbk";
            cmdTemp.ExecuteNonQuery();

            command = (MySQLCommand)connection.CreateCommand();
            command.CommandText = commandStr;
            dataReader = (MySQLDataReader)command.ExecuteReader();

            return dataReader;

        }

        public int ExecuteNonQuery(string commandStr)
        {
            connection.Open();

            MySQLCommand cmdTemp = (MySQLCommand)connection.CreateCommand();
            cmdTemp.CommandText = "set names gbk";
            cmdTemp.ExecuteNonQuery();

            command = (MySQLCommand)connection.CreateCommand();
            command.CommandText = commandStr;
            return command.ExecuteNonQuery();
        }

        public int ExecuteNonQueryForInc(string commandStr)
        {
            connection.Open();

            MySQLCommand cmdTemp1 = (MySQLCommand)connection.CreateCommand();
            cmdTemp1.CommandText = "set names gbk";
            cmdTemp1.ExecuteNonQuery();

            command = (MySQLCommand)connection.CreateCommand();
            command.CommandText = commandStr;
            command.ExecuteNonQuery();

            MySQLCommand cmdTemp2 = (MySQLCommand)connection.CreateCommand();
            cmdTemp2.CommandText = "select last_insert_id()";
            MySQLDataReader readerTemp;
            readerTemp=(MySQLDataReader)cmdTemp2.ExecuteReader();
            int LastID=-1;
            while (readerTemp.Read())
            {
                LastID=readerTemp.GetInt16(0);
            }

            return LastID;
        }

        public void Close()
        {
            connection.Close();
        }

    }
}
