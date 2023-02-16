using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.Extensions.Options;
using RadencyTask2_1.PathConfig;
using RadencyTask2_1.ReadFiles.Interfaces;
using System.Globalization;
using RadencyTask2_1.Models;
using RadencyTask2_1.ReadFiles.Models;
using Microsoft.VisualBasic.FileIO;
using System;

namespace RadencyTask2_1.ReadFiles.Services
{
    public class CsvReadService : ReadService, IReadService
    {
        private readonly AppSettings _appSettings;
        public CsvReadService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string ExtensionType => ".csv";
        public Task ReadFiles(IEnumerable<string> files)
        {

            foreach (var file in files)
            {
                using (TextFieldParser csvParser = new TextFieldParser(file))
                {
                    csvParser.CommentTokens = new string[] { "#" };
                    csvParser.SetDelimiters(new string[] { "," });
                    csvParser.HasFieldsEnclosedInQuotes = true;
                    csvParser.TrimWhiteSpace = true;


                    while (!csvParser.EndOfData)
                    {
                        var rawPaymentTransactionProperties = csvParser.ReadFields().ToList();
                        if (csvParser.LineNumber == 2)
                        {
                            continue;
                        }
                        var address = rawPaymentTransactionProperties.ElementAtOrDefault(2);
                        var rawPaymentTransaction = CreateRawPaymentTransaction(rawPaymentTransactionProperties, address);
                    }
                }
            }

            return Task.CompletedTask;
        }


    }
}
