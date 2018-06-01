using System;
using Core.Constants;
using Microsoft.AspNetCore.Mvc;
using ProcessingWebApp.Attributes;

public class HomeController : Controller
{
    [UnhandledExceptionFilter]
    public ActionResult Index()
    {
        return View();
    }
}