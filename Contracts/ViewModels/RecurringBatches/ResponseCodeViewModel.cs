using System;

namespace Contracts.ViewModels.RecurringBatches
{
    public class ResponseCodeViewModel
    {
        public Guid BatchId { get; set; }
        public string ResponseCode { get; set; }
        public int NumberOfTransactions { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
