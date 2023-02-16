using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadencyTask2_1.ReadFiles.Interfaces;

namespace RadencyTask2_1.ReadFiles
{
    public class ReadFileCreator
    {

        private readonly IEnumerable<IReadService> _readServices;

        public ReadFileCreator(IEnumerable<IReadService> readServices)
        {
            _readServices = readServices ?? throw new ArgumentNullException(nameof(readServices));
        }

        public IReadService CreateReadService(string extensionFile)
        {
            var readServise = _readServices
                .FirstOrDefault(service => service.ExtensionType == extensionFile);
            return readServise;
        }

    }
}
