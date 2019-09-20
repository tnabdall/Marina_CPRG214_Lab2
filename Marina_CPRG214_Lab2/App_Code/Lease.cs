using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marina_CPRG214_Lab2.App_Code
{
    public class Lease
    {
        public int LeaseId { get; set; } // Id of lease
        public Slip Slip { get; set; } // Slip assigned to lease
        public Customer Customer { get; set; } // Customer who bought lease

        /// <summary>
        /// Represents a lease.
        /// </summary>
        /// <param name="leaseId">Lease id</param>
        /// <param name="slip">Slip assigned to customer</param>
        /// <param name="customer">Customer who leased slip</param>
        public Lease(int leaseId, Slip slip, Customer customer)
        {
            LeaseId = leaseId;
            Slip = slip ?? throw new ArgumentNullException(nameof(slip));
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        }
    }
}