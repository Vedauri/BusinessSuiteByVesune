using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BusinessSuiteByVesune.CRUD
{
    public class ItemTypeCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BusinessSuiteByVesune.Properties.Settings.BusinessSuiteVesuneConnectionString"].ToString();

        public int Create(ItemType itemType)
        {
            int result = 0;
            string textCommand = "INSERT INTO tbl_ItemTypes (ItemTypeName) OUTPUT INSERTED.ItemTypeId VALUES (@ItemTypeName)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ItemTypeName", itemType.ItemTypeName);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["ItemTypeId"].ToString());
                    }
                    connection.Close();
                }
            }
            return result;
        }
        public ItemType Read(int itemTypeId)
        {
            ItemType itemType = new ItemType();
            string textCommand = "SELECT * FROM tbl_ItemTypes WHERE ItemTypeId = @ItemTypeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ItemTypeId", itemTypeId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itemType.ItemTypeId = Convert.ToInt32(reader["ItemTypeId"].ToString());
                        itemType.ItemTypeName = reader["ItemTypeName"].ToString();
                    }
                    connection.Close();
                }
            }
            return itemType;
        }
        public int Update(ItemType itemType)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_ItemTypes SET ItemTypeName = @ItemTypeName WHERE ItemTypeId = @ItemTypeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ItemTypeId", itemType.ItemTypeId);
                command.Parameters.AddWithValue("@ItemTypeName", itemType.ItemTypeName);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public int Destroy(int itemTypeId)
        {
            int result = 0;
            string textCommand = "DELETE FROM tbl_ItemTypes WHERE ItemTypeId = @ItemTypeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ItemTypeId", itemTypeId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public List<ItemType> GetItemTypes()
        {
            List<ItemType> itemTypes = new List<ItemType>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_ItemTypes";
                SqlCommand command = new SqlCommand(textCommand, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ItemType itemType = new ItemType
                        {
                            ItemTypeId = Convert.ToInt32(reader["ItemTypeId"].ToString()),
                            ItemTypeName = reader["ItemTypeName"].ToString()
                        };

                        itemTypes.Add(itemType);
                    }
                    connection.Close();
                }
            }
            return itemTypes;
        }
    }
}
