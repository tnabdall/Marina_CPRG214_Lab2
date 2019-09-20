using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    [DataObject(true)]
    public static class CustomerDB
    {
       /// <summary>
       /// Verifies customer login info to gain authentication
       /// </summary>
       /// <param name="username">Username</param>
       /// <param name="password">Unencrypted password</param>
       /// <returns></returns>
        public static Customer VerifyLogin(String username, String password)
        {
            // Creates connection
            SqlConnection connection = MarinaDB.GetConnection();
            // Empty customer
            Customer customer;
            // Holds password hash and salt from DB
            string savedPasswordHash;
            byte[] salt;

            // Grab all parameters from DB
            string query = "SELECT ID, FirstName, LastName, Phone, City, Username, Password, Salt FROM Customer " +
                "WHERE Username = @Username ; ";

            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();

            cmd.Parameters.AddWithValue("@Username", username);
            // Run query
            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow);
            // If username is in DB, retrieve saved password hash and salt and create customer object
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
            else // User doesnt exist, return null
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
                    return null; // If comparison fails, return null

            return customer; // Returns customer
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static bool RegisterCustomer(Customer customer, String password)
        {
            // Allows conversion of first and last name to title case
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            // Makes first and last name title case so we can check with DB properly
            customer.FirstName = textInfo.ToTitleCase(customer.FirstName);
            customer.LastName = textInfo.ToTitleCase(customer.LastName);

            // Generates salt for new customer
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Generates hashedpassword from unencrypted password and salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            // Combines salt with hashed password
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            // Convert to string for DB
            string savedPasswordHash = Convert.ToBase64String(hashBytes);                     

            // Gets connection
            SqlConnection connection = MarinaDB.GetConnection();

            // Test if there is a match for First and Last name in DB. If there is, see if there is no assigned username
            string testUsernameExists = "SELECT Username FROM Customer " +
                "WHERE Username=@Username ; ";


            SqlCommand cmdUser = new SqlCommand(testUsernameExists, connection);
            connection.Open();
            cmdUser.Parameters.AddWithValue("@Username", customer.Username);

            // Run query
            SqlDataReader readerUser = cmdUser.ExecuteReader();
            if (readerUser.HasRows) // There is a customer with same username so end function
            {
                readerUser.Close();
                connection.Close();

                return false;
            }
            readerUser.Close();
            connection.Close();

            // Check if customer first and last name exists in DB
            bool registerExistingCustomer = false;

            connection = MarinaDB.GetConnection();

            // Test if there is a match for First and Last name in DB. If there is, see if there is no assigned username
            string testFirstAndLastNameExists = "SELECT Username FROM Customer " +
                "WHERE FirstName=@FirstName AND LastName=@LastName AND Username IS NULL ; ";

            
            SqlCommand cmd = new SqlCommand(testFirstAndLastNameExists, connection);
            connection.Open();
            cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
            cmd.Parameters.AddWithValue("@LastName", customer.LastName);

            // Run query
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows) // There is a customer with no username but a first and last name match
            {              
                // Username doesnt exist for customer with first and last name. So update matched record.
                registerExistingCustomer = true;               
                                
            }
            reader.Close();
            connection.Close();

            connection = MarinaDB.GetConnection();
            connection.Open();

            if (registerExistingCustomer) // Update existing customer with no username
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