using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace BusinessSuiteByVesune.CRUD
{
    public class JobCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BusinessSuiteByVesune.Properties.Settings.BusinessSuiteVesuneConnectionString"].ToString();
        
        public int Create(Job job)
        {
            int result = 0;
            string textCommand = "INSERT INTO tbl_Jobs (Name, Description, Notes, BalanceDue, City, Mobile, ScheduledDate, JobStatusTypeId, JobTypeId) OUTPUT INSERTED.JobId VALUES (@Name, @Description, @Notes, @BalanceDue, @City, @Mobile, @ScheduledDate, @JobStatusTypeId, @JobTypeId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@Name", job.Name);
                command.Parameters.AddWithValue("@Description", job.Description);
                command.Parameters.AddWithValue("@Notes", job.Notes);
                command.Parameters.AddWithValue("@BalanceDue", job.BalanceDue);
                command.Parameters.AddWithValue("@City", job.City);
                command.Parameters.AddWithValue("@Mobile", job.Mobile);
                command.Parameters.AddWithValue("@ScheduledDate", job.ScheduledDate);
                command.Parameters.AddWithValue("@JobStatusTypeId", job.JobStatusTypeId);
                command.Parameters.AddWithValue("@JobTypeId", job.JobTypeId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["JobId"].ToString());
                    }
                    connection.Close();
                }
            }
            return result;
        }
        public Job Read(int jobId)
        {
            Job job = new Job();
            string textCommand = "SELECT * FROM tbl_Jobs WHERE JobId = @JobId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@JobId", jobId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        job.Name = reader["Name"].ToString();
                        job.Description = reader["Description"].ToString();
                        job.Notes = reader["Notes"].ToString();
                        job.BalanceDue = Convert.ToDecimal(reader["BalanceDue"].ToString());
                        job.City = reader["City"].ToString();
                        job.Mobile = Convert.ToBoolean(reader["Mobile"].ToString());
                        job.ScheduledDate = Convert.ToDateTime(reader["ScheduledDate"].ToString());
                        job.JobStatusTypeId = Convert.ToInt32(reader["JobStatusTypeId"].ToString());
                        job.JobTypeId = Convert.ToInt32(reader["JobTypeId"].ToString());

                    }
                    connection.Close();
                }
            }
            return job;
        }
        public int Update(Job job)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_Jobs SET Name = @Name, Description = @Description, Notes = @Notes, BalanceDue = @BalanceDue, City = @City, Mobile = @Mobile, ScheduledDate = @ScheduledDate, JobStatusTypeId = @JobStatusTypeId, JobTypeId = @JobTypeId WHERE JobId = @JobId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@JobId", job.JobId);
                command.Parameters.AddWithValue("@Name", job.Name);
                command.Parameters.AddWithValue("@Description", job.Description);
                command.Parameters.AddWithValue("@Notes", job.Notes);
                command.Parameters.AddWithValue("@BalanceDue", job.BalanceDue);
                command.Parameters.AddWithValue("@City", job.City);
                command.Parameters.AddWithValue("@Mobile", job.Mobile);
                command.Parameters.AddWithValue("@ScheduledDate", job.ScheduledDate);
                command.Parameters.AddWithValue("@JobStatusTypeId", job.JobStatusTypeId);
                command.Parameters.AddWithValue("@JobTypeId", job.JobTypeId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public int Destroy(int jobId)
        {
            int result = 0;
            string textCommand = "DELETE FROM tbl_Jobs WHERE JobId = @JobId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@JobId", jobId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }

            if (result > 0)
            {
                // delete charges
                List<Charge> charges = new ChargeCRUD().GetChargesByJobId(jobId);

                if (charges.Count > 0)
                {
                    foreach (Charge charge in charges)
                    {
                        new ChargeCRUD().Destroy(jobId, charge.ChargeId);
                    }
                }

                // remove orders
                List<Order> orders = new OrderCRUD().GetOrders().Where(x => x.JobId == jobId).ToList();

                if (orders.Count > 0)
                {
                    foreach (Order order in orders)
                    {
                        order.JobId = 0;
                        new OrderCRUD().Update(order);
                    }
                }

                // remove invoices
                List<Invoice> invoices = new InvoiceCRUD().GetInvoicesByJobId(jobId);

                if (invoices.Count > 0)
                {
                    foreach (Invoice invoice in invoices)
                    {
                        invoice.JobId = 0;
                        new InvoiceCRUD().Update(invoice);
                    }
                }

                // remove work periods
                List<WorkPeriod> workPeriods = new WorkPeriodCRUD().GetWorkPeriodsByJobId(jobId);

                if (workPeriods.Count > 0)
                {
                    foreach (WorkPeriod workPeriod in workPeriods)
                    {
                        workPeriod.JobId = 0;
                        new WorkPeriodCRUD().Update(workPeriod);
                    }
                }



            }

            return result;
        }
        public List<Job> GetJobs()
        {
            List<Job> jobs = new List<Job>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_Jobs";
                SqlCommand command = new SqlCommand(textCommand, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Job job = new Job
                        {
                            JobId = Convert.ToInt32(reader["JobId"].ToString()),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            Notes = reader["Notes"].ToString(),
                            BalanceDue = Convert.ToDecimal(reader["BalanceDue"]),
                            City = reader["City"].ToString(),
                            Mobile = Convert.ToBoolean(reader["Mobile"].ToString()),
                            ScheduledDate = Convert.ToDateTime(reader["ScheduledDate"].ToString()),
                            JobStatusTypeId = Convert.ToInt32(reader["JobStatusTypeId"].ToString()),
                            JobTypeId = Convert.ToInt32(reader["JobTypeId"].ToString()),
                    };

                        jobs.Add(job);
                    }
                    connection.Close();
                }
            }
            return jobs;
        }
    }
}
