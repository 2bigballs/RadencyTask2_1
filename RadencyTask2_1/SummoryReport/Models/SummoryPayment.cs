using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadencyTask2_1.SummoryReport.Models
{
    public class SummoryPayment
    {
        public string City { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public decimal Total { get; set; }

    }
    public class Service
    {
        public string Name { get; set; }
        public IEnumerable<Payers> Payers { get; set; }

        public decimal Total { get; set; }
    }
    public class Payers
    {
        public string Name { get; set; }
        public decimal Payment { get; set; }
        public DateTime Date { get; set; }
        public long AccountNumber { get; set; }
    }
}
