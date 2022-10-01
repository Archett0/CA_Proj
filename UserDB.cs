using CA_Proj.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;

namespace CA_Proj
{
    public class UserDB
    {
        private string connStr;

        public UserDB(string connStr)
        {
            this.connStr = connStr;
        }

        public User GetUserBySession(string sessionId)
        {
            User user = null;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string sql = String.Format(@"SELECT u.user_id, username, password, nickname
                                           FROM session s, `user` u
                                           WHERE s.user_id = u.user_id AND 
                                           s.session_id = '{0}'", sessionId);

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Id = (int)reader["user_id"],
                                Username = (string)reader["username"],
                                Password = (string)reader["password"],
                                Nickname = (string)reader["nickname"]
                            };
                        }
                    }
                }

                conn.Close();
            }

            return user;
        }

        public User GetUserByUsername(string username)
        {
            User user = null;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                //Console.WriteLine("GetUserByUsername:conn.Open();");
                string q = String.Format(@"SELECT * FROM `user`
                WHERE username = '{0}'", username);

                using (MySqlCommand cmd = new MySqlCommand(q, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        //Console.WriteLine("GetUserByUsername:reader");
                        if (reader.Read())
                        {
                            //Console.WriteLine("GetUserByUsername:getuser");
                            user = new User
                            {
                                Id = (int)reader["user_id"],
                                Username = (string)reader["username"],
                                Password = (string)reader["password"],
                                Nickname = (string)reader["nickname"]
                            };
                        }
                    }
                }
                conn.Close();
            }
            //Console.WriteLine("GetUserByUsername:returnuser");
            return user;
        }

        public string AddSession(int userId)
        {
            string sessionId = null;
            Guid guid = Guid.NewGuid();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string q = String.Format(@"INSERT INTO session(
                session_id, user_id, timestamp) VALUES('{0}', '{1}', {2})",
                        guid, userId, DateTimeOffset.Now.ToUnixTimeSeconds());

                using (MySqlCommand cmd = new MySqlCommand(q, conn))
                {
                    if (cmd.ExecuteNonQuery() == 1)//only one influence
                    {
                        sessionId = guid.ToString();
                    }
                }

                conn.Close();
            }

            return sessionId;
        }

        public bool RemoveSession(string sessionId)
        {
            bool status = false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();

                string q = String.Format(@"DELETE FROM session
                WHERE session_id = '{0}'", sessionId);

                using (MySqlCommand cmd = new MySqlCommand(q, conn))
                {
                    if (cmd.ExecuteNonQuery() == 1)//only one can be delete
                    {
                        status = true;
                    }
                }

                conn.Close();
            }

            return status;
        }
    }
}
