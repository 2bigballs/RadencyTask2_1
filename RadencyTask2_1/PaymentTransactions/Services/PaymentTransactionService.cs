using RadencyTask2_1.PaymentTransactions.Models;
using RadencyTask2_1.ReadFiles.Models;

namespace RadencyTask2_1.PaymentTransactions.Services
{
    public interface IPaymentTransactionService
    {
        List<PaymentTransaction> GetPaymentTransaction(List<RawPaymentTransaction> rawPaymentTransactions);

    }
    public class PaymentTransactionService : IPaymentTransactionService
    {
        public virtual List<PaymentTransaction> GetPaymentTransaction(List<RawPaymentTransaction> rawPaymentTransactions)
        {

            var validRawPaymentTransactions = rawPaymentTransactions.Where(x => x.Validate()).ToList();
            List<PaymentTransaction> paymentTransactions = validRawPaymentTransactions.Select(x => x.ConvertToPaymentTransaction()).ToList();
            return paymentTransactions;

        }

    }

   
}
