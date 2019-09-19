using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    [DataObject(true)]
    public static class CustomerDB
    {
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static void AddCustomer(Customer customer)
        {

        }

        public static Customer VerifyLogin(String username, String password)
        {

            SqlConnection connection = MarinaDB.GetConnection();

            Customer customer;
            string savedPasswordHash;
            byte[] salt;

            string query = "SELECT ID, FirstName, LastName, Phone, City, Username, Password, Salt FROM Customer " +
                "WHERE Username = @Username ; ";

            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();

            cmd.Parameters.AddWithValue("@Username", username);
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);
            if (reader.HasRows)
            {
                reader.Read();
                savedPasswordHash = reader["Password"].ToString();
                salt = Convert.FromBase64String(reader["Salt"].ToString());
                customer = new Customer(
                    (int)reader["ID"],
                    reader["FirstName"].ToString(),
                    reader["LastName"].ToString(),
                    reader["Phone"].ToString(),
                    reader["City"].ToString(),
                    reader["Username"].ToString()
                    );
            }
            else
            {
                return null;
            }

            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return null;

            return customer;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static bool RegisterCustomer(Customer customer, String password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            // Check if customer first and last name exists in DB
            bool registerExistingCustomer = false;
            SqlConnection connection = MarinaDB.GetConnection();

            string testUserExistsQuery = "SELECT Username FROM Customer " +
                "WHERE FirstName=@FirstName AND LastName=@LastName AND Username IS NULL ";

            SqlCommand cmd = new SqlCommand(testUserExistsQuery, connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customer.LastName);
            cmd.Parameters.AddWithValue("@Username", customer.Username);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {                
                reader.Read();
                string user = reader["Username"] as string;
                // Trying to register an existing user
                if (user != null)
                {
                    return false;
                }
                // Run query to add user and login to register existing customer
                registerExistingCustomer = true;
                reader.Close();
            }
            connection.Close();

            connection = MarinaDB.GetConnection();
            connection.Open();

            if (registerExistingCustomer) // Update existing customer
            {
                string query = "Update Customer SET Phone = @Phone, City = @City, Username = @Username, Password = @Password, Salt = @Salt WHERE " +
                    "FirstName=@FirstName AND LastName=@LastName ; ";

                SqlCommand cmd2 = new SqlCommand(query, connection);

                cmd2.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd2.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd2.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd2.Parameters.AddWithValue("@City", customer.City);
                cmd2.Parameters.AddWithValue("@Username", customer.Username);
                cmd2.Parameters.AddWithValue("@Password", savedPasswordHash);
                cmd2.Parameters.AddWithValue("@Salt", Convert.ToBase64String(salt));

                cmd2.ExecuteNonQuery();
            }
            else // Register new customer
            {

                string query = "INSERT INTO Customer (FirstName, LastName, Phone, City, Username, Password, Salt) VALUES " +
                    "(@FirstName,@LastName,@Phone,@City,@Username,@Password,@Salt) ; ";

                SqlCommand cmd2 = new SqlCommand(query, connection);

                cmd2.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd2.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd2.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd2.Parameters.AddWithValue("@City", customer.City);
                cmd2.Parameters.AddWithValue("@Username", customer.Username);
                cmd2.Parameters.AddWithValue("@Password", savedPasswordHash);
                cmd2.Parameters.AddWithValue("@Salt", Convert.ToBase64String(salt));

                cmd2.ExecuteNonQuery();
            }

            connection.Close();
            return true;
            
        }
    }
}