using Contracts.DataEntities.RecurringPayments;
using Contracts.Enums;
using Contracts.Filters.RecurringBatches;
using Core.Interfaces.DataAccess.RecurringBatch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecurringPaymentDataAccess
{
    public class BatchRepository : IBatchRepository
    {
        public Batch GetSingle(Guid batchId)
        {
            // TODO : implement access to database or data service
            var batch = new Batch
            {
                Id = Guid.NewGuid(),
                BatchStatus = BatchStatus.Processed,
                BatchClosingDateTime = DateTime.Now,
                CreateDateTime = DateTime.Now,
                DbaName = "Sjonna búð",
                MerchantAgreementNumber = "123456",
                MerchantCity = "Reykjavík",
                MerchantAssignedBatchNumber = "123456789",
                MerchantCountry = "352",
                MerchantName = "Sjonni HF",
                MerchantPostCode = "112",
                MerchantTypeMccCode = "2388",
                ProcessingEndTime = DateTime.Now,
                ProcessingStartTime = DateTime.Now,
                TerminalId = "1234567",
                AuxiliaryField01 = Guid.NewGuid().ToString(),
                AuxiliaryField02 = "1912822999"
            };

            batch.BatchReport = new BatchReport
            {
                BatchStatus = batch.BatchStatus,
                BatchId = batch.Id,
                AuthorizedBatchAmount = 1000,
                AuxiliaryField01 = batch.AuxiliaryField01,
                AuxiliaryField02 = batch.AuxiliaryField02,
                BatchTotalAmount = 1200,
                DbaName = batch.DbaName,
                MerchantAgreementNumber = batch.MerchantAgreementNumber,
                MerchantAssignedBatchNumber = batch.MerchantAssignedBatchNumber,
                MerchantName = batch.MerchantName,
                NumberOfAuthorizedTransactions = 10,
                ProcessingStartTime = batch.CreateDateTime.AddSeconds(10),
                ProcessingEndTime = batch.CreateDateTime.AddSeconds(20),
                TotalNumberOfTransactions = 12
            };

            return batch;
        }

        public RecurringBatchPaginationList GetPagedBatchList(BatchFilter filter)
        {
            // TODO : implement access to database or data service
            var rand = new Random();
            var batchList = new List<Batch>();
            for (var i = 0; i < 1500; i++)
            {
                var batch = new Batch
                {
                    Id = i == 0 ? Guid.Parse("37d16b94-3345-4149-bed7-35135df18379") : Guid.NewGuid(),
                    BatchStatus = i%2==0 ? BatchStatus.Processed : BatchStatus.Open,
                    BatchClosingDateTime = DateTime.Now,
                    CreateDateTime = DateTime.Now.AddDays(-1*i),
                    DbaName = $"Sjonna búð {i}",
                    MerchantAgreementNumber = i.ToString().PadLeft(6, '0'),
                    MerchantCity = "Reykjavík",
                    MerchantAssignedBatchNumber = "123456789",
                    MerchantCountry = "352",
                    MerchantName = $"Sjonni {i} HF",
                    MerchantPostCode = "112",
                    MerchantTypeMccCode = "2388",
                    ProcessingEndTime = DateTime.Now,
                    ProcessingStartTime = DateTime.Now,
                    TerminalId = rand.Next(1000, 10000).ToString(),
                    AuxiliaryField01 = Guid.NewGuid().ToString(),
                    AuxiliaryField02 = "1912822999"
                };
                if (i%2 == 0)
                {
                    batch.BatchReport = new BatchReport
                    {
                        BatchStatus = batch.BatchStatus,
                        BatchId = batch.Id,
                        AuthorizedBatchAmount = 1000,
                        AuxiliaryField01 = batch.AuxiliaryField01,
                        AuxiliaryField02 = batch.AuxiliaryField02,
                        BatchTotalAmount = 1200,
                        DbaName = batch.DbaName,
                        MerchantAgreementNumber = batch.MerchantAgreementNumber,
                        MerchantAssignedBatchNumber = batch.MerchantAssignedBatchNumber,
                        MerchantName = batch.MerchantName,
                        NumberOfAuthorizedTransactions = 10,
                        ProcessingStartTime = batch.CreateDateTime.AddSeconds(10),
                        ProcessingEndTime = batch.CreateDateTime.AddSeconds(20),
                        TotalNumberOfTransactions = 12
                    };
                }
                batchList.Add(batch);
            }

            var filteredByDate = batchList.Where(x => x.CreateDateTime > filter.From && x.CreateDateTime < filter.To).ToList();
            if (!string.IsNullOrEmpty(filter.TextSearch))
            {
                batchList =
                    filteredByDate.Where(
                        x => x.DbaName.ToLower().Contains(filter.TextSearch.ToLower()) ||
                             x.MerchantAgreementNumber.ToLower().Contains(filter.TextSearch.ToLower()) ||
                             x.MerchantName.ToLower().Contains(filter.TextSearch.ToLower()) ||
                             x.TerminalId.ToLower().Contains(filter.TextSearch.ToLower())).ToList();
            }

            var recurringBatchPaginationList = new RecurringBatchPaginationList
            {
                TotalCount = batchList.Count,
                CurrentPage = filter.PageNumber,
            };
            
            recurringBatchPaginationList.TotalPages = (int)Math.Ceiling(recurringBatchPaginationList.TotalCount / (double)filter.PageSize);

            recurringBatchPaginationList.Batches =  batchList.Skip((recurringBatchPaginationList.CurrentPage - 1) * filter.PageSize).Take(filter.PageSize).ToList();

            recurringBatchPaginationList.IsPreviousPage = filter.PageNumber > 1;

            recurringBatchPaginationList.IsNextPage = filter.PageNumber < recurringBatchPaginationList.TotalPages;

            return recurringBatchPaginationList;
        }

        public List<Batch> GetBatchList(BatchFilter filter)
        {
            // TODO : implement access to database or data service
            var rand = new Random();
            var batchList = new List<Batch>();
            for (var i = 0; i < 40; i++)
            {
                var batch = new Batch
                {
                    Id = Guid.NewGuid(),
                    BatchStatus = i % 2 == 0 ? BatchStatus.Processed : BatchStatus.Open,
                    BatchClosingDateTime = DateTime.Now,
                    CreateDateTime = DateTime.Now.AddDays(-1 * i),
                    DbaName = $"Sjonna búð {i}",
                    MerchantAgreementNumber = i.ToString().PadLeft(6, '0'),
                    MerchantCity = "Reykjavík",
                    MerchantAssignedBatchNumber = "123456789",
                    MerchantCountry = "352",
                    MerchantName = $"Sjonni {i} HF",
                    MerchantPostCode = "112",
                    MerchantTypeMccCode = "2388",
                    ProcessingEndTime = DateTime.Now,
                    ProcessingStartTime = DateTime.Now,
                    TerminalId = rand.Next(1000, 10000).ToString(),
                    AuxiliaryField01 = Guid.NewGuid().ToString(),
                    AuxiliaryField02 = "1912822999"
                };
                if (i % 2 == 0)
                {
                    batch.BatchReport = new BatchReport
                    {
                        BatchStatus = batch.BatchStatus,
                        BatchId = batch.Id,
                        AuthorizedBatchAmount = 1000,
                        AuxiliaryField01 = batch.AuxiliaryField01,
                        AuxiliaryField02 = batch.AuxiliaryField02,
                        BatchTotalAmount = 1200,
                        DbaName = batch.DbaName,
                        MerchantAgreementNumber = batch.MerchantAgreementNumber,
                        MerchantAssignedBatchNumber = batch.MerchantAssignedBatchNumber,
                        MerchantName = batch.MerchantName,
                        NumberOfAuthorizedTransactions = 10,
                        ProcessingStartTime = batch.CreateDateTime.AddSeconds(10),
                        ProcessingEndTime = batch.CreateDateTime.AddSeconds(20),
                        TotalNumberOfTransactions = 12
                    };
                }
                batchList.Add(batch);
            }

            var filteredByDate = batchList.Where(x => x.CreateDateTime > filter.From && x.CreateDateTime < filter.To).ToList();
            if (!string.IsNullOrEmpty(filter.TextSearch))
            {
                batchList =
                    filteredByDate.Where(
                        x => x.DbaName.ToLower().Contains(filter.TextSearch.ToLower()) ||
                             x.MerchantAgreementNumber.ToLower().Contains(filter.TextSearch.ToLower()) ||
                             x.MerchantName.ToLower().Contains(filter.TextSearch.ToLower()) ||
                             x.TerminalId.ToLower().Contains(filter.TextSearch.ToLower())).ToList();
            }

            return batchList;
        }
    }
}
