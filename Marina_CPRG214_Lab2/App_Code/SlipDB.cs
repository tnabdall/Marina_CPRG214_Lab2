using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    [DataObject(true)]
    public static class SlipDB
    {
        /// <summary>
        /// Gets all available slips assigned to a dock
        /// </summary>
        /// <param name="DockId">Dock id</param>
        /// <returns>All slips assigned to dock</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Slip> GetAvailableSlipsByDock(int DockId)
        {
            List<Slip> slips = new List<Slip>(); // Empty list


            SqlConnection connection = MarinaDB.GetConnection();

            // Selects all available slips assigned to dock
            string query = "SELECT s.ID AS SlipId, Width, Length, d.ID AS DockId, Name, WaterService, ElectricalService " +
                "FROM Slip s JOIN Dock d " +
                "ON s.DockId = d.ID " +
                "WHERE s.DockID = @DockId " +
                "AND s.ID NOT IN (SELECT DISTINCT SlipID FROM Lease) ; ";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@DockId", DockId);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                // Creates dock object for slip
               Dock dock =new Dock(
                    (int)reader["DockId"],
                    reader["Name"].ToString(),
                    ((bool)reader["WaterService"]),
                    ((bool)reader["ElectricalService"])
                        );

                // Adds slip to list
                slips.Add(new Slip(
                    (int)reader["SlipId"],
                    (int)reader["Width"],
                    (int)reader["Length"],
                    dock
                        ));
            }
            reader.Close();
            return slips;
        }

        /// <summary>
        /// Gets all available slips for every dock
        /// </summary>
        /// <returns>All available slips</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Slip> GetAllAvailableSlips()
        {
            List<Slip> slips = new List<Slip>(); // Empty list


            SqlConnection connection = MarinaDB.GetConnection();

            // Gets all slips that are available for all docks
            string query = "SELECT s.ID AS SlipId, Width, Length, d.ID AS DockId, Name, WaterService, ElectricalService " +
                "FROM Slip s JOIN Dock d " +
                "ON s.DockId = d.ID " +
                "Where ID NOT IN (SELECT DISTINCT ID FROM Lease) ; ";

            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                // Adds new slip to list
                slips.Add(new Slip(
                    (int)reader["SlipId"],
                    (int)reader["Width"],
                    (int)reader["Length"],
                    new Dock( // Adds dock to slip
                        (int) reader["DockId"],
                        reader["Name"].ToString(),
                        ((int) reader["WaterService"])==1,
                        ((int)reader["ElectricalService"]) == 1
                        )));
            }
            reader.Close();
            return slips;
        }

        
        
    }
}