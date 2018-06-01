using Contracts.Enums;
using System;

namespace Contracts.ViewModels.RecurringBatches
{
    public class RecurringBatchDataViewModel
    {
        public Guid BatchId { get; set; }
        public string BatchNumber { get; set; }
        public decimal? TotalAmount { get; set; }
        public string MerchantNationalId { get; set; }
        public string MerchantDbaName { get; set; }
        public string MerchantNumber { get; set; }
        public BatchStatus BatchStatus { get; set; }
        public DateTime BatchDate { get; set; }
    }
}
