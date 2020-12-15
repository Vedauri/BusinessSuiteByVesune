using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BusinessSuiteByVesune.CRUD
{
    public class UserCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BusinessSuiteByVesune.Properties.Settings.BusinessSuiteVesuneConnectionString"].ToString();

        public User Login(string pin)
        {
            User user = new User();
            string textCommand = "SELECT * FROM tbl_Users WHERE Pin = @Pin";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@Pin", pin);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.UserId = Convert.ToInt32(reader["UserId"].ToString());
                        user.Name = reader["Name"].ToString();
                        user.Pin = reader["Pin"].ToString();
                        user.Working = Convert.ToBoolean(reader["Working"].ToString());
                        user.WorkPeriodId = Convert.ToInt32(reader["WorkPeriodId"].ToString());
                        user.WorkRate = Convert.ToDecimal(reader["WorkRate"].ToString());
                    }
                    connection.Close();
                }
            }
            return user;
        }
        public int Create(User user)
        {
            int result = 0;
            string textCommand = "INSERT INTO tbl_Users (Name, WorkPeriodId, Pin, Working, WorkRate) OUTPUT INSERTED.UserId VALUES (@Name, @WorkPeriodId, @Pin, @Working, @WorkRate)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@WorkPeriodId", 0);
                command.Parameters.AddWithValue("@Pin", user.Pin);
                command.Parameters.AddWithValue("@Working", false);
                command.Parameters.AddWithValue("@WorkRate", user.WorkRate);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["UserId"].ToString());
                    }
                    connection.Close();
                }
            }
            return result;
        }
        public User Read(int userId)
        {
            User user = new User();
            string textCommand = "SELECT * FROM tbl_Users WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.UserId = Convert.ToInt32(reader["UserId"].ToString());
                        user.Name = reader["Name"].ToString();
                        user.Pin = reader["Pin"].ToString();
                        user.Working = Convert.ToBoolean(reader["Working"].ToString());
                        user.WorkPeriodId = Convert.ToInt32(reader["WorkPeriodId"].ToString());
                        user.WorkRate = Convert.ToDecimal(reader["WorkRate"].ToString());

                    }
                    connection.Close();
                }
            }
            return user;
        }
        public int Update(User user)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_Users SET Name = @Name, WorkPeriodId = @WorkPeriodId, Pin = @Pin, Working = @Working, WorkRate = @WorkRate WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@UserId", user.UserId);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@WorkPeriodId", user.WorkPeriodId);
                command.Parameters.AddWithValue("@Pin", user.Pin);
                command.Parameters.AddWithValue("@Working", user.Working);
                command.Parameters.AddWithValue("@WorkRate", user.WorkRate);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public int Destroy(int userId)
        {
            int result = 0;
            string textCommand = "DELETE FROM tbl_Users WHERE UserId = @UserId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            string textCommand = "SELECT * FROM tbl_Users";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"].ToString()),
                            Name = reader["Name"].ToString(),
                            Pin = reader["Pin"].ToString(),
                            Working = Convert.ToBoolean(reader["Working"].ToString()),
                            WorkPeriodId = Convert.ToInt32(reader["WorkPeriodId"].ToString()),
                            WorkRate = Convert.ToDecimal(reader["WorkRate"].ToString())
                        };
                        users.Add(user);
                    }
                    connection.Close();
                }
            }
            return users;
        }
    }
}
