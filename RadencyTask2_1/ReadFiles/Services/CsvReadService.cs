using Microsoft.Extensions.Options;
using RadencyTask2_1.PathConfig;
using RadencyTask2_1.ReadFiles.Interfaces;

namespace RadencyTask2_1.ReadFiles.Services
{
    public class CsvReadService : IReadService
    {
        private readonly AppSettings _appSettings;
        public CsvReadService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string ExtensionType => ".csv";
        public Task ReadFiles(IEnumerable<string> files)
        {
            return Task.CompletedTask;
        }

      
    }
}
