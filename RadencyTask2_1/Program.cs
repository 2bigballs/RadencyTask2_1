using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RadencyTask2_1;
using RadencyTask2_1.PathConfig;
using RadencyTask2_1.PaymentTransactions;
using RadencyTask2_1.ReadFiles;
using RadencyTask2_1.ReadFiles.Interfaces;
using RadencyTask2_1.ReadFiles.Services;
using RadencyTask2_1.SummoryReport.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var startup = new Startup();
        var builderServiceProvider = startup.Init();
        var appSettings = builderServiceProvider.GetRequiredService<IOptions<AppSettings>>();
        var readFileCreator = builderServiceProvider.GetRequiredService<ReadFileCreator>();
        var paymentTransactionService = builderServiceProvider.GetRequiredService<PaymentTransactionService>();
        var summoryReportService = builderServiceProvider.GetRequiredService<SummoryReportService>();

        var filesList = Directory.GetFiles(appSettings.Value.ToRead).GroupBy(x => Path.GetExtension(x));
        foreach (var file in filesList)
        {
            var readService = readFileCreator.CreateReadService(file.Key);
            if (readService == null)
            {
                continue;
            }
            var subRawPaymentTransactions = readService.ReadFiles(file);
            var getPaymentTransaction = paymentTransactionService.GetPaymentTransaction(subRawPaymentTransactions);
            var dataForReport = summoryReportService.GetDataForReport(getPaymentTransaction);
            summoryReportService.Create(dataForReport);
        }

    }
}
