using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Reflection;
using Tasker.API.Models;
//using Tasker.API.Models.Objects;
namespace Tasker.API.DBConnection
{
    public class SQLConnection
    {
        private SqlConnectionStringBuilder builder;
        private static SQLConnection INSTANCE;

        /// <summary>
        /// create connection to database
        /// </summary>
        private SQLConnection()
        {
            try
            {
                builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["SQLConnection"].ToString());
            }
            catch (Exception e)
            {
                throw new Exception("35", e);
            }
        }

        public static SQLConnection GetInstance()
        {
            if (INSTANCE == null)
            {
                INSTANCE = new SQLConnection();
            }
            return INSTANCE;
        }

        public List<string> SelectTasks()
        {
            
            return SQLConnection.GetInstance().SelectQuery("SELECT * FROM Tasks");
        }

        private List<string> SelectQuery(string query)
        {
            List<String> result = new List<string>();

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                if (!(connection.State == System.Data.ConnectionState.Open))
                {
                    connection.Open();
                }
;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StringBuilder currRes = new StringBuilder();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (i == 0)
                                {
                                    currRes.Append(reader[i]);
                                }
                                else
                                {
                                    currRes.Append(",");
                                    currRes.Append(reader[i]);
                                }
                            }

                            result.Add(currRes.ToString());
                        }
                    }
                }
            }
            return result;
        }

    }
}