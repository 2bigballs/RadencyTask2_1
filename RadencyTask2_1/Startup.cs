using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RadencyTask2_1.PathConfig;
using RadencyTask2_1.ReadFiles;
using RadencyTask2_1.ReadFiles.Interfaces;
using RadencyTask2_1.ReadFiles.Services;
using RadencyTask2_1.SummoryReport.Services;
using RadencyTask2_1.Meta.Models;
using RadencyTask2_1.Meta.Services;
using RadencyTask2_1.PaymentTransactions.Services;

namespace RadencyTask2_1
{
    public class Startup
    {
        public IServiceProvider Init()
        {
            var config = Configuration();
            var services = Service(config);
            var builderServiceProvider = services.BuildServiceProvider();
            return builderServiceProvider;
        }

        private IConfiguration Configuration ()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            return config;
        }

        private IServiceCollection Service(IConfiguration config)
        {
            var services = new ServiceCollection()
                .AddSingleton<IReadService, TxtReadService>()
                .AddSingleton<IReadService, CsvReadService>()
                .AddSingleton<ReadFileCreator>()
                .AddSingleton<MetaModel>()
                .AddSingleton<IPaymentTransactionService,PaymentTransactionWithMetaService>()
                .AddSingleton<SummoryReportService>()
                .AddSingleton<SystemWatcher>()
                .AddSingleton<MetaService>()
                .AddSingleton<DataProcessing>();

            services.Configure<AppSettings>(config.GetSection(AppSettings.Key));
            return services;
        }
    }
}
