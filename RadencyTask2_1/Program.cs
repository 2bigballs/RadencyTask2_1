using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RadencyTask2_1;
using RadencyTask2_1.Meta.Services;
using RadencyTask2_1.PathConfig;


public class Program
{
    public static void Main(string[] args)
    {
        var startup = new Startup();
        var builderServiceProvider = startup.Init();

        var systemWatcher = builderServiceProvider.GetRequiredService<SystemWatcher>();
        var dataProcessing = builderServiceProvider.GetRequiredService<DataProcessing>();
        var appSettings= builderServiceProvider.GetRequiredService<IOptions<AppSettings>>();
        var metaService = builderServiceProvider.GetRequiredService<MetaService>();

        var filesList = Directory.GetFiles(appSettings.Value.ToRead);
        dataProcessing.Process(filesList);
        systemWatcher.ConfigurationWatcher();
        metaService.Write();



    }

   
}
