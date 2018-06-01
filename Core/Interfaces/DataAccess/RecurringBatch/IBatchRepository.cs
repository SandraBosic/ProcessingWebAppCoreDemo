using Contracts.DataEntities.RecurringPayments;
using Contracts.Filters.RecurringBatches;
using System;
using System.Collections.Generic;

namespace Core.Interfaces.DataAccess.RecurringBatch
{
    public interface IBatchRepository
    {
        Batch GetSingle(Guid batchId);
        RecurringBatchPaginationList GetPagedBatchList(BatchFilter filter);
        List<Batch> GetBatchList(BatchFilter filter);
    }
}
