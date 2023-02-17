using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadencyTask2_1.PaymentTransactions.Models;

namespace RadencyTask2_1.ReadFiles.Models
{
    public class RawPaymentTransaction
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Payment { get; set; }
        public string Date { get; set; }
        public string AccountNumber { get; set; }
        public string Service { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(FirstName)
                || string.IsNullOrWhiteSpace(LastName)
                || string.IsNullOrWhiteSpace(Address)
                || string.IsNullOrWhiteSpace(Service))
            {
                return false;
            }

            if (!Decimal.TryParse(Payment,NumberStyles.Any, CultureInfo.InvariantCulture, out decimal payment))
            {
                return false;
            }

            var isValidDate = DateTime.TryParseExact(Date.Trim(), "yyyy-dd-MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
            if (!isValidDate)
            {
                return false;
            }

            if (!long.TryParse(AccountNumber, out long accountNumber))
            {
                return false;
            }

            return true;
        }

        public PaymentTransaction ConvertToPaymentTransaction()
        {
            PaymentTransaction paymentTransaction = new()
            {
                FirstName = FirstName,
                LastName = LastName,
                Address = new Address(Address),
                Payment = Decimal.Parse(Payment, CultureInfo.InvariantCulture),
                Date = DateTime.ParseExact(Date.Trim(), "yyyy-dd-MM", CultureInfo.InvariantCulture),
                AccountNumber = long.Parse(AccountNumber),
                Service = Service
            };
            return paymentTransaction;
        }
    }
}
