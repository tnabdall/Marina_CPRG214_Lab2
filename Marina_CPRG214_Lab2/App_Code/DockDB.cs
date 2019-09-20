using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    [DataObject(true)]
    public class DockDB
    {
        /// <summary>
        /// Gets all docks in Marina
        /// </summary>
        /// <returns>All docks in Marina</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Dock> GetDocks()
        {
            List<Dock> docks = new List<Dock>(); // Empty list

            SqlConnection connection = MarinaDB.GetConnection();

            // Query to get all dock info
            string query = "SELECT * " + 
                "FROM Dock ; " ;

            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                // Add every found dock to list
                docks.Add(new Dock(
                    (int)reader["ID"],
                    reader["Name"].ToString(),
                    ((bool)reader["WaterService"]),
                    ((bool)reader["ElectricalService"])
                        ));
            }
            reader.Close();
            return docks;
        }

    }
}