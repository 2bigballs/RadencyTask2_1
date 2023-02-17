using RadencyTask2_1.Meta.Models;
using RadencyTask2_1.ReadFiles.Models;

namespace RadencyTask2_1.ReadFiles.Services
{
    public abstract class ReadServiceWithMeta : ReadService
    {
        private readonly MetaModel _metaModel;
        public ReadServiceWithMeta(MetaModel metaModel)
        {
            _metaModel = metaModel;
        }
        protected override RawPaymentTransaction CreateRawPaymentTransaction(List<string> paymentTransactionPropertyList, string address)
        {
            _metaModel.ParsedLines++;
            return base.CreateRawPaymentTransaction(paymentTransactionPropertyList, address);
        }

    }
}
