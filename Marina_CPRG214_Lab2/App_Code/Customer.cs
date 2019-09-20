using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    public class Customer
    {
        public int CustomerId { get; set; } // Customer Id
        public string FirstName { get; set; } // First name
        public string LastName { get; set; } // Last name
        public string Phone { get; set; } // Phone #
        public string City { get; set; } // City they live in
        public string Username { get; set; } // Username

        /// <summary>
        /// Creates a customer
        /// </summary>
        /// <param name="customerId">Id</param>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="phone">Phone #</param>
        /// <param name="city">City</param>
        public Customer(int customerId, string firstName, string lastName, string phone, string city)
        {
            CustomerId = customerId;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            City = city ?? throw new ArgumentNullException(nameof(city));
        }

        /// <summary>
        /// Creates a customer
        /// </summary>
        /// <param name="customerId">Id</param>
        /// <param name="firstName">First name</param>
        /// <param name="lastName">Last name</param>
        /// <param name="phone">Phone #</param>
        /// <param name="city">City</param>
        /// <param name="username">Customer's username</param>
        public Customer(int customerId, string firstName, string lastName, string phone, string city, string username) : this(customerId, firstName, lastName, phone, city)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}