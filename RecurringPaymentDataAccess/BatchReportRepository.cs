using Contracts.DataEntities.RecurringPayments;
using Contracts.Enums;
using Core.Interfaces.DataAccess.RecurringBatch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecurringPaymentDataAccess
{
    public class BatchReportRepository : IBatchReportRepository
    {
        public BatchReport GetSingle(Guid batchId)
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
            batch.BatchReport.TransactionReports = new List<TransactionReport>();
            for (var j = 0; j < 12; j++)
            {
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
                transactionReport.UpdatedVirtualAccount = transactionReport.ResponseCode == "00" && j % 2 == 0
                    ? Guid.Parse("87A2013B-31B9-406F-B8E0-D5D14728F297")
                    : Guid.Empty;
                transactionReport.TransactionStatus = transactionReport.ResponseCode == "00"
                    ? TransactionStatus.Processed
                    : TransactionStatus.Invalid;
                batch.BatchReport.TransactionReports.Add(transactionReport);
            }

            return batch.BatchReport;
        }
    }
}
