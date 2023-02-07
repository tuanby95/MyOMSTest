﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject2
{
    public static class DataLogic
    {
        public static class SqlHelper
        {
            // Set the connection, command, and then execute the command with non query.  
            public static Int32 ExecuteNonQuery(String connectionString, String commandText,
                CommandType commandType)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(commandText, conn))
                    {
                        // There're three command types: StoredProcedure, Text, TableDirect. The TableDirect   
                        // type is only for OLE DB.    
                        cmd.CommandType = commandType;

                        conn.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }

            // Set the connection, command, and then execute the command and only return one value.  
            public static Object ExecuteScalar(String connectionString, String commandText,
                CommandType commandType)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(commandText, conn))
                    {
                        cmd.CommandType = commandType;

                        conn.Open();
                        return cmd.ExecuteScalar();
                    }
                }
            }

            // Set the connection, command, and then execute the command with query and return the reader.  
            public static SqlDataReader ExecuteReader(String connectionString, String commandText,
                CommandType commandType)
            {
                SqlConnection conn = new SqlConnection(connectionString);

                using (SqlCommand cmd = new SqlCommand(commandText, conn))
                {
                    cmd.CommandType = commandType;

                    conn.Open();
                    // When using CommandBehavior.CloseConnection, the connection will be closed when the   
                    // IDataReader is closed.  
                    SqlDataReader reader = cmd.ExecuteReader();

                    return reader;
                }
            }
        }
    }
}
