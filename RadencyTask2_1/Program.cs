using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RadencyTask2_1.PathConfig;
using RadencyTask2_1.PaymentTransactions;
using RadencyTask2_1.ReadFiles;
using RadencyTask2_1.ReadFiles.Interfaces;
using RadencyTask2_1.ReadFiles.Services;


public class Program
{
    public static void Main(string[] args)
    {

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        IConfiguration config = builder.Build();

        //setup our DI
        var services = new ServiceCollection()
            .AddSingleton<IReadService, TxtReadService>()
            .AddSingleton<IReadService, CsvReadService>()
            .AddSingleton<ReadFileCreator>()
            .AddSingleton<ReadFileService>()
            .AddSingleton<PaymentTransactionService>();


        services.Configure<AppSettings>(config.GetSection(AppSettings.Key));


        var builderServiceProvider = services.BuildServiceProvider();

        var paymentTransactionService = builderServiceProvider.GetRequiredService<PaymentTransactionService>();


        var paymentTransactions = paymentTransactionService.GetPaymentTransaction();
        paymentTransactionService.SummoryPaymentReport(paymentTransactions);

    }
}
