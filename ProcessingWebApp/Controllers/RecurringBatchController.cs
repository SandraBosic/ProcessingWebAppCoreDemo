using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.Filters.RecurringBatches;
using Core.Configuration;
using Core.Interfaces.Configuration;
using Core.Interfaces.Configuration.Pagination;
using Core.Interfaces.Managers.Export;
using Core.Interfaces.Managers.RecurringBatch;
using Core.Interfaces.System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProcessingWebApp.Attributes;

namespace ProcessingWebApp.Controllers
{
    [UnhandledExceptionFilter]
    public class RecurringBatchController : BaseController
    {

        private readonly IClock clock;
        private readonly IManageRecurringBatches recurringBatchesManager;
        private BatchFilter defaultBatchFilter;
        private readonly IOptions<PagingConfiguration> paginationConfiguration;
        private readonly IGuid guid;
        
        public RecurringBatchController(IClock clock,
         IManageRecurringBatches recurringBatchesManager,
         IOptions<PagingConfiguration> paginationConfiguration, 
         IGuid guid, IManageExport exportManager) : base(exportManager)
        {
            this.clock = clock;
            this.recurringBatchesManager = recurringBatchesManager;
            this.paginationConfiguration = paginationConfiguration;
            this.guid = guid;
        }

        public IActionResult Index(Guid? viewBatch)
        {
            defaultBatchFilter = new BatchFilter
            {
                TextSearch = string.Empty,
                From = clock.Now.AddDays(-30),
                To = clock.Now,
                PageNumber = 1,
                PageSize = paginationConfiguration.Value.NumberOfRecordsPerPage,
                OpenBatch = viewBatch
            };

            SetSessionFilter(defaultBatchFilter);
            
            var model = recurringBatchesManager.GetBatchViewWithPaging(defaultBatchFilter);
            return View(model);
        }

        [HttpPost]
        public ActionResult BatchFilter(BatchFilter batchFilter)
        {
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage).ToList();
                var returnString = errorList.Aggregate(string.Empty, (current, error) => current + $"<div class='error'>{error}</div>");

                return Content(returnString);
            }

            batchFilter.PageSize = paginationConfiguration.Value.NumberOfRecordsPerPage;
            SetSessionFilter(batchFilter);

            var model = recurringBatchesManager.GetBatchViewWithPaging(batchFilter);

            return PartialView("_BatchData", model);
        }

        [HttpGet]
        public ActionResult GetPage(int pageNumber)
        {
            var batchFilter = GetSessionFilter(defaultBatchFilter);

            batchFilter.PageSize = paginationConfiguration.Value.NumberOfRecordsPerPage;
            batchFilter.PageNumber = pageNumber;

            var model = recurringBatchesManager.GetBatchViewWithPaging(batchFilter);

            return PartialView("_BatchData", model);
        }

        [HttpGet]
        public ActionResult GetBatchMetadata(Guid id)
        {
            var model = recurringBatchesManager.GetBatchMetadataView(id);

            return PartialView("_BatchMetadata", model);
        }
    }
}