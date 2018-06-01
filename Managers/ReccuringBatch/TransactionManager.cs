using Contracts.Filters.RecurringBatches;
using Contracts.ViewModels;
using Contracts.ViewModels.RecurringBatches;
using Core.Interfaces.DataAccess.RecurringBatch;
using Core.Interfaces.Managers.RecurringBatch;
using Core.Interfaces.System;
using Core.Interfaces.VirtualNumberApiAccess;
using System;
using System.Collections.Generic;

namespace Managers.RecurringBatch
{
    public class TransactionManager : IManageRecurringBatchTransactions
    {
        private readonly ITransactionReportRepository transactionReportRepository;
        private readonly IGuid guid;
        private readonly IVirtualNumberApiAccess virtualNumberApiAccess;
        private readonly IBatchReportRepository batchReportRepository;

        public TransactionManager(ITransactionReportRepository transactionReportRepository, IGuid guid,
            IVirtualNumberApiAccess virtualNumberApiAccess, IBatchReportRepository batchReportRepository)
        {
            this.transactionReportRepository = transactionReportRepository;
            this.guid = guid;
            this.virtualNumberApiAccess = virtualNumberApiAccess;
            this.batchReportRepository = batchReportRepository;
        }

        public RecurringBatchTransactionViewModel GetTransactionViewWithPaging(TransactionFilter filter)
        {
            try
            {
                var batchReport = batchReportRepository.GetSingle(filter.BatchId);

                var transactionViewModel = new RecurringBatchTransactionViewModel
                {
                    AgreementNumber = batchReport.MerchantAgreementNumber,
                    BatchStatus = batchReport.BatchStatus,
                    DbaName = batchReport.DbaName,
                    MerchantAssignedBatchNumber = batchReport.MerchantAssignedBatchNumber,
                    MerchantName = batchReport.MerchantName,
                    MerchantNationalId = batchReport.AuxiliaryField02,
                    ProcessingEndTime = batchReport.ProcessingEndTime,
                    ProcessingStartTime = batchReport.ProcessingStartTime,
                    BatchId = filter.BatchId
                };

                var transactionReports = transactionReportRepository.GetPagedTransactionList(filter);
                foreach (var transactionReport in transactionReports.Transactions)
                {
                    var transactionDataViewModel = new RecurringBatchTransactionDataViewModel
                    {
                        ReferenceNumber = transactionReport.ReferenceNumber,
                        TransId = transactionReport.TransId,
                        AuthorizationCode = transactionReport.AuthCode,
                        CardNumber = transactionReport.AuxiliaryField05,
                        Amount = transactionReport.Amount,
                        TransactionId = transactionReport.Id,
                        VirtualAccountNumber = transactionReport.ReceivedVirtualAccount,
                        UpdatedVirtualAccountNumber = transactionReport.UpdatedVirtualAccount
                    };
                    if (transactionReport.UpdatedVirtualAccount != guid.Empty)
                    {
                        transactionDataViewModel.NewCardNumber =
                            virtualNumberApiAccess.Get6Plus4CardForVirtualNumber(transactionReport.UpdatedVirtualAccount);
                    }

                    transactionViewModel.Transactions.Add(transactionDataViewModel);
                }
                transactionViewModel.TransactionFilter = filter;
                transactionViewModel.Pagination = new PaginationViewModel
                {
                    CurrentPage = transactionReports.CurrentPage,
                    IsNextPage = transactionReports.IsNextPage,
                    IsPreviousPage = transactionReports.IsPreviousPage,
                    TotalCount = transactionReports.TotalCount,
                    TotalPages = transactionReports.TotalPages,
                    PaginationFunctionUrl = "RecurringBatchTransaction/GetPage"
                };
                return transactionViewModel;
            }
            catch (Exception exception)
            {
                //logger.LogError("Unexpected error occurred when filtering and mapping RecurringBatch", exception);
                return null;
            }
        }

        public List<RecurringBatchTransactionDataViewModel> GetTransactionListForExport(TransactionFilter filter)
        {
            try
            {
                var transactionData = new List<RecurringBatchTransactionDataViewModel>();
                var transactionReports = transactionReportRepository.GetPagedTransactionList(filter);
                foreach (var transactionReport in transactionReports.Transactions)
                {
                    var transactionDataViewModel = new RecurringBatchTransactionDataViewModel
                    {
                        ReferenceNumber = transactionReport.ReferenceNumber,
                        TransId = transactionReport.TransId,
                        AuthorizationCode = transactionReport.AuthCode,
                        CardNumber = transactionReport.AuxiliaryField05
                    };
                    if (transactionReport.UpdatedVirtualAccount != guid.Empty)
                    {
                        transactionDataViewModel.NewCardNumber =
                            virtualNumberApiAccess.Get6Plus4CardForVirtualNumber(transactionReport.UpdatedVirtualAccount);
                    }

                    transactionData.Add(transactionDataViewModel);
                }
                return transactionData;
            }
            catch (Exception exception)
            {
                //logger.LogError("Unexpected error occurred when filtering and mapping RecurringBatch", exception);
                return null;
            }
        }
    }
}
