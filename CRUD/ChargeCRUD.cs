using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BusinessSuiteByVesune.CRUD
{
    public class ChargeCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BusinessSuiteByVesune.Properties.Settings.BusinessSuiteVesuneConnectionString"].ToString();

        public int Create(Charge charge)
        {

            int result = 0;
            string textCommand = "INSERT INTO tbl_Charges (JobId, ChargeName, ChargeAmount, ChargeQuantity, ChargeDescription, ChargeNotes) OUTPUT INSERTED.ChargeId VALUES (@JobId, @ChargeName, @ChargeAmount, @ChargeQuantity, @ChargeDescription, @ChargeNotes)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@JobId", charge.JobId);
                command.Parameters.AddWithValue("@ChargeName", charge.ChargeName);
                command.Parameters.AddWithValue("@ChargeAmount", charge.ChargeAmount);
                command.Parameters.AddWithValue("@ChargeQuantity", charge.ChargeQuantity);
                command.Parameters.AddWithValue("@ChargeDescription", charge.ChargeDescription);
                command.Parameters.AddWithValue("@ChargeNotes", charge.ChargeNotes);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["ChargeId"].ToString());
                    }
                    connection.Close();
                }
            }
            return result;
        }
        public Charge Read(int chargeId, int jobId)
        {
            Charge charge = new Charge();
            string textCommand = "SELECT * FROM tbl_Charges WHERE ChargeId = @ChargeId AND JobId = @JobId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ChargeId", chargeId);
                command.Parameters.AddWithValue("@JobId", jobId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        charge.JobId = Convert.ToInt32(reader["JobId"].ToString());
                        charge.ChargeName = reader["ChargeName"].ToString();
                        charge.ChargeAmount = Convert.ToDecimal(reader["ChargeAmount"].ToString());
                        charge.ChargeQuantity = Convert.ToInt32(reader["ChargeQuantity"].ToString());
                        charge.ChargeDescription = reader["ChargeDescription"].ToString();
                        charge.ChargeNotes = reader["ChargeNotes"].ToString();
                    }
                    connection.Close();
                }
            }
            return charge;
        }
        public int Update(Charge charge)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_Charges SET ChargeName = @ChargeName, ChargeAmount = @ChargeAmount, ChargeQuantity = @ChargeQuantity, ChargeDescription = @ChargeDescription, ChargeNotes = @ChargeNotes WHERE ChargeId = @ChargeId AND JobId = @JobId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ChargeId", charge.ChargeId);
                command.Parameters.AddWithValue("@JobId", charge.JobId);
                command.Parameters.AddWithValue("@ChargeName", charge.ChargeName);
                command.Parameters.AddWithValue("@ChargeAmount", charge.ChargeAmount);
                command.Parameters.AddWithValue("@ChargeQuantity", charge.ChargeQuantity);
                command.Parameters.AddWithValue("@ChargeDescription", charge.ChargeDescription);
                command.Parameters.AddWithValue("@ChargeNotes", charge.ChargeNotes);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public int Destroy(int jobId, int chargeId)
        {
            int result = 0;
            string textCommand = "DELETE FROM tbl_Charges WHERE ChargeId = @ChargeId AND @JobId = @JobId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@JobId", jobId);
                command.Parameters.AddWithValue("@ChargeId", chargeId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public List<Charge> GetChargesByJobId(int jobId)
        {
            List<Charge> charges = new List<Charge>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_Charges WHERE JobId = JobId";
                SqlCommand command = new SqlCommand(textCommand, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Charge charge = new Charge
                        {
                            JobId = Convert.ToInt32(reader["JobId"].ToString()),
                            ChargeId = Convert.ToInt32(reader["ChargeId"].ToString()),
                            ChargeName = reader["ChargeName"].ToString(),
                            ChargeAmount = Convert.ToDecimal(reader["ChargeAmount"].ToString()),
                            ChargeQuantity = Convert.ToInt32(reader["ChargeQuantity"].ToString()),
                            ChargeDescription = reader["ChargeDescription"].ToString(),
                            ChargeNotes = reader["ChargeNotes"].ToString(),
                    };

                        charges.Add(charge);
                    }
                    connection.Close();
                }
            }
            return charges;
        }
    }
}
