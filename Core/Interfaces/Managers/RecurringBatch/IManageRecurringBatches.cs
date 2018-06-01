using Contracts.Filters.RecurringBatches;
using Contracts.ViewModels.RecurringBatches;
using System;
using System.Collections.Generic;

namespace Core.Interfaces.Managers.RecurringBatch
{
    public interface IManageRecurringBatches
    {
        RecurringBatchViewModel GetBatchViewWithPaging(BatchFilter batchFilter);
        RecurringBatchMetadataViewModel GetBatchMetadataView(Guid batchId);
        List<RecurringBatchDataViewModel> GetBatchViewListForExport(BatchFilter batchFilter);
    }
}
