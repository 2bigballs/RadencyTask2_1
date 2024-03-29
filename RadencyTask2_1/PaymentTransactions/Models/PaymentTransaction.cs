﻿
namespace RadencyTask2_1.PaymentTransactions.Models
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
