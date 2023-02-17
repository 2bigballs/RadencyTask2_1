using Microsoft.Extensions.Options;
using RadencyTask2_1.Meta.Models;
using RadencyTask2_1.PathConfig;
using RadencyTask2_1.PaymentTransactions.Services;
using RadencyTask2_1.ReadFiles;
using RadencyTask2_1.SummoryReport.Services;

namespace RadencyTask2_1
{
    public class DataProcessing
    {
        private readonly AppSettings _appSettings;
        private readonly ReadFileCreator _readFileCreator;
        private readonly IPaymentTransactionService _paymentTransactionService;
        private readonly SummoryReportService _summoryReportService;
        private readonly MetaModel _meta;

        public DataProcessing(IOptions<AppSettings> appSettings, 
            ReadFileCreator readFileCreator, IPaymentTransactionService paymentTransactionService,
            SummoryReportService summoryReportService, MetaModel meta)
        {
            _appSettings = appSettings.Value;
            _readFileCreator = readFileCreator;
            _paymentTransactionService = paymentTransactionService;
            _summoryReportService = summoryReportService;
            _meta = meta;
        }

        public void Process(params string[] filesList)
        {
            
            
            foreach (var file in filesList)
            {
                var readService = _readFileCreator.CreateReadService(Path.GetExtension(file));
                if (readService == null)
                {
                    continue;
                }

                Action addInvalidFile = () => _meta.InvalidFiles.Add(file);
                _meta.ChangeErrorHandler += addInvalidFile;
                var subRawPaymentTransactions = readService.ReadFile(file);
                var getPaymentTransaction = _paymentTransactionService.GetPaymentTransaction(subRawPaymentTransactions);
                var dataForReport = _summoryReportService.GetDataForReport(getPaymentTransaction);
                _summoryReportService.Create(dataForReport);
                _meta.ParsedFiles++;

                _meta.ChangeErrorHandler -= addInvalidFile;
            }
        }
    }
}
