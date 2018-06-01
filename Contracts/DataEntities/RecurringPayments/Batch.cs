using Contracts.Enums;
using System;
using System.Collections.Generic;

namespace Contracts.DataEntities.RecurringPayments
{
    public class Batch
    {
        public Guid Id { get; set; }
        public string MerchantAgreementNumber { get; set; }         
        public string TerminalId { get; set; }
        public string MerchantAssignedBatchNumber { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? ProcessingStartTime { get; set; }
        public DateTime? ProcessingEndTime { get; set; }
        public BatchStatus BatchStatus { get; set; }
        public string MerchantTypeMccCode { get; set; }
        public string MerchantName { get; set; }
        public string MerchantCity { get; set; }
        public string MerchantPostCode { get; set; }
        public string MerchantCountry { get; set; }
        public virtual List<Transaction> Transactions { get; set; }
        public BatchReport BatchReport { get; set; }
        public DateTime? BatchClosingDateTime { get; set; }
        public DateTime? BatchDeletedDateTime { get; set; }
        public string DbaName { get; set; }
        public string AuxiliaryField01 { get; set; }
        public string AuxiliaryField02 { get; set; }
    }
}
