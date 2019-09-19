using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Username { get; set; }

        public Customer(int customerId, string firstName, string lastName, string phone, string city)
        {
            CustomerId = customerId;
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));
            City = city ?? throw new ArgumentNullException(nameof(city));
        }

        public Customer(int customerId, string firstName, string lastName, string phone, string city, string username) : this(customerId, firstName, lastName, phone, city)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }
    }
}