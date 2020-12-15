using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BusinessSuiteByVesune.CRUD
{
    public class InvoiceCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BusinessSuiteByVesune.Properties.Settings.BusinessSuiteVesuneConnectionString"].ToString();

        public int Create(Invoice invoice)
        {

            int result = 0;
            string textCommand = "INSERT INTO tbl_Invoices (InvoiceName, InvoiceTypeId, JobId) OUTPUT INSERTED.InvoiceId VALUES (@InvoiceName, @InvoiceTypeId, @JobId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@InvoiceName", invoice.InvoiceName);
                command.Parameters.AddWithValue("@InvoiceTypeId", invoice.InvoiceTypeId);
                command.Parameters.AddWithValue("@JobId", invoice.JobId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["InvoiceId"].ToString());
                    }
                    connection.Close();
                }
            }
            return result;
        }
        public Invoice Read(int invoiceId)
        {
            Invoice invoice = new Invoice();
            string textCommand = "SELECT * FROM tbl_Invoices WHERE InvoiceId = @InvoiceId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@InvoiceId", invoiceId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        invoice.InvoiceId = Convert.ToInt32(reader["InvoiceId"].ToString());
                        invoice.InvoiceTypeId = Convert.ToInt32(reader["InvoiceTypeId"].ToString());
                        invoice.JobId = Convert.ToInt32(reader["JobId"].ToString());
                        invoice.InvoiceName = reader["InvoiceName"].ToString();
                    }
                    connection.Close();
                }
            }
            return invoice;
        }
        public int Update(Invoice invoice)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_Invoices SET InvoiceName = @InvoiceName, InvoiceTypeId = @InvoiceTypeId WHERE InvoiceId = @InvoiceId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@InvoiceName", invoice.InvoiceName);
                command.Parameters.AddWithValue("@InvoiceTypeId", invoice.InvoiceTypeId);
                command.Parameters.AddWithValue("@InvoiceId", invoice.InvoiceId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public int Destroy(int invoiceId)
        {
            int result = 0;
            string textCommand = "DELETE FROM tbl_Invoices WHERE InvoiceId = @InvoiceId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@InvoiceId", invoiceId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public List<Invoice> GetInvoices()
        {
            List<Invoice> invoices = new List<Invoice>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_Invoices";

                SqlCommand command = new SqlCommand(textCommand, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Invoice invoice = new Invoice
                        {
                            InvoiceId = Convert.ToInt32(reader["InvoiceId"].ToString()),
                            InvoiceTypeId = Convert.ToInt32(reader["InvoiceTypeId"].ToString()),
                            JobId = Convert.ToInt32(reader["JobId"].ToString()),
                            InvoiceName = reader["InvoiceName"].ToString(),
                        };

                        invoices.Add(invoice);
                    }
                    connection.Close();
                }
            }
            return invoices;
        }
        public List<Invoice> GetInvoicesByJobId(int jobId)
        {
            List<Invoice> invoices = new List<Invoice>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_Invoices WHERE JobId = JobId";

                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@JobId", jobId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Invoice invoice = new Invoice
                        {
                            InvoiceId = Convert.ToInt32(reader["InvoiceId"].ToString()),
                            InvoiceTypeId = Convert.ToInt32(reader["InvoiceTypeId"].ToString()),
                            JobId = Convert.ToInt32(reader["JobId"].ToString()),
                            InvoiceName = reader["InvoiceName"].ToString(),
                        };

                        invoices.Add(invoice);
                    }
                    connection.Close();
                }
            }
            return invoices;
        }
        public List<InvoiceType> GetInvoiceTypes()
        {
            List<InvoiceType> invoices = new List<InvoiceType>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_InvoiceTypes";

                SqlCommand command = new SqlCommand(textCommand, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InvoiceType invoice = new InvoiceType
                        {
                            InvoiceTypeName = reader["InvoiceTypeName"].ToString(),
                            InvoiceTypeId = Convert.ToInt32(reader["InvoiceTypeId"].ToString()),
                        };

                        invoices.Add(invoice);
                    }
                    connection.Close();
                }
            }
            return invoices;
        }
    }
}
