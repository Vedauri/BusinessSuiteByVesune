using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BusinessSuiteByVesune.CRUD
{
    public class OrderCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BusinessSuiteByVesune.Properties.Settings.BusinessSuiteVesuneConnectionString"].ToString();
        public int Create(Order order)
        {
            int result = 0;
            string textCommand = "INSERT INTO tbl_Orders (Quantity, OrderStatusId, Price, Name, Description, OrderedDate, ReceivedDate, JobId) OUTPUT INSERTED.OrderId VALUES (@Quantity, @OrderStatusId, @Price, @Name, @Description, @OrderedDate, @ReceivedDate, @JobId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@Quantity", order.Quantity);
                command.Parameters.AddWithValue("@OrderStatusId", order.OrderStatusId);
                command.Parameters.AddWithValue("@Price", order.Price);
                command.Parameters.AddWithValue("@Name", order.Name);
                command.Parameters.AddWithValue("@Description", order.Description);
                command.Parameters.Add("@OrderedDate", SqlDbType.DateTime).Value = (object)order.OrderedDate ?? DBNull.Value;
                command.Parameters.Add("@ReceivedDate", SqlDbType.DateTime).Value = (object)order.ReceivedDate ?? DBNull.Value;
                command.Parameters.AddWithValue("@JobId", order.JobId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["OrderId"].ToString());
                    }
                    connection.Close();
                }
            }
            return result;
        }
        public Order Read(int orderId)
        {
            Order order = new Order();
            string textCommand = "SELECT * FROM tbl_Orders WHERE OrderId = @OrderId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@OrderId", orderId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        order.OrderId = Convert.ToInt32(reader["OrderId"].ToString());
                        order.Quantity = Convert.ToInt32(reader["Quantity"].ToString());
                        order.OrderStatusId = Convert.ToInt32(reader["OrderStatusId"].ToString());
                        order.Price = Convert.ToDecimal(reader["Price"]);
                        order.Name = reader["Name"].ToString();
                        order.Description = reader["Description"].ToString();
                        order.OrderedDate = Convert.ToDateTime(reader["OrderedDate"].ToString());
                        order.ReceivedDate = Convert.ToDateTime(reader["ReceivedDate"].ToString());
                        order.JobId = Convert.ToInt32(reader["JobId"].ToString());

                    }
                    connection.Close();
                }
            }
            return order;
        }
        public int Update(Order order)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_Orders SET Quantity = @Quantity, OrderStatusId = @OrderStatusId, Price = @Price, Name = @Name, Description = @Description, OrderedDate = @OrderedDate, ReceivedDate = @ReceivedDate, JobId = @JobId WHERE OrderId = @OrderId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@OrderId", order.OrderId);
                command.Parameters.AddWithValue("@Quantity", order.Quantity);
                command.Parameters.AddWithValue("@OrderStatusId", order.OrderStatusId);
                command.Parameters.AddWithValue("@Price", order.Price);
                command.Parameters.AddWithValue("@Name", order.Name);
                command.Parameters.AddWithValue("@Description", order.Description);
                command.Parameters.Add("@OrderedDate", SqlDbType.DateTime).Value = (object)order.OrderedDate ?? DBNull.Value;
                command.Parameters.Add("@ReceivedDate", SqlDbType.DateTime).Value = (object)order.ReceivedDate ?? DBNull.Value;
                command.Parameters.AddWithValue("@JobId", order.JobId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public int Destroy(int orderId)
        {
            int result = 0;
            string textCommand = "DELETE FROM tbl_Orders WHERE OrderId = @OrderId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@OrderId", orderId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_Orders";
                SqlCommand command = new SqlCommand(textCommand, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            OrderId = Convert.ToInt32(reader["OrderId"].ToString()),
                            Quantity = Convert.ToInt32(reader["Quantity"].ToString()),
                            OrderStatusId = Convert.ToInt32(reader["OrderStatusId"].ToString()),
                            Price = Convert.ToDecimal(reader["Price"]),
                            Name = reader["Name"].ToString(),
                            Description = reader["Description"].ToString(),
                            OrderedDate = reader["OrderedDate"] as DateTime?,
                            ReceivedDate = reader["ReceivedDate"] as DateTime?,
                            JobId = Convert.ToInt32(reader["JobId"]),
                        };

                        // (DateTime?)reader... ?? Convert.ToDateTime(reader["ReceivedDate"].ToString()),

                        orders.Add(order);
                    }
                    connection.Close();
                }
            }
            return orders;
        }
        public List<OrderStatus> GetOrderStatuses()
        {
            List<OrderStatus> orderStatuses = new List<OrderStatus>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_OrderStatuses";
                SqlCommand command = new SqlCommand(textCommand, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OrderStatus orderStatus = new OrderStatus
                        {
                            OrderStatusId = Convert.ToInt32(reader["OrderStatusId"].ToString()),
                            OrderStatusName = reader["OrderStatusName"].ToString(),
                        };

                        orderStatuses.Add(orderStatus);
                    }
                    connection.Close();
                }
            }
            return orderStatuses;
        }
    }
}
