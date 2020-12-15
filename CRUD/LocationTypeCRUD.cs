using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BusinessSuiteByVesune.CRUD
{
    public class LocationTypeCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["BusinessSuiteByVesune.Properties.Settings.BusinessSuiteVesuneConnectionString"].ToString();

        public int Create(LocationType locationType)
        {
            int result = 0;
            string textCommand = "INSERT INTO tbl_LocationTypes (LocationTypeName) OUTPUT INSERTED.LocationTypeId VALUES (@LocationTypeName)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@LocationTypeName", locationType.LocationTypeName);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["LocationTypeId"].ToString());
                    }
                    connection.Close();
                }
            }
            return result;
        }
        public LocationType Read(int locationTypeId)
        {
            LocationType locationType = new LocationType();
            string textCommand = "SELECT * FROM tbl_LocationTypes WHERE LocationTypeId = @LocationTypeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@LocationTypeId", locationTypeId);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        locationType.LocationTypeId = Convert.ToInt32(reader["LocationTypeId"].ToString());
                        locationType.LocationTypeName = reader["LocationTypeName"].ToString();
                    }
                    connection.Close();
                }
            }
            return locationType;
        }
        public int Update(LocationType locationType)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_LocationTypes SET LocationTypeName = @LocationTypeName WHERE LocationTypeId = @LocationTypeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@LocationTypeName", locationType.LocationTypeName);
                command.Parameters.AddWithValue("@LocationTypeId", locationType.LocationTypeId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public int Destroy(int locationTypeId)
        {
            int result = 0;
            string textCommand = "DELETE FROM tbl_LocationTypes WHERE LocationTypeId = @LocationTypeId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@LocationTypeId", locationTypeId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public List<LocationType> GetLocationTypes()
        {
            List<LocationType> locationTypes = new List<LocationType>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string textCommand = "SELECT * FROM tbl_LocationTypes";
                SqlCommand command = new SqlCommand(textCommand, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LocationType locationType = new LocationType
                        {
                            LocationTypeId = Convert.ToInt32(reader["LocationTypeId"].ToString()),
                            LocationTypeName = reader["LocationTypeName"].ToString()
                        };

                        locationTypes.Add(locationType);
                    }
                    connection.Close();
                }
            }
            return locationTypes;
        }
    }
}
