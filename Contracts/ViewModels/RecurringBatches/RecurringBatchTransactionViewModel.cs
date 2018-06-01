using Contracts.Enums;
using Contracts.Filters.RecurringBatches;
using System;
using System.Collections.Generic;

namespace Contracts.ViewModels.RecurringBatches
{
    public class RecurringBatchTransactionViewModel
    {
        public RecurringBatchTransactionViewModel()
        {
            Transactions = new List<RecurringBatchTransactionDataViewModel>();
        }

        public Guid BatchId { get; set; }
        public string MerchantName { get; set; }
        public string MerchantNationalId { get; set; }
        public string MerchantAssignedBatchNumber { get; set; }
        public string AgreementNumber { get; set; }
        public string DbaName { get; set; }
        public BatchStatus BatchStatus { get; set; }
        public DateTime ProcessingStartTime { get; set; }
        public DateTime ProcessingEndTime { get; set; }
        public TransactionFilter TransactionFilter { get; set; }
        public List<RecurringBatchTransactionDataViewModel> Transactions { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}
