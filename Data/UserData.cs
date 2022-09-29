using CA_Proj.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.Common;

namespace CA_Proj.Data
{
	public class UserData:Data
	{
        public static bool QueryIfUserExist(string sql,params DbParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameters);
                MySqlDataReader reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                   return true;
                }
                return false;
            }
            
        }

        public static bool UpdateToRegister(string sql, params DbParameter[] parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddRange(parameters);
                int influenceNumber = cmd.ExecuteNonQuery();
                if (influenceNumber == 1)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
