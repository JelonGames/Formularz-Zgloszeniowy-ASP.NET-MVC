using FormuarzRefresh.Function;
using FormuarzRefresh.Models;
using FormuarzRefresh.Models.FormModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection;
using System.Xml;

namespace FormuarzRefresh.Controllers.FunctionControler
{
    public class SaveFormsController : Controller
    {
        // pobranie jsona
        private readonly JObject json = GetJson.Start();

        //adres połączenia do bazy danych
        private string stringSqlConnection;

        //Obiekt zalogowane użytkownika
        private LogOnPerson user;

        public SaveFormsController()
        {
            stringSqlConnection = (string)json["DatabaseConnection"];
        }

        //funkcja do sprawdzania czy uzytkownik jest zalogowany
        private bool IsLogon()
        {
            object? person = null;
            if (TempData.TryGetValue("LogOnPerson", out person))
            {
                TempData.Keep("LogOnPerson");
                ViewData["Person"] = person.ToString();
                user = JsonConvert.DeserializeObject<LogOnPerson>(person.ToString());
                return true;
            }

            ViewData["Person"] = string.Empty;
            return false;
        }

        [HttpPost]
        public IActionResult NewPerson(NewPerson model)
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            //zapis do bazy
            string xml = XmlConverter.ConvertObjectToXMLString(model);
            bool isCorrect = SaveFormsInDatabase.Push(user, xml, model, stringSqlConnection);
            if (!isCorrect)
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    RequestId = "005BD-SaveForms"
                };

                return RedirectToAction("MyError", "Home", error);
            }

            //wysłanie maila
            if(!SendMail(model.Message, model.UserName, model.Sender))
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    RequestId = "006BD-SendError"
                };

                return RedirectToAction("SendApproval", "Home", error);
            }

            return RedirectToAction("SendApproval", "Home", null);
        }

        [HttpPost]
        public IActionResult ModPerson(ModPerson model)
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            //zapis do bazy
            string xml = XmlConverter.ConvertObjectToXMLString(model);
            bool isCorrect = SaveFormsInDatabase.Push(user, xml, model, stringSqlConnection);
            if (!isCorrect)
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    RequestId = "005BD - SaveForms"
                };

                return RedirectToAction("MyError", "Home", error);
            }

            //wysłanie maila
            if (!SendMail(model.Message, model.UserName, model.Sender))
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    RequestId = "006BD-SendError"
                };

                return RedirectToAction("SendApproval", "Home", error);
            }

            return RedirectToAction("SendApproval", "Home", null);
        }

        [HttpPost]
        public IActionResult DelPerson(DelPerson model)
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            //zapis do bazy
            string xml = XmlConverter.ConvertObjectToXMLString(model);
            bool isCorrect = SaveFormsInDatabase.Push(user, xml, model, stringSqlConnection);
            if (!isCorrect)
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    RequestId = "005BD-SaveForms"
                };

                return RedirectToAction("MyError", "Home", error);
            }

            //wysłanie maila
            if (!SendMail(model.Message, model.UserName, model.Sender))
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    RequestId = "006BD-SendError"
                };

                return RedirectToAction("SendApproval", "Home", error);
            }

            return RedirectToAction("SendApproval", "Home", null);
        }

        [HttpPost]
        public IActionResult RestartPassword(RestartPassword model)
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            //zapis do bazy
            string xml = XmlConverter.ConvertObjectToXMLString(model);
            bool isCorrect = SaveFormsInDatabase.Push(user, xml, model, stringSqlConnection);
            if (!isCorrect)
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    RequestId = "005BD-SaveForms"
                };

                return RedirectToAction("MyError", "Home", error);
            }

            //wysłanie maila
            if (!SendMail(model.Message, model.UserName, model.Sender))
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    RequestId = "006BD-SendError"
                };

                return RedirectToAction("SendApproval", "Home", error);
            }

            return RedirectToAction("SendApproval", "Home", null);
        }

        [HttpPost]
        public IActionResult Resources(TempResources model)
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            //zapis do bazy
            Resources newModel = new Resources()
            {
                SendDate = model.SendDate,
                Business = model.Business,
                Dept = model.Dept,
                Sender = model.Sender,
                Message = model.Message,
                AddOrDelResources = model.AddOrDelResources,
                NameAndGoalResources_Add = model.NameAndGoalResources_Add,
                MoreInformation_Add = model.MoreInformation_Add,
                NameResources_Del = model.NameResources_Del,
                PathResources_Del = model.PathResources_Del,
                DatabaseData_Del = model.DatabaseData_Del,
                MoreInformation_Del = model.MoreInformation_Del
            };

            if(model.AddOrDelResources == AddOrDelResources.Add)
            {
                string[] temp = model.UserPermission_Add.Split(';');
                foreach (string user in temp)
                {
                    newModel.UserPermission_Add.Add(user);
                }
            }

            string xml = XmlConverter.ConvertObjectToXMLString(newModel);
            bool isCorrect = SaveFormsInDatabase.Push(user, xml, newModel, stringSqlConnection);
            if (!isCorrect)
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    RequestId = "005BD-SaveForms"
                };

                return RedirectToAction("MyError", "Home", error);
            }

            //wysłanie maila
            string subject = string.Empty;
            if(model.AddOrDelResources == AddOrDelResources.Add)
            {
                subject = model.NameAndGoalResources_Add;
            }
            else
            {
                subject = model.NameResources_Del;
            }

            if (!SendMail(model.Message, subject, model.Sender))
            {
                ErrorViewModel error = new ErrorViewModel()
                {
                    RequestId = "006BD-SendError"
                };

                return RedirectToAction("SendApproval", "Home", error);
            }

            return RedirectToAction("SendApproval", "Home", null);
        }

        private bool SendMail(string Message, string UserName, string Sender)
        {
            bool isCorrect = false;

            if (Message == string.Empty || Message == null)
            {
                isCorrect = MailSender.Send(
                    $"Nowy formularz został wysłany dla {UserName}",
                    json["MailProperty"]["BodyMail"]["NewForm"]["ToHR"].ToString(),
                    new List<string>
                    {
                        json["MailProperty"]["MailTo"]["HR"].ToString()
                    },
                    Sender);
            }
            else
            {
                string secondMail = string.Empty;
                if (Message == "Zarząd")
                    secondMail = json["MailProperty"]["MailTo"]["Zarzad"].ToString();
                else if (Message == "Jarosław Przytuła")
                    secondMail = json["MailProperty"]["MailTo"]["Dyrektor"].ToString();

                isCorrect = MailSender.Send(
                    $"Nowy formularz został wysłany dla {UserName}",
                    json["MailProperty"]["BodyMail"]["NewForm"]["ToHRalt"].ToString(),
                    new List<string>
                    {
                        json["MailProperty"]["MailTo"]["HR"].ToString(),
                        secondMail
                    },
                    Sender);
            }

            if (!isCorrect)
                return isCorrect;

            TempData.Keep("LogOnPerson");
            LogOnPerson user = JsonConvert.DeserializeObject<LogOnPerson>(TempData["LogOnPerson"].ToString());
            isCorrect = MailSender.Send(
                    $"Nowy formularz został wysłany dla {UserName}",
                    json["MailProperty"]["BodyMail"]["NewForm"]["ToUser"].ToString(),
                    new List<string>
                    {
                        user.Email
                    },
                    Sender);

            return isCorrect;
        }
    }
}
