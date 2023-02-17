using RadencyTask2_1.Meta.Models;
using RadencyTask2_1.PaymentTransactions.Models;
using RadencyTask2_1.ReadFiles.Models;

namespace RadencyTask2_1.PaymentTransactions.Services
{
    public class PaymentTransactionWithMetaService : PaymentTransactionService
    {
        private readonly MetaModel _metaModel;
        public PaymentTransactionWithMetaService(MetaModel metaModel)
        {
            _metaModel = metaModel;
        }

        public override List<PaymentTransaction> GetPaymentTransaction(List<RawPaymentTransaction> rawPaymentTransactions)
        {
            var validRawPaymentTransactions = rawPaymentTransactions.Where(x => !x.Validate()).ToList();
            _metaModel.FoundErrors += validRawPaymentTransactions.Count;
            return base.GetPaymentTransaction(rawPaymentTransactions);
        }
    }
}
