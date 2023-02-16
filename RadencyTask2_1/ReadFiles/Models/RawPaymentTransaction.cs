using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
