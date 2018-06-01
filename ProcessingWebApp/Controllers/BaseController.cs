using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Managers.Export;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ProcessingWebApp.Controllers
{
    public class BaseController : Controller
    {
        private IManageExport exportManager;

        public BaseController(IManageExport exportManager)
        {
            this.exportManager = exportManager;
        }

        protected T GetSessionFilter<T>(T defaultObject)
        {
            var sessionFilter = HttpContext.Session.GetString(typeof(T).FullName);
            if (sessionFilter == null)
            {
                HttpContext.Session.SetString(typeof(T).FullName, JsonConvert.SerializeObject(defaultObject));
                return defaultObject;
            }
            return JsonConvert.DeserializeObject<T>(sessionFilter);
        }

        protected void SetSessionFilter<T>(T filter)
        {
            HttpContext.Session.SetString(typeof(T).FullName, JsonConvert.SerializeObject(filter));
        }
    }
}