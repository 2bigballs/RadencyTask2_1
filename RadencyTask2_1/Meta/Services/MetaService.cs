using System.Text;
using RadencyTask2_1.Meta.Models;
using RadencyTask2_1.PathConfig;
using Microsoft.Extensions.Options;

namespace RadencyTask2_1.Meta.Services
{
    public class MetaService
    {
        private readonly AppSettings _appSettings;
        private readonly MetaModel _meta;
    
        public MetaService(IOptions<AppSettings> appSettings, MetaModel meta)
        {
            _meta = meta;
            _appSettings = appSettings.Value;
        }
        public void Write()
        {
            var path = Path.Combine(_appSettings.ToWrite, DateTime.Now.ToShortDateString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string metaName = $"meta.log";
            var pathMeta = Path.Combine(path, metaName);
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"parsed_files:{_meta.ParsedFiles}");
            stringBuilder.AppendLine($"parsed_lines:{_meta.ParsedLines}");
            stringBuilder.AppendLine($"found_errors:{_meta.FoundErrors}");
            stringBuilder.AppendLine($"found_errors:{string.Join(',',_meta.InvalidFiles)}");
            File.WriteAllText(pathMeta, stringBuilder.ToString());
        }

        
    }
}
