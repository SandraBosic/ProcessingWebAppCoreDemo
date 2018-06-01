using Contracts.ViewModels.RecurringBatches;
using Core.Interfaces.Managers.Export;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Managers.Export
{
    public class RecurringBatchTransactionMapForExport : IMapForExport
    {
        public string[][] Map<T>(IEnumerable<T> data)
        {
            var transactions = (List<RecurringBatchTransactionDataViewModel>)data;
            var mapped = transactions.Select(Map).ToList();
            mapped.Insert(0, columnNames);
            return mapped.ToArray();
        }

        private readonly string[] columnNames = new[]
        {
            "Reference number",
            "Authorization code",
            "Trans id",
            "Amount",
            "Card number",
            "New card number"
        };

        private static string[] Map(RecurringBatchTransactionDataViewModel recurringBatch)
        {
            return new[]
            {
                recurringBatch.ReferenceNumber,
                recurringBatch.AuthorizationCode,
                recurringBatch.TransId,
                recurringBatch.Amount.ToString(CultureInfo.InvariantCulture),
                recurringBatch.CardNumber,
                recurringBatch.NewCardNumber
            };
        }
    }
}
