using Contracts.DataEntities.RecurringPayments;
using System;

namespace Core.Interfaces.DataAccess.RecurringBatch
{
    public interface IBatchReportRepository
    {
        BatchReport GetSingle(Guid batchId);
    }
}
