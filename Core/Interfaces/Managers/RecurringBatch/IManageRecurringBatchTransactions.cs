using Contracts.Filters.RecurringBatches;
using Contracts.ViewModels.RecurringBatches;
using System.Collections.Generic;

namespace Core.Interfaces.Managers.RecurringBatch
{
    public interface IManageRecurringBatchTransactions
    {
        RecurringBatchTransactionViewModel GetTransactionViewWithPaging(TransactionFilter filter);
        List<RecurringBatchTransactionDataViewModel> GetTransactionListForExport(TransactionFilter filter);
    }
}
