using Contracts.Enums;
using System;
using System.Collections.Generic;

namespace Contracts.ViewModels.RecurringBatches
{
    public class RecurringBatchMetadataViewModel
    {
        public RecurringBatchMetadataViewModel()
        {
            ResponseCodeSummary = new List<ResponseCodeViewModel>();
        }
        public Guid BatchId { get; set; }
        public string MerchantName { get; set; }
        public string BatchNumber { get; set; }
        public int TotalNumberOfTransactions { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountOfApprovedTransactions { get; set; }
        public decimal TotalAmountOfRejectedTransactions { get; set; }
        public int TotalNumberOfAuthorizedTransactions { get; set; }
        public int TotalNumberOfRejectedTransactions { get; set; }
        public BatchStatus BatchStatus { get; set; }
        public List<ResponseCodeViewModel> ResponseCodeSummary { get; set; }
    }
}
