using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RadencyTask2_1;
using RadencyTask2_1.Meta.Models;
using RadencyTask2_1.PathConfig;
using RadencyTask2_1.PaymentTransactions;
using RadencyTask2_1.PaymentTransactions.Services;
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

        var systemWatcher = builderServiceProvider.GetRequiredService<SystemWatcher>();
        var dataProcessing = builderServiceProvider.GetRequiredService<DataProcessing>();
        var appSettings= builderServiceProvider.GetRequiredService<IOptions<AppSettings>>();


        var filesList = Directory.GetFiles(appSettings.Value.ToRead);
        dataProcessing.Process(filesList);
        systemWatcher.ConfigurationWatcher();
       


    }

   
}
