using System.Globalization;
using Microsoft.Extensions.Options;
using RadencyTask2_1.Models;
using RadencyTask2_1.PathConfig;
using RadencyTask2_1.ReadFiles.Interfaces;
using RadencyTask2_1.ReadFiles.Models;

namespace RadencyTask2_1.ReadFiles.Services
{
    public class TxtReadService : ReadService, IReadService
    {
        public string ExtensionType => ".txt";

        public List<RawPaymentTransaction> ReadFiles(IEnumerable<string> files)
        {
            List<RawPaymentTransaction> rawPaymentTransactionList = new();


            foreach (var file in files)
            {
                var stringArr = File.ReadAllLines(file);

                var rawPaymentTransactions = ConvertToPaymentTransaction(stringArr);

                rawPaymentTransactionList.AddRange(rawPaymentTransactions);
            }

            return rawPaymentTransactionList;
        }

        private  List<RawPaymentTransaction> ConvertToPaymentTransaction(string[] stringArr)
        {
            List<RawPaymentTransaction> rawPaymentTransactions = new();

            foreach (var line in stringArr)
            {
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
