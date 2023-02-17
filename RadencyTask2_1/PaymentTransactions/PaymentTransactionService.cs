using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using RadencyTask2_1.PaymentTransactions.Services;
using RadencyTask2_1.ReadFiles.Models;
using RadencyTask2_1.ReadFiles.Services;

namespace RadencyTask2_1.PaymentTransactions
{
    public class PaymentTransactionService
    {
        private readonly ReadFileService _readFileService;

        public PaymentTransactionService(ReadFileService readFileService)
        {
            _readFileService = readFileService;
        }

        public List<PaymentTransaction> GetPaymentTransaction()
        {

            var rawPaymentTransactions = _readFileService.ReadFiles();

            var validRawPaymentTransactions = rawPaymentTransactions.Where(x => x.Validate()).ToList();
            //rawPaymentTransactions.Count-validRawPaymentTransactions.Count=count of error ------- realize THIS don't forget 
            List<PaymentTransaction> paymentTransactions = validRawPaymentTransactions.Select(x => x.ConvertToPaymentTransaction()).ToList();
            return paymentTransactions;

        }

       


    }
}
