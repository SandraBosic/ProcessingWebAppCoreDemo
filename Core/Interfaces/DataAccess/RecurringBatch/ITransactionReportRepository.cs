using Contracts.DataEntities.RecurringPayments;
using Contracts.Filters.RecurringBatches;
using System;

namespace Core.Interfaces.DataAccess.RecurringBatch
{
    public interface ITransactionReportRepository
    {
        TransactionReport GetSingle(Guid transactionReportId);
        RecurringBatchTransacitonPaginationList GetPagedTransactionList(TransactionFilter filter);
    }
}
