using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DataServices
{
    public class StateConnection
    {
        private NpgsqlConnection connection = new NpgsqlConnection("Server=localhost; Port=5432; Database=BasicToDoList; User Id=postgres; Password = 3545;");

        protected NpgsqlConnection OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

        protected void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
