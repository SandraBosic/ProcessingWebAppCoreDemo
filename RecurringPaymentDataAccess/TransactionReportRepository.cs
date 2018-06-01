using Contracts.DataEntities.RecurringPayments;
using Contracts.Enums;
using Contracts.Filters.RecurringBatches;
using Core.Interfaces.DataAccess.RecurringBatch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecurringPaymentDataAccess
{
    public class TransactionReportRepository : ITransactionReportRepository
    {
        public TransactionReport GetSingle(Guid transactionReportId)
        {
            // TODO : implement access to database or data service
            var rand = new Random();
            var batchId = Guid.NewGuid();
            var responseCodes = new List<string>
            {
                "00",
                "01",
                "02",
                "03",
                "04"
            };
            var transactionReport = new TransactionReport
            {
                Amount = 100,
                AuthCode = rand.Next(10000, 99999).ToString(),
                AuxiliaryField01 = "1912822999",
                AuxiliaryField02 = "1912822999",
                AuxiliaryField03 = "Skýring",
                AuxiliaryField05 = "999999******9999",
                BatchId = batchId,
                Id = Guid.NewGuid(),
                ReceivedVirtualAccount = Guid.Parse("94A8F887-E551-46DC-BF19-55F2E62A262D"),
                ReferenceNumber = rand.Next(100000, 9999999).ToString(),
                ResponseCode = responseCodes.OrderBy(s => Guid.NewGuid()).First(),
                TransId = rand.Next(1000000, 9999999).ToString(),
                TransLcid = string.Empty
            };
            transactionReport.TransactionStatus = transactionReport.ResponseCode == "00"
                ? TransactionStatus.Processed
                : TransactionStatus.Invalid;

            return transactionReport;
        }

        public RecurringBatchTransacitonPaginationList GetPagedTransactionList(TransactionFilter filter)
        {
            // TODO : implement access to database or data service
            var batchRepository = new BatchRepository();
            var batch = batchRepository.GetSingle(Guid.Empty);
            var rand = new Random();
            var responseCodes = new List<string>
            {
                "00",
                "01",
                "02",
                "03",
                "04"
            };
            for (var j = 0; j < 1200; j++)
            {
                var transactionReport = new TransactionReport
                {
                    Amount = 100,
                    AuthCode = rand.Next(10000, 99999).ToString(),
                    AuxiliaryField01 = "1912822999",
                    AuxiliaryField02 = "1912822999",
                    AuxiliaryField03 = "Skýring",
                    AuxiliaryField05 = "999999******9999",
                    BatchId = filter.BatchId,
                    Id = Guid.NewGuid(),
                    ReceivedVirtualAccount = Guid.Parse("94A8F887-E551-46DC-BF19-55F2E62A262D"),
                    ReferenceNumber = rand.Next(100000, 9999999).ToString(),
                    ResponseCode = responseCodes.OrderBy(s => Guid.NewGuid()).First(),
                    TransId = rand.Next(1000000, 9999999).ToString(),
                    TransLcid = string.Empty
                };
                transactionReport.UpdatedVirtualAccount = transactionReport.ResponseCode == "00" && j % 2 == 0
                    ? Guid.Parse("87A2013B-31B9-406F-B8E0-D5D14728F297")
                    : Guid.Empty;
                transactionReport.TransactionStatus = transactionReport.ResponseCode == "00"
                    ? TransactionStatus.Processed
                    : TransactionStatus.Invalid;
                batch.BatchReport.TransactionReports.Add(transactionReport);
            }

            var filteredByBatchIdAndResponseCode = batch.BatchReport.TransactionReports.Where(x => x.BatchId == filter.BatchId && x.ResponseCode == filter.ResponseCode).ToList();
            if (!string.IsNullOrEmpty(filter.TextSearch))
            {
                batch.BatchReport.TransactionReports =
                    filteredByBatchIdAndResponseCode.Where(
                        x => x.AuxiliaryField05.ToLower().Contains(filter.TextSearch.ToLower()) ||
                             x.ReferenceNumber.ToLower().Contains(filter.TextSearch.ToLower()) ||
                             x.TransId.ToLower().Contains(filter.TextSearch.ToLower()) ||
                             x.AuthCode.ToLower().Contains(filter.TextSearch.ToLower())).ToList();
            }

            var recurringBatchTransactionPaginationList = new RecurringBatchTransacitonPaginationList()
            {
                TotalCount = batch.BatchReport.TransactionReports.Count,
                CurrentPage = filter.PageNumber,
            };

            recurringBatchTransactionPaginationList.TotalPages = (int)Math.Ceiling(recurringBatchTransactionPaginationList.TotalCount / (double)filter.PageSize);

            recurringBatchTransactionPaginationList.Transactions = batch.BatchReport.TransactionReports.Skip((recurringBatchTransactionPaginationList.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize).ToList();

            recurringBatchTransactionPaginationList.IsPreviousPage = filter.PageNumber > 1;

            recurringBatchTransactionPaginationList.IsNextPage = filter.PageNumber < recurringBatchTransactionPaginationList.TotalPages;

            return recurringBatchTransactionPaginationList;
        }
    }
}
