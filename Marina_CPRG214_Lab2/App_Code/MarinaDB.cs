using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    public static class MarinaDB
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=Marina;Integrated Security=True";
            return new SqlConnection(connectionString);
        }
    }
}