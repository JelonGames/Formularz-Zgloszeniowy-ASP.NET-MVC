using FormuarzRefresh.Function;
using FormuarzRefresh.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FormuarzRefresh.Controllers
{
    public class PageController : Controller
    {
        //funkcja do sprawdzania czy uzytkownik jest zalogowany
        private bool IsLogon()
        {
            object? person = null;
            if (TempData.TryGetValue("LogOnPerson", out person))
            {
                TempData.Keep("LogOnPerson");
                ViewData["Person"] = person.ToString();
                return true;
            }

            ViewData["Person"] = string.Empty;
            return false;
        }

        public IActionResult AddUser()
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            try
            {
                if (GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]") == null ||
                    GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[VoipProject]") == null ||
                    GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Printers]") == null ||
                    GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Apps]") == null)
                        throw new Exception();


                ViewData["Dept"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]"));
                ViewData["Voip"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[VoipProject]"));
                ViewData["Printers"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Printers]"));
                ViewData["Apps"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Apps]"));
            }
            catch (Exception)
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    RequestId = "001DB-AddUserView"
                };

                return RedirectToAction("MyError", "Home", model);
            }
            

            return View("AddUser");
        }

        public IActionResult ModUser()
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            try
            {
                if (GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]") == null ||
                    GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[VoipProject]") == null ||
                    GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Printers]") == null ||
                    GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Apps]") == null)
                    throw new Exception();


                ViewData["Dept"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]"));
                ViewData["Voip"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[VoipProject]"));
                ViewData["Printers"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Printers]"));
                ViewData["Apps"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Apps]"));
            }
            catch (Exception)
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    RequestId = "001DB-ModUserView"
                };

                return RedirectToAction("MyError", "Home", model);
            }


            return View("ModUser");
        }

        public IActionResult DelUser()
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            try
            {
                if (GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]") == null ||
                    GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Printers]") == null ||
                    GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Apps]") == null)
                    throw new Exception();


                ViewData["Dept"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]"));
                ViewData["Printers"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Printers]"));
                ViewData["Apps"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Apps]"));
            }
            catch (Exception)
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    RequestId = "001DB-DelPersonView"
                };

                return RedirectToAction("MyError", "Home", model);
            }


            return View("DelUser");
        }

        public IActionResult RestartPassword()
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            try
            {
                if (GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]") == null)
                    throw new Exception();


                ViewData["Dept"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]"));
            }
            catch (Exception)
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    RequestId = "001DB-DelPersonView"
                };

                return RedirectToAction("MyError", "Home", model);
            }


            return View("RestartPassword");
        }

        public IActionResult Resources()
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            try
            {
                if (GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]") == null)
                    throw new Exception();


                ViewData["Dept"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]"));
            }
            catch (Exception)
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    RequestId = "001DB-DelPersonView"
                };

                return RedirectToAction("MyError", "Home", model);
            }

            return View("Resources");
        }
    }
}
