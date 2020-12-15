using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BusinessSuiteByVesune.CRUD
{
    public class ItemCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BusinessSuiteByVesune.Properties.Settings.BusinessSuiteVesuneConnectionString"].ToString();
        public int Create(Item item)
        {
            int result = 0;
            string textCommand = "INSERT INTO tbl_Items (Name, SkuNumber, ItemTypeId, LocationTypeId, Price, Notes, Description, Quantity) OUTPUT INSERTED.ItemId VALUES (@Name, @SkuNumber, @ItemTypeId, @LocationTypeId, @Price, @Notes, @Description, @Quantity)";
            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@SkuNumber", item.SkuNumber);
                command.Parameters.AddWithValue("@ItemTypeId", item.ItemTypeId);
                command.Parameters.AddWithValue("@LocationTypeId", item.LocationTypeId);
                command.Parameters.AddWithValue("@Price", item.Price);
                command.Parameters.AddWithValue("@Notes", item.Notes);
                command.Parameters.AddWithValue("@Description", item.Description);
                command.Parameters.AddWithValue("@Quantity", item.Quantity);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["ItemId"].ToString());
                    }
                    connection.Close();
                }
            }
            return result;
        }
        public Item Read(int itemId)
        {
            Item item = new Item();
            string textCommand = "SELECT * FROM tbl_Items WHERE ItemId = @ItemId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);
                
                command.Parameters.AddWithValue("@ItemId", itemId);
                
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item.ItemId = Convert.ToInt32(reader["ItemId"].ToString());
                        item.Name = reader["Name"].ToString();
                        item.SkuNumber = reader["SkuNumber"].ToString();
                        item.ItemTypeId = Convert.ToInt32(reader["ItemTypeId"].ToString());
                        item.LocationTypeId = Convert.ToInt32(reader["LocationTypeId"].ToString());
                        item.Price = Convert.ToDecimal(reader["Price"].ToString());
                        item.Notes = reader["Notes"].ToString();
                        item.Description = reader["Description"].ToString();
                        item.Quantity = Convert.ToInt32(reader["Quantity"]);

                    }
                    connection.Close();
                }
            }
            return item;
        }
        public int Update(Item item)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_Items SET Name = @Name, SkuNumber = @SkuNumber, ItemTypeId = @ItemTypeId, LocationTypeId = @LocationTypeId, Price = @Price, Notes = @Notes, Quantity = @Quantity, Description = @Description WHERE ItemId = @ItemId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ItemId", item.ItemId);
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@SkuNumber", item.SkuNumber);
                command.Parameters.AddWithValue("@ItemTypeId", item.ItemTypeId);
                command.Parameters.AddWithValue("@LocationTypeId", item.LocationTypeId);
                command.Parameters.AddWithValue("@Price", item.Price);
                command.Parameters.AddWithValue("@Notes", item.Notes);
                command.Parameters.AddWithValue("@Quantity", item.Quantity);
                command.Parameters.AddWithValue("@Description", item.Description);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public int Destroy(int itemId)
        {
            int result = 0;
            string textCommand = "DELETE FROM tbl_Items WHERE ItemId = @ItemId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);
                
                command.Parameters.AddWithValue("@ItemId", itemId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public List<Item> GetAllItems()
        {
            List<Item> inventory = new List<Item>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_Items";
                SqlCommand command = new SqlCommand(textCommand, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Item item = new Item
                        {
                            ItemId = Convert.ToInt32(reader["ItemId"].ToString()),
                            Name = reader["Name"].ToString(),
                            SkuNumber = reader["SkuNumber"].ToString(),
                            ItemTypeId = Convert.ToInt32(reader["ItemTypeId"].ToString()),
                            LocationTypeId = Convert.ToInt32(reader["LocationTypeId"].ToString()),
                            Price = Convert.ToDecimal(reader["Price"].ToString()),
                            Notes = reader["Notes"].ToString(),
                            Description = reader["Description"].ToString(),
                            Quantity = Convert.ToInt32(reader["Quantity"].ToString())
                        };

                        inventory.Add(item);
                    }
                    connection.Close();
                }
            }
            return inventory;
        }
    }
}
