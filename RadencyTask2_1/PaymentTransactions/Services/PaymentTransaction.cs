using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RadencyTask2_1.PaymentTransactions.Services
{
    public class PaymentTransaction
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        public decimal Payment { get; set; }
        public DateTime Date { get; set; }
        public long AccountNumber { get; set; }
        public string Service { get; set; }
    }

    public class Address
    {
        public string City { get; }

        public Address(string address)
        {
            City = address.Split(',').FirstOrDefault();
        }
    }

}
