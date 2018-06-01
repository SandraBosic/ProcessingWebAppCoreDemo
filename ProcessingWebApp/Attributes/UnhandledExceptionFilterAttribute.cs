using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProcessingWebApp.Attributes
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public UnhandledExceptionFilterAttribute()
        {
            //inject logger
        }

        public override void OnException(ExceptionContext context)
        {
            //log exception

            context.Result = new ViewResult {ViewName = "Error"};   
        }
    }
}
