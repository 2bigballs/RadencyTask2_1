using RadencyTask2_1.ReadFiles.Models;

namespace RadencyTask2_1.ReadFiles.Interfaces
{
    public interface IReadService
    {
        public string ExtensionType { get; }
        List<RawPaymentTransaction> ReadFile(string files);

    }
}
