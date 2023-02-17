using RadencyTask2_1.Meta.Models;
using RadencyTask2_1.ReadFiles.Interfaces;
using RadencyTask2_1.ReadFiles.Models;

namespace RadencyTask2_1.ReadFiles.Services
{
    public class TxtReadService : ReadServiceWithMeta, IReadService
    {
        public string ExtensionType => ".txt";
        public TxtReadService(MetaModel metaModel) : base(metaModel)
        {
        }

        public List<RawPaymentTransaction> ReadFile(string file)
        {
            var stringArr = File.ReadAllLines(file);

            var rawPaymentTransactions = ConvertToPaymentTransaction(stringArr);

            return rawPaymentTransactions;
        }
         
        private  List<RawPaymentTransaction> ConvertToPaymentTransaction(string[] stringArr)
        {
            List<RawPaymentTransaction> rawPaymentTransactions = new();

            foreach (var line in stringArr)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var addressStartIndex = line.IndexOf("“", StringComparison.Ordinal);
                var addressEndIndex = line.IndexOf("”", StringComparison.Ordinal);
                
                var address = line.Substring(addressStartIndex + 1, addressEndIndex - addressStartIndex - 1);
                var lineWithoutAddress = line.Replace($"“{address}”", "");
                var paymentTransactionPropertyList = lineWithoutAddress.Split(",").ToList();
                var rawPaymentTransaction = CreateRawPaymentTransaction(paymentTransactionPropertyList, address);
                rawPaymentTransactions.Add(rawPaymentTransaction);
            }

            return rawPaymentTransactions;
        }


        
    }
}
