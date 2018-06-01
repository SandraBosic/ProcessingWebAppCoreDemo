using Contracts.Enums;
using Contracts.Exceptions;
using Contracts.Filters.RecurringBatches;
using Contracts.ViewModels;
using Contracts.ViewModels.RecurringBatches;
using Core.Interfaces.DataAccess.RecurringBatch;
using Core.Interfaces.Managers.RecurringBatch;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Managers.RecurringBatch
{
    public class BatchManager : IManageRecurringBatches
    {
        private readonly IBatchRepository batchRepository;
        private readonly IBatchReportRepository batchReportRepository;

        public BatchManager(IBatchRepository batchRepository, IBatchReportRepository batchReportRepository)
        {
            this.batchRepository = batchRepository;
            this.batchReportRepository = batchReportRepository;
        }

        public RecurringBatchViewModel GetBatchViewWithPaging(BatchFilter batchFilter)
        {
            try
            {
                var batchData = new List<RecurringBatchDataViewModel>();
                var batchPagination = batchRepository.GetPagedBatchList(batchFilter);
                foreach (var batch in batchPagination.Batches)
                {
                    var batchDataViewModel = new RecurringBatchDataViewModel
                    {
                        BatchStatus = batch.BatchStatus,
                        BatchId = batch.Id,
                        BatchDate = batch.CreateDateTime,
                        BatchNumber = batch.MerchantAssignedBatchNumber,
                        MerchantDbaName = batch.DbaName,
                        MerchantNationalId = batch.AuxiliaryField02,
                        MerchantNumber = batch.MerchantAgreementNumber
                    };

                    if (batch.BatchStatus == BatchStatus.Processed)
                    {
                        batchDataViewModel.TotalAmount = batch.BatchReport.BatchTotalAmount;
                    }

                    batchData.Add(batchDataViewModel);
                }
                return new RecurringBatchViewModel
                {
                    Batches = batchData,
                    Filter = batchFilter,
                    Pagination =
                        new PaginationViewModel
                        {
                            CurrentPage = batchPagination.CurrentPage,
                            IsNextPage = batchPagination.IsNextPage,
                            IsPreviousPage = batchPagination.IsPreviousPage,
                            TotalCount = batchPagination.TotalCount,
                            TotalPages = batchPagination.TotalPages,
                            PaginationFunctionUrl = "RecurringBatch/GetPage"
                        }
                };
            }
            catch (Exception exception)
            {
                //logger.LogError("Unexpected error occurred when filtering and mapping RecurringBatch", exception);
                return null;
            }
        }

        public RecurringBatchMetadataViewModel GetBatchMetadataView(Guid batchId)
        {
            try
            {
                var batchMetadataViewModel = new RecurringBatchMetadataViewModel();
                var batch = batchRepository.GetSingle(batchId);
                if (batch == null)
                {
                    return null;
                }

                batchMetadataViewModel.BatchStatus = batch.BatchStatus;
                batchMetadataViewModel.BatchId = batchId;
                batchMetadataViewModel.BatchNumber = batch.MerchantAssignedBatchNumber;
                batchMetadataViewModel.MerchantName = batch.MerchantName;

                if (batch.BatchStatus == BatchStatus.Processed)
                {
                    batch.BatchReport = batchReportRepository.GetSingle(batch.Id);
                    if (batch.BatchReport == null)
                    {
                        throw new BatchReportNotFoundForProcessedBatch();
                    }

                    batchMetadataViewModel.TotalAmount = batch.BatchReport.BatchTotalAmount;
                    batchMetadataViewModel.TotalNumberOfAuthorizedTransactions =
                        batch.BatchReport.TransactionReports.Count(x => x.ResponseCode == "00");
                    batchMetadataViewModel.TotalAmountOfApprovedTransactions =
                        batch.BatchReport.TransactionReports.Where(x => x.ResponseCode == "00").Sum(x => x.Amount);
                    batchMetadataViewModel.TotalNumberOfRejectedTransactions =
                        batch.BatchReport.TransactionReports.Count(x => x.ResponseCode != "00");
                    batchMetadataViewModel.TotalAmountOfApprovedTransactions =
                        batch.BatchReport.TransactionReports.Where(x => x.ResponseCode != "00").Sum(x => x.Amount);
                    batchMetadataViewModel.TotalNumberOfTransactions = batch.BatchReport.TransactionReports.Count;

                    var responseCodesInCurrentTransactionReport = new List<string>();
                    foreach (var transactionReport in batch.BatchReport.TransactionReports)
                    {
                        if (!responseCodesInCurrentTransactionReport.Contains(transactionReport.ResponseCode))
                        {
                            responseCodesInCurrentTransactionReport.Add(transactionReport.ResponseCode);
                        }
                    }

                    foreach (var responseCode in responseCodesInCurrentTransactionReport)
                    {
                        batchMetadataViewModel.ResponseCodeSummary.Add(new ResponseCodeViewModel
                        {
                            ResponseCode = responseCode,
                            BatchId = batchId,
                            TotalAmount =
                                batch.BatchReport.TransactionReports.Where(x => x.ResponseCode == responseCode)
                                    .Sum(x => x.Amount),
                            NumberOfTransactions =
                                batch.BatchReport.TransactionReports.Count(x => x.ResponseCode == responseCode)
                        });
                    }
                }

                return batchMetadataViewModel;
            }
            catch (Exception exception)
            {
                //logger.LogError($"Unexpected error occurred when fetching RecurringBatch metadata for batchId {batchId}", exception);
                return null;
            }
        }

        public List<RecurringBatchDataViewModel> GetBatchViewListForExport(BatchFilter batchFilter)
        {
            try
            {
                var batchData = new List<RecurringBatchDataViewModel>();
                var batchList = batchRepository.GetBatchList(batchFilter);
                foreach (var batch in batchList)
                {
                    var batchDataViewModel = new RecurringBatchDataViewModel
                    {
                        BatchStatus = batch.BatchStatus,
                        BatchId = batch.Id,
                        BatchDate = batch.CreateDateTime,
                        BatchNumber = batch.MerchantAssignedBatchNumber,
                        MerchantDbaName = batch.DbaName,
                        MerchantNationalId = batch.AuxiliaryField02,
                        MerchantNumber = batch.MerchantAgreementNumber
                    };

                    if (batch.BatchStatus == BatchStatus.Processed)
                    {
                        batchDataViewModel.TotalAmount = batch.BatchReport.BatchTotalAmount;
                    }

                    batchData.Add(batchDataViewModel);
                }
                return batchData;
            }
            catch (Exception exception)
            {
                //logger.LogError("Unexpected error occurred when filtering and mapping RecurringBatch", exception);
                return null;
            }
        }
    }
}
