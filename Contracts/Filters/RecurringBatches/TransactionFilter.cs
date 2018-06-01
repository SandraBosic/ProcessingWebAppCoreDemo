using Contracts.ViewModels.RecurringBatches;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Contracts.Filters.RecurringBatches
{
    public class TransactionFilter
    {
        public Guid BatchId { get; set; }
        [DisplayName("Response codes")]
        public string ResponseCode { get; set; }
        [DisplayName("Search")]
        public string TextSearch { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<ResponseCodeViewModel> AvailableResponseCodes { get; set; }
    }
}
