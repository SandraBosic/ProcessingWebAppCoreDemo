using System;

namespace Contracts.ViewModels.RecurringBatches
{
    public class RecurringBatchTransactionDataViewModel
    {
        public Guid TransactionId { get; set; }
        public string ReferenceNumber { get; set; }
        public string AuthorizationCode { get; set; }
        public string TransId { get; set; }
        public string CardNumber { get; set; }
        public string NewCardNumber { get; set; }
        public decimal Amount { get; set; }
        public Guid VirtualAccountNumber { get; set; }
        public Guid UpdatedVirtualAccountNumber { get; set; }
    }
}
