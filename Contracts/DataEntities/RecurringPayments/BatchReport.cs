using Contracts.Enums;
using System;
using System.Collections.Generic;

namespace Contracts.DataEntities.RecurringPayments
{
    public class BatchReport
    {
        public BatchReport()
        {
            TransactionReports = new List<TransactionReport>();
        }
        public Guid BatchId { get; set; }
        public DateTime ProcessingStartTime { get; set; }
        public DateTime ProcessingEndTime { get; set; }
        public int TotalNumberOfTransactions { get; set; }
        public int NumberOfAuthorizedTransactions { get; set; }
        public decimal BatchTotalAmount { get; set; }
        public decimal AuthorizedBatchAmount { get; set; }
        public string MerchantAgreementNumber { get; set; }
        public string MerchantAssignedBatchNumber { get; set; }
        public string MerchantName { get; set; }
        public List<TransactionReport> TransactionReports { get; set; }
        public BatchStatus BatchStatus { get; set; }
        public Batch Batch { get; set; }
        public string DbaName { get; set; }
        public string AuxiliaryField01 { get; set; }
        public string AuxiliaryField02 { get; set; }
    }
}
