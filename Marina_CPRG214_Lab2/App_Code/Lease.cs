using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    public class Lease
    {
        public int LeaseId { get; set; }
        public Slip Slip { get; set; }
        public Customer Customer { get; set; }

        public Lease(int leaseId, Slip slip, Customer customer)
        {
            LeaseId = leaseId;
            Slip = slip ?? throw new ArgumentNullException(nameof(slip));
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        }
    }
}