using FormuarzRefresh.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace FormuarzRefresh.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            object? person = null;
            
            if(TempData.TryGetValue("LogOnPerson", out person))
            {
                TempData.Keep("LogOnPerson");
                ViewData["Person"] = person.ToString();
            }
            else 
            { 
                ViewData["Person"] = string.Empty;
            }

            return View("Index");
        }

        public IActionResult SendApproval(ErrorViewModel error)
        {
            object? person = null;

            if (TempData.TryGetValue("LogOnPerson", out person))
            {
                TempData.Keep("LogOnPerson");
                ViewData["Person"] = person.ToString();
            }
            else
            {
                ViewData["Person"] = string.Empty;
            }

            if(error.RequestId != null) 
            {
                ViewData["Error"] = JsonConvert.SerializeObject(error);
            }

            return View("SendApproval");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            object? person = null;

            if (TempData.TryGetValue("LogOnPerson", out person))
            {
                TempData.Keep("LogOnPerson");
                ViewData["Person"] = person.ToString();
            }
            else
            {
                ViewData["Person"] = string.Empty;
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult MyError(ErrorViewModel errorViewModel)
        {
            object? person = null;

            if (TempData.TryGetValue("LogOnPerson", out person))
            {
                TempData.Keep("LogOnPerson");
                ViewData["Person"] = person.ToString();
            }
            else
            {
                ViewData["Person"] = string.Empty;
            }

            return View("~/Views/Shared/Error.cshtml", errorViewModel);
        }
    }
}