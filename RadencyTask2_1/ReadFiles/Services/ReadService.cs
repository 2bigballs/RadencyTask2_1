using RadencyTask2_1.Meta.Models;
using RadencyTask2_1.ReadFiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadencyTask2_1.ReadFiles.Services
{
    public abstract class ReadService
    {
        
        protected virtual RawPaymentTransaction CreateRawPaymentTransaction(List<string> paymentTransactionPropertyList, string address)
        {
            RawPaymentTransaction rawPaymentTransaction = new();
            try
            {
                rawPaymentTransaction.FirstName = paymentTransactionPropertyList[0];
                rawPaymentTransaction.LastName = paymentTransactionPropertyList[1];
                rawPaymentTransaction.Address = address;
                rawPaymentTransaction.Payment = paymentTransactionPropertyList[3];
                rawPaymentTransaction.Date = paymentTransactionPropertyList[4];
                rawPaymentTransaction.AccountNumber = paymentTransactionPropertyList[5];
                rawPaymentTransaction.Service = paymentTransactionPropertyList[6];
                return rawPaymentTransaction;
            }
            catch
            {
                return rawPaymentTransaction;
            }
        }
    }
}
