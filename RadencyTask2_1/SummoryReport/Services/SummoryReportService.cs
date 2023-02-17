using RadencyTask2_1.PaymentTransactions.Models;
using RadencyTask2_1.SummoryReport.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using RadencyTask2_1.Meta.Models;
using RadencyTask2_1.PathConfig;


namespace RadencyTask2_1.SummoryReport.Services
{
    public class SummoryReportService
    {

        private readonly AppSettings _appSettings;
        private readonly MetaModel _metaModel;
        public SummoryReportService(IOptions<AppSettings> appSettings, MetaModel metaModel)
        {
            _metaModel = metaModel;
            _appSettings = appSettings.Value;
        }
        public List<SummoryPayment> GetDataForReport(List<PaymentTransaction> paymentTransactions)
        {
            List<SummoryPayment> summoryPayments = new();

            var groupByCity = paymentTransactions.GroupBy(x => x.Address.City)
                .Select(x => new { x.Key, Services = x.GroupBy(y => y.Service), Total = x.Sum(y => y.Payment) });

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

        private IEnumerable<Service> CreateServices(IEnumerable<IGrouping<string, PaymentTransaction>> groupPayment)
        {

            List<Service> services = new();
            foreach (var groupPaymentByService in groupPayment)
            {
                Service service = new()
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

        public void Create(List<SummoryPayment> SummoryPaymentReport)
        {
            var jsonString = JsonSerializer.Serialize(SummoryPaymentReport);

            var path = Path.Combine(_appSettings.ToWrite, DateTime.Now.ToShortDateString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string jsonName = $"output{_metaModel.ParsedFiles}.json";
            var pathJSON = Path.Combine(path, jsonName);
            File.WriteAllText(pathJSON, jsonString);
        }

    }
}
