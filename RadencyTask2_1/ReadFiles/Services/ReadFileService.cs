using Microsoft.Extensions.Options;
using RadencyTask2_1.PathConfig;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadencyTask2_1.PaymentTransactions;
using RadencyTask2_1.ReadFiles.Models;
using RadencyTask2_1.SummoryReport.Services;

namespace RadencyTask2_1.ReadFiles.Services
{
    public class ReadFileService
    {
        private readonly ReadFileCreator _readFileCreator;
        private readonly AppSettings _appSettings;
        private readonly PaymentTransactionService _paymentTransactionService;
        private readonly SummoryReportService _summoryReportService;
   

        public ReadFileService(ReadFileCreator readFileCreator, IOptions<AppSettings> appSettings, PaymentTransactionService paymentTransactionService, SummoryReportService summoryReportService)
        {
            _readFileCreator = readFileCreator;
            _paymentTransactionService = paymentTransactionService;
            _summoryReportService = summoryReportService;
            _appSettings = appSettings.Value;
        }

        //if nothing do, so we can manage this to Dictionary<string,List<>>
        public void ReadFiles()
        {

            var filesList = Directory.GetFiles(_appSettings.ToRead).GroupBy(x => Path.GetExtension(x));
            foreach (var file in filesList)
            {
                var readService = _readFileCreator.CreateReadService(file.Key);
                if (readService == null)
                {
                    continue;
                }
                
                var subRawPaymentTransactions = readService.ReadFiles(file);
                var getPaymentTransaction= _paymentTransactionService.GetPaymentTransaction(subRawPaymentTransactions);
                var dataForReport=_summoryReportService.GetDataForReport(getPaymentTransaction);
                _summoryReportService.Create(dataForReport);

            }
        }

        
    }

}
