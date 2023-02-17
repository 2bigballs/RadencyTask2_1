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

            watcher.Created += OnCreated;
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
           
             Task.Run( async () => await ProcessFileWhenAvailable(e.FullPath));
            
        }
        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }

        private async Task ProcessFileWhenAvailable(string path)
        {
            await Task.Run( () =>
            {
                FileInfo fileInfo = new FileInfo(path);
                while (IsFileLocked(fileInfo))
                { }
                _dataProcessing.Process(path);
            });



        }
    }
}
