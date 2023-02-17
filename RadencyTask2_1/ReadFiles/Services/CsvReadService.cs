using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.Extensions.Options;
using RadencyTask2_1.PathConfig;
using RadencyTask2_1.ReadFiles.Interfaces;
using System.Globalization;
using RadencyTask2_1.ReadFiles.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using RadencyTask2_1.Meta.Models;

namespace RadencyTask2_1.ReadFiles.Services
{
    public class CsvReadService : ReadServiceWithMeta, IReadService
    {
        public CsvReadService(MetaModel metaModel) : base(metaModel)
        {
        }
        public string ExtensionType => ".csv";
        public List<RawPaymentTransaction> ReadFile(string file)
        {
            var rawPaymentTransactions = ConvertToPaymentTransaction(file);
            return rawPaymentTransactions;
        }

        private List<RawPaymentTransaction> ConvertToPaymentTransaction(string file)
        {
            List<RawPaymentTransaction> rawPaymentTransactions = new();
            using TextFieldParser csvParser = new TextFieldParser(file);
            csvParser.CommentTokens = new string[] { "#" };
            csvParser.SetDelimiters(new string[] { "," });
            csvParser.HasFieldsEnclosedInQuotes = true;
            csvParser.TrimWhiteSpace = true;

            var numberLineOfHeader = 2;
            while (!csvParser.EndOfData)
            {
                var rawPaymentTransactionProperties = csvParser.ReadFields().ToList();
                if (csvParser.LineNumber == numberLineOfHeader)
                {
                    continue;
                }

                var address = rawPaymentTransactionProperties.ElementAtOrDefault(2);
                var rawPaymentTransaction = CreateRawPaymentTransaction(rawPaymentTransactionProperties, address);
                rawPaymentTransactions.Add(rawPaymentTransaction);
            }

            return rawPaymentTransactions;
        }


    }
}
