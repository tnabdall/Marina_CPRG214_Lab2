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
        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Dock> GetDocks()
        {
            List<Dock> docks = new List<Dock>();

            SqlConnection connection = MarinaDB.GetConnection();

            string query = "SELECT * " +
                "FROM Dock ; " ;

            SqlCommand cmd = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
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