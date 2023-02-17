using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using RadencyTask2_1.PathConfig;

namespace RadencyTask2_1
{
    public class SystemWatcher
    {
        private readonly AppSettings _appSettings;
        private readonly DataProcessing _dataProcessing;
        public SystemWatcher(IOptions<AppSettings> appSettings, DataProcessing dataProcessing)
        {
            _dataProcessing = dataProcessing;
            _appSettings = appSettings.Value;
        }
        public void ConfigurationWatcher()
        {
            using var watcher = new FileSystemWatcher(_appSettings.ToRead);

            watcher.Created+= OnCreated;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            _dataProcessing.Process(e.FullPath);
        }
    }
}
