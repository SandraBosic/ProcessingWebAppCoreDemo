using Contracts.ViewModels.RecurringBatches;
using Core.Interfaces.Managers.Export;
using System.Collections.Generic;
using System.Linq;

namespace Managers.Export
{
    public class RecurringBatchMapForExport : IMapForExport
    {
        public string[][] Map<T>(IEnumerable<T> data)
        {
            var batches = (List<RecurringBatchDataViewModel>)data;
            var mapped = batches.Select(Map).ToList();
            mapped.Insert(0, columnNames);
            return mapped.ToArray();
        }

        private readonly string[] columnNames = new[]
        {
            "MerchantAssignedBatchNumber",
            "Boðgreiðsla batch id",
            "Total amount",
            "Merchant ssn",
            "DBA name",
            "Agreement number",
            "Batch status",
            "Create date"
        };

        private static string[] Map(RecurringBatchDataViewModel recurringBatch)
        {
            return new[]
            {
                recurringBatch.BatchNumber,
                recurringBatch.BatchId.ToString(),
                recurringBatch.TotalAmount.ToString(),
                recurringBatch.MerchantNationalId,
                recurringBatch.MerchantDbaName,
                recurringBatch.MerchantNumber,
                recurringBatch.BatchStatus.ToString(),
                recurringBatch.BatchDate.ToString("dd.MM.yyyy HH:mm:ss.ffff")
            };
        }
    }
}
