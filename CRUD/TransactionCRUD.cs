using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using BusinessSuiteByVesune.Objects;

namespace BusinessSuiteByVesune.CRUD
{
    public class TransactionCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BusinessSuiteByVesune.Properties.Settings.BusinessSuiteVesuneConnectionString"].ToString();

        public int Create(Transaction trans)
        {
            int result = 0;
            string textCommand = "INSERT INTO tbl_Transactions (Name, Amount, Notes, JobId, UserId) OUTPUT INSERTED.TransactionId VALUES (@Name, @Amount, @Notes, @JobId, @UserId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@Name", trans.Name);
                command.Parameters.AddWithValue("@Amount", trans.Amount);
                command.Parameters.AddWithValue("@Notes", trans.Notes);
                command.Parameters.AddWithValue("@JobId", trans.JobId);
                command.Parameters.AddWithValue("@UserId", trans.UserId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["TransactionId"].ToString());
                    }
                    connection.Close();
                }
            }
            return result;
        }
        public Transaction Read(int transactionId)
        {
            Transaction trans = new Transaction();
            string textCommand = "SELECT * FROM tbl_Transactions WHERE TransactionId = @TransactionId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@TransactionId", transactionId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        trans.Name = reader["Name"].ToString();
                        trans.Amount = Convert.ToDecimal(reader["Amount"].ToString());
                        trans.Notes = reader["Notes"].ToString();
                        trans.JobId = Convert.ToInt32(reader["JobId"].ToString());
                        trans.UserId = Convert.ToInt32(reader["UserId"].ToString());

                    }
                    connection.Close();
                }
            }
            return trans;
        }
        public int Update(Transaction trans)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_Transactions SET Name = @Name, Amount = @Amount, Notes = @Notes, JobId = @JobId, UserId = @UserId WHERE TransactionId = @TransactionId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@Name", trans.Name);
                command.Parameters.AddWithValue("@Amount", trans.Amount);
                command.Parameters.AddWithValue("@Notes", trans.Notes);
                command.Parameters.AddWithValue("@JobId", trans.JobId);
                command.Parameters.AddWithValue("@UserId", trans.UserId);
                command.Parameters.AddWithValue("@TransactionId", trans.TransactionId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public int Destroy(int transactionId)
        {
            int result = 0;
            string textCommand = "DELETE FROM tbl_Transactions WHERE TransactionId = @TransactionId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@TransactionId", transactionId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public List<Transaction> GetTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_Transactions";
                SqlCommand command = new SqlCommand(textCommand, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transaction trans = new Transaction
                        {
                            TransactionId = Convert.ToInt32(reader["TransactionId"].ToString()),
                            Name = reader["Name"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"].ToString()),
                            Notes = reader["Notes"].ToString(),
                            JobId = Convert.ToInt32(reader["JobId"].ToString()),
                            UserId = Convert.ToInt32(reader["UserId"].ToString()),
                        };

                        transactions.Add(trans);
                    }
                    connection.Close();
                }
            }
            return transactions;
        }
    }
}
