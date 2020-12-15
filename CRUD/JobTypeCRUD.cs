using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BusinessSuiteByVesune.CRUD
{
    public class JobTypeCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BusinessSuiteByVesune.Properties.Settings.BusinessSuiteVesuneConnectionString"].ToString();

        public int Create(JobType jobType)
        {
            int result = 0;
            string textCommand = "INSERT INTO tbl_JobTypes (JobTypeName) OUTPUT INSERTED.JobTypeId VALUES (@JobTypeName)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@JobTypeName", jobType.JobTypeName);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["JobTypeId"].ToString());
                    }
                    connection.Close();
                }
            }
            return result;
        }
        public JobType Read(int jobTypeId)
        {
            JobType jobType = new JobType();
            string textCommand = "SELECT * FROM tbl_JobTypes WHERE JobTypeId = @JobTypeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@JobTypeId", jobTypeId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        jobType.JobTypeId = Convert.ToInt32(reader["JobTypeId"].ToString());
                        jobType.JobTypeName = reader["JobTypeName"].ToString();
                    }
                    connection.Close();
                }
            }
            return jobType;
        }
        public int Update(JobType jobType)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_JobTypes SET JobTypeName = @JobTypeName WHERE JobTypeId = @JobTypeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@JobTypeId", jobType.JobTypeId);
                command.Parameters.AddWithValue("@JobTypeName", jobType.JobTypeName);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public int Destroy(int jobTypeId)
        {
            int result = 0;
            string textCommand = "DELETE FROM tbl_JobTypes WHERE JobTypeId = @JobTypeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@JobTypeId", jobTypeId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public List<JobType> GetJobTypes()
        {
            List<JobType> jobTypes = new List<JobType>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_JobTypes";
                SqlCommand command = new SqlCommand(textCommand, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        JobType jobType = new JobType
                        {
                            JobTypeId = Convert.ToInt32(reader["JobTypeId"].ToString()),
                            JobTypeName = reader["JobTypeName"].ToString()
                    };

                        jobTypes.Add(jobType);
                    }
                    connection.Close();
                }
            }
            return jobTypes;
        }
        public List<JobStatusType> GetJobStatusTypes()
        {
            List<JobStatusType> jobStatusTypes = new List<JobStatusType>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_JobStatusTypes";
                SqlCommand command = new SqlCommand(textCommand, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        JobStatusType jobStatusType = new JobStatusType
                        {
                            JobStatusTypeId = Convert.ToInt32(reader["JobStatusTypeId"].ToString()),
                            JobStatusTypeName = reader["JobStatusTypeName"].ToString()
                        };

                        jobStatusTypes.Add(jobStatusType);
                    }
                    connection.Close();
                }
            }
            return jobStatusTypes;
        }
    }
}
