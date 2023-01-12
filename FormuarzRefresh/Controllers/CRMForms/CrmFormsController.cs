using FormuarzRefresh.Function;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Newtonsoft.Json;
using FormuarzRefresh.Models;

namespace FormuarzRefresh.Controllers.CRMForms
{
    public class CrmFormsController : Controller
    {
        DataTable dt = new DataTable();
        DataTable maxPageDT = new DataTable();

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

        [Route("/CrmForms/ListForms/{id:int}", Name = "ListForms")]
        public IActionResult ListForms(int id)
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            TempData.Keep("LogOnPerosn");
            LogOnPerson person = JsonConvert.DeserializeObject<LogOnPerson>(TempData["LogOnPerson"].ToString());
            if (person.IsHR == true || person.IsAdmin == true)
            {
                dt = GetDataInDatabase.GetData($"SELECT TOP (100) [ID], [SendDate], [CompleteDate], [Dept], [User], [Status], [Title], [Version] FROM [dbo].[ViewListForms] where [ID] <= (Select MAX([ID]) - ({id} * 100) FROM [dbo].[ViewListForms]) order by [ID] desc");
                maxPageDT = GetDataInDatabase.GetData("SELECT MAX([ID]) AS MAX FROM [dbo].[ViewListForms]");
            }
            else
            {
                string user = person.FirstName + " " + person.LastName;
                string query1 = $"SELECT TOP (100) [ID], [SendDate], [CompleteDate], [Dept], [User], [Status], [Title], [Version] FROM [dbo].[ViewListForms] where [ID] <= (Select MAX([ID]) - ({id} * 100) FROM [dbo].[ViewListForms]) AND [User] = '{user}' order by [ID] desc";
                string query2 = $"SELECT MAX([ID]) AS MAX FROM [dbo].[ViewListForms] WHERE [User] = '{user}'";

                dt = GetDataInDatabase.GetData(query1);
                maxPageDT = GetDataInDatabase.GetData(query2);
            }

            int maxPage = 0;
            if (maxPageDT.Rows.Count > 0 && maxPageDT != null)
            {
                maxPage = (int)maxPageDT.Rows[0]["MAX"];
                maxPage = maxPage / 100;
            }

            ViewData.Add("Items", JsonConvert.SerializeObject(dt));
            ViewData.Add("Page", id);
            ViewData.Add("MaxPage", maxPage);

            try
            {
                if (GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]") == null ||
                    GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [dbo].[StatusName]") == null)
                    throw new Exception();


                ViewData["Dept"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]"));
                ViewData["Status"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [dbo].[StatusName]"));
            }
            catch (Exception)
            {
                ErrorViewModel errorModel = new ErrorViewModel()
                {
                    RequestId = "001DB-ListForms"
                };

                return RedirectToAction("MyError", "Home", errorModel);
            }

            return View("ListForms");
        }

        [HttpPost]
        public IActionResult ListFormsSearch(ListFormsSearch model)
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            string queryString = $"SELECT [ID], [SendDate], [CompleteDate], [Dept], [User], [Status], [Title], [Version] FROM [dbo].[ViewListForms] WHERE [User] like '%{model.User}%'";

            if (model.SendDateFrom.Date != new DateTime(1, 1, 1) && model.SendDateTo != new DateTime(1, 1, 1))
                queryString += $" AND (YEAR([SendDate]) BETWEEN {model.SendDateFrom.Year} AND {model.SendDateTo.Year} AND MONTH([SendDate]) BETWEEN {model.SendDateFrom.Month} AND {model.SendDateTo.Month} AND DAY([SendDate]) BETWEEN {model.SendDateFrom.Day} AND {model.SendDateTo.Day})";

            if (model.CompleteDateFrom != new DateTime(1, 1, 1) && model.CompleteDateTo != new DateTime(1, 1, 1))
                queryString += $" AND (YEAR([CompleteDate]) BETWEEN {model.CompleteDateFrom.Year} AND {model.CompleteDateTo.Year} AND MONTH([CompleteDate]) BETWEEN {model.CompleteDateFrom.Month} AND {model.CompleteDateTo.Month} AND DAY([CompleteDate]) BETWEEN {model.CompleteDateFrom.Day} AND {model.CompleteDateTo.Day})";

            if (model.Dept != string.Empty && model.Dept != null)
                queryString += $" AND [Dept] = '{model.Dept}'";

            if (model.Status != string.Empty && model.Status != null)
                queryString += $" AND [Status] = '{model.Status}'";

            if (model.Title != string.Empty && model.Title != null)
                queryString += $" AND [Title] like '%{model.Title}%'";

            dt = GetDataInDatabase.GetData(queryString);

            ViewData.Add("Items", JsonConvert.SerializeObject(dt));
            ViewData.Add("Page", 0);
            ViewData.Add("MaxPage", 0);

            try
            {
                if (GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]") == null ||
                    GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [dbo].[StatusName]") == null)
                    throw new Exception();


                ViewData["Dept"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [opt].[Departments]"));
                ViewData["Status"] = JsonConvert.SerializeObject(GetDataInDatabase.GetData("SELECT [ID], [Name] FROM [dbo].[StatusName]"));
            }
            catch (Exception)
            {
                ErrorViewModel errorModel = new ErrorViewModel()
                {
                    RequestId = "001DB-ListFormsSearch"
                };

                return RedirectToAction("MyError", "Home", errorModel);
            }

            return View("ListForms");
        }
    }
}
