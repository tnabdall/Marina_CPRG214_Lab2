using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    [DataObject(true)]
    public static class LeaseDB
    {
        /// <summary>
        /// Gets all available leases by customer id
        /// </summary>
        /// <param name="CustomerId">Customer id</param>
        /// <returns>Leases that customer has leased</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Lease> GetAvailableLeasesByCustomer(int CustomerId)
        {
            List<Lease> leases = new List<Lease>(); // Empty list

            SqlConnection connection = MarinaDB.GetConnection();

            // Gets all information to build the leases. Need to build a slip, dock, and customer to build a lease object
            string query = "SELECT l.ID AS LeaseId, s.ID AS SlipId, s.Width, s.Length, d.ID AS DockId, d.Name, d.WaterService, d.ElectricalService, c.ID AS CustomerId, c.FirstName, c.LastName, c.Phone, c.City   " +
                "FROM Lease l JOIN Slip s " +
                "ON l.SlipId = s.ID " +
                "JOIN Customer c " +
                "ON l.CustomerID = c.ID " +
                "JOIN Dock d " +
                "ON s.DockId = d.ID " +
                "WHERE c.ID = @CustomerId ; ";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                // Create dock (for slip) , slip, and customer assigned to lease
                Dock dock = new Dock(
                    (int)reader["DockId"],
                    reader["Name"].ToString(),
                    ((int)reader["WaterService"]) == 1,
                    ((int)reader["ElectricalService"]) == 1);
                Slip slip = new Slip(
                    (int)reader["SlipId"],
                    (int)reader["Width"],
                    (int)reader["Length"],
                    dock);
                
                Customer newcustomer = new Customer(
                    (int)reader["CustomerId"],
                    reader["FirstName"].ToString(),
                    reader["LastName"].ToString(),
                    reader["Phone"].ToString(),
                    reader["City"].ToString());
                    
                // Add new lease to list
                leases.Add(new Lease((int) reader["LeaseId"], slip, newcustomer));
            }
            reader.Close();
            return leases;
        }

        /// <summary>
        /// Leases a new slip for the customer
        /// </summary>
        /// <param name="customerId">Id of customer to lease slip</param>
        /// <param name="slipId">Id of slip to lease</param>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void LeaseSlip(int customerId, int slipId)
        {                           

            SqlConnection connection = MarinaDB.GetConnection();
            connection.Open();


            string query = "INSERT INTO Lease (SlipId, CustomerId) VALUES " +
            "(@SlipId, @CustomerId) ; ";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@SlipId", slipId);
            cmd.Parameters.AddWithValue("@CustomerId", customerId);

            cmd.ExecuteNonQuery();
            
        }

        /// <summary>
        /// Gets slips assigned to customer id
        /// </summary>
        /// <param name="CustomerId">Customer id</param>
        /// <returns>List of slips that customer has leased</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Slip> GetLeasesByCustomerId(int CustomerId)
        {
            List<Slip> slips = new List<Slip>(); // EMpty list

            if (CustomerId == -1) // If customer id is invalid, return empty list
            {
                return slips;
            }

            SqlConnection connection = MarinaDB.GetConnection();

            // Gets all slips associated with customer (through lease table)
            string query = "SELECT s.ID AS SlipId, Width, Length " +
                "FROM Lease l JOIN Slip s " +
                "ON l.SlipId = s.ID " +
                "WHERE l.CustomerId = @CustomerId ; " ;

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@CustomerId", CustomerId);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {                
                // Populates list
                slips.Add(new Slip(
                    (int)reader["SlipId"],
                    (int)reader["Width"],
                    (int)reader["Length"]
                        ));
            }
            reader.Close();
            return slips;
        }

    }
}