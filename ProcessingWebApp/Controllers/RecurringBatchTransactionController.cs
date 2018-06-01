using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProcessingWebApp.Attributes;

namespace ProcessingWebApp.Controllers
{
    [UnhandledExceptionFilter]
    public class RecurringBatchTransactionController : Controller
    {
        public IActionResult Index(Guid batchId, string responseCode)
        {
            return View();
        }
    }
}