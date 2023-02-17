using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using RadencyTask2_1.PaymentTransactions.Models;
using RadencyTask2_1.ReadFiles.Models;
using RadencyTask2_1.ReadFiles.Services;

namespace RadencyTask2_1.PaymentTransactions
{
    public class PaymentTransactionService
    {
        private readonly ReadFileService _readFileService;

        public PaymentTransactionService(ReadFileService readFileService)
        {
            _readFileService = readFileService;
        }

        public List<PaymentTransaction> GetPaymentTransaction()
        {

            var rawPaymentTransactions = _readFileService.ReadFiles();

            var validRawPaymentTransactions = rawPaymentTransactions.Where(x => x.Validate()).ToList();
            //rawPaymentTransactions.Count-validRawPaymentTransactions.Count=count of error ------- realize THIS don't forget 
            List<PaymentTransaction> paymentTransactions = validRawPaymentTransactions.Select(x => x.ConvertToPaymentTransaction()).ToList();
            return paymentTransactions;

        }

        public List<SummoryPayment> SummoryPaymentReport(List<PaymentTransaction> paymentTransactions)
        {
            List<SummoryPayment> summoryPayments = new();

            var groupByCity = paymentTransactions.GroupBy(x => x.Address.City)
                .Select(x=>new {x.Key, Services = x.GroupBy(y => y.Service),Total=x.Sum(y=>y.Payment)});

            foreach (var groupPayment in groupByCity)
            {

                SummoryPayment summoryPayment = new()
                {
                    City = groupPayment.Key,
                    Services = CreateServices(groupPayment.Services),
                    Total = groupPayment.Total

                };

                summoryPayments.Add(summoryPayment);
            }

            return summoryPayments;
        }

        private IEnumerable<Services> CreateServices(IEnumerable<IGrouping<string, PaymentTransaction>>  groupPayment)
        {
            
            List<Services> services = new();
            foreach (var groupPaymentByService in groupPayment)
            {
                Services service = new()
                {
                    Name = groupPaymentByService.Key,
                    Payers = CreatePayers(groupPaymentByService),
                    Total = groupPaymentByService.Sum(x => x.Payment)
                };
                services.Add(service);
            }

            return services;
        }

        private IEnumerable<Payers> CreatePayers(IEnumerable<PaymentTransaction> groupPaymentsByService)
        {

            List<Payers> players = new();
            foreach (var groupPaymentByService in groupPaymentsByService)
            {
                Payers player = new()
                {
                    Name = groupPaymentByService.FirstName + groupPaymentByService.LastName,
                    Payment = groupPaymentByService.Payment,
                    Date = groupPaymentByService.Date,
                    AccountNumber = groupPaymentByService.AccountNumber

                };
                players.Add(player);
            }
            return players;
        }
    }
}
