using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProcessingWebApp.Controllers
{
    public class RecurringBatchTransactionController : Controller
    {
        public IActionResult Index(Guid batchId, string responseCode)
        {
            return View();
        }
    }
}