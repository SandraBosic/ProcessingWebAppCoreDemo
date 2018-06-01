using Contracts.Filters.RecurringBatches;
using System.Collections.Generic;

namespace Contracts.ViewModels.RecurringBatches
{
    public class RecurringBatchViewModel
    {
        public RecurringBatchViewModel()
        {
            Batches = new List<RecurringBatchDataViewModel>();
        }
        public BatchFilter Filter { get; set; }
        public IList<RecurringBatchDataViewModel> Batches { get; set; }

        public PaginationViewModel Pagination { get; set; }
    }
}
