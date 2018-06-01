using Contracts.Enums;
using System;

namespace Contracts.DataEntities.RecurringPayments
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public Guid BatchId { get; set; }

        public Guid VirtualAccount { get; set; }

        public string ReferenceNumber { get; set; }

        public TransactionStatus TransactionStatus { get; set; }

        public string TransAmount { get; set; }

        public string TransCurrency { get; set; }

        public string ResponseCode { get; set; }
        public string TransId { get; set; }
        public string AuthCode { get; set; }
        public string TransLcid { get; set; }
        public Guid UpdatedVirtualAccount { get; set; }

        public Batch Batch { get; set; }

        public DateTime CreateDateTime { get; set; }
        
        public string AuxiliaryField01 { get; set; }

        public string AuxiliaryField02 { get; set; }

        public string AuxiliaryField03 { get; set; }

        public string AuxiliaryField04 { get; set; }
        public string AuxiliaryField05 { get; set; }
    }
}
