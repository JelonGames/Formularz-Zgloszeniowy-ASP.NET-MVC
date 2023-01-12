using FormuarzRefresh.Function;
using FormuarzRefresh.Models;
using FormuarzRefresh.Models.FormModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Text.RegularExpressions;

namespace FormuarzRefresh.Controllers.CRMForms
{
    public class SelectDetailViewFormsController : Controller
    {
        private DataTable dt = new DataTable();
        private DataTable status = new DataTable();

        private LogOnPerson user;
        private readonly JObject json = GetJson.Start();

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

        [Route("/ViewFormsDetails/{id:int}", Name = "ViewFormsDetails")]
        public IActionResult ViewFormsDetails(int id)
        {
            if (!IsLogon())
            {
                TempData["IDForms"] = id;
                return RedirectToAction("LoginPage", "LogOn");
            } 

            string queryString = $"SELECT [ID], [SendDate], [CompleteDate], [IDStatus], [Status], [IDUser], [User], [IDDept], [Dept], [Title], [XML], [RealizedBy], [RealizedByName], [ConfirmBy], [ConfirmByName], [Version], [IDType], [TypeName] FROM [dbo].[ViewDetailsAllForms] WHERE [ID] = {id}";

            dt = GetDataInDatabase.GetData(queryString);

            TempData.Keep("LogOnPerson");
            LogOnPerson person = JsonConvert.DeserializeObject<LogOnPerson>(TempData["LogOnPerson"].ToString());
            
            if(person.IsHR)
            {
                queryString = $"SELECT TOP (1000) [ID] ,[Name] FROM [dbo].[StatusName] Where [ID] not in (1, 5, 6)";
            }
            else if(person.IsAdmin)
            {
                queryString = $"SELECT TOP (1000) [ID] ,[Name] FROM [dbo].[StatusName] Where [ID] not in (1, 2, 3)";
            }
            else
            {
                queryString = "SELECT -1 as 'ID' ,'Brak Uprawnień' as 'Name'";
            }
            

            status = GetDataInDatabase.GetData(queryString);

            if (dt.Rows.Count == 0)
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    RequestId = "002DB-ViewFormsDetails-" + id
                };

                return RedirectToAction("MyError", "Home", model);
            }
            if (status.Rows.Count == 0)
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    RequestId = "004DB-ViewFormsDetails-StatusName-" + id
                };

                return RedirectToAction("MyError", "Home", model);
            }

            TempData["Status"] = JsonConvert.SerializeObject(status);

            TempData["dataDB"] = null;
            TempData["dataDB"] = JsonConvert.SerializeObject(dt);

            int IDType = (int)dt.Rows[0]["IDType"];
            if (IDType == 1)
            {
                return RedirectToAction("AddUser");
            }
            else if(IDType == 2)
            {
                return RedirectToAction("ModUser");
            }
            else if (IDType == 3)
            {
                return RedirectToAction("DelUser");
            }
            else if (IDType == 4)
            {
                return RedirectToAction("ResPassword");
            }
            else if (IDType == 5)
            {
                return RedirectToAction("Res");
            }
            else
            {
                ErrorViewModel model = new ErrorViewModel()
                {
                    RequestId = "003DB-SelectForms-" + id
                };

                return RedirectToAction("MyError", "Home", model);
            }
        }

        public IActionResult AddUser()
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            TempData.Keep("dataDB");
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(TempData["dataDB"].ToString());

            NewPerson model = XmlConverter.ConvertXmlStringtoObject<NewPerson>(dt.Rows[0]["XML"].ToString());

            ViewData["Form"] = JsonConvert.SerializeObject(dt);
            TempData.Keep("Status");
            ViewData["Status"] = TempData["Status"];
            return View("AddUser", model);
        }
        
        public IActionResult ModUser()
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            TempData.Keep("dataDB");
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(TempData["dataDB"].ToString());

            ModPerson model = XmlConverter.ConvertXmlStringtoObject<ModPerson>(dt.Rows[0]["XML"].ToString());

            ViewData["Form"] = JsonConvert.SerializeObject(dt);
            TempData.Keep("Status");
            ViewData["Status"] = TempData["Status"];
            return View("ModUser", model);
        }

        public IActionResult DelUser()
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            TempData.Keep("dataDB");
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(TempData["dataDB"].ToString());

            DelPerson model = XmlConverter.ConvertXmlStringtoObject<DelPerson>(dt.Rows[0]["XML"].ToString());

            ViewData["Form"] = JsonConvert.SerializeObject(dt);
            TempData.Keep("Status");
            ViewData["Status"] = TempData["Status"];
            return View("DelUser", model);
        }

        public IActionResult ResPassword()
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            TempData.Keep("dataDB");
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(TempData["dataDB"].ToString());

            RestartPassword model = XmlConverter.ConvertXmlStringtoObject<RestartPassword>(dt.Rows[0]["XML"].ToString());

            ViewData["Form"] = JsonConvert.SerializeObject(dt);
            TempData.Keep("Status");
            ViewData["Status"] = TempData["Status"];
            return View("RestartPassword", model);
        }

        public IActionResult Res()
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            TempData.Keep("dataDB");
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(TempData["dataDB"].ToString());

            Resources model = XmlConverter.ConvertXmlStringtoObject<Resources>(dt.Rows[0]["XML"].ToString());

            ViewData["Form"] = JsonConvert.SerializeObject(dt);
            TempData.Keep("Status");
            ViewData["Status"] = TempData["Status"];
            return View("Resources", model);
        }

        [HttpPost]
        [Route("/ChangeStatus/{id:int}", Name = "ChangeStatus")]
        public IActionResult ChangeStatus(int id)
        {
            if (!IsLogon())
                return RedirectToAction("LoginPage", "LogOn");

            TempData.Keep("LogOnPerson");
            LogOnPerson person = JsonConvert.DeserializeObject<LogOnPerson>(TempData["LogOnPerson"].ToString());

            Dictionary<int, bool> canChangeStatus = new Dictionary<int, bool>()
            {
                //Wartość intowa oznacza IDstatusu z bazy
                {-1, false }, //Wartość pusta
                {2, false }, //Nie zatwierdzone
                {3, false }, //Zatwierdzone
                {4, false }, //Błędnie Wypełnione
                {5, false }, //W Trakcie Realizacji
                {6, false }  //Gotowe
                //Słownik ten jest potrzebny do sprawdzenia na jaki satus można dokonać zmiany
            };

            string queryString = $"SELECT [IDStatus] FROM [dbo].[ViewDetailsAllForms] where [ID] = {id}";
            DataTable IDStatusDT = new DataTable();

            IDStatusDT = GetDataInDatabase.GetData(queryString);
            int idStatus = (int)IDStatusDT.Rows[0][0];

            if(idStatus == 1)
            {
                canChangeStatus[2] = true;
                canChangeStatus[3] = true;
                canChangeStatus[4] = true;
            }
            else if(idStatus == 3)
            {
                canChangeStatus[5] = true;
            }
            else if(idStatus == 5)
            {
                canChangeStatus[4] = true;
                canChangeStatus[6] = true;
            }

            int idStatusNew = Convert.ToInt32(Request.Form["StatusSelect"].ToString());

            if (!canChangeStatus[idStatusNew])
            {
                TempData["errorStatusChange"] = "Nie można zmienić statusu";
                return RedirectToRoute("ViewFormsDetails", new { @id = id });
            }

            if(!ChangeStatusForms.Change(id, idStatusNew, person.Login, person.IsHR))
            {
                TempData["errorStatusChange"] = "Nie udało się zmienić statusu";
                return RedirectToRoute("ViewFormsDetails", new { @id = id });
            }

            Console.WriteLine(Request.Form["StatusSelect"].ToString());

            //Wysłanie maila
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();
            data.Add(new Dictionary<string, string>
            {
                { "subject", "Zmieniono status formularza " },
                { "body", json["MailProperty"]["BodyMail"]["ChangeStatus"]["ToUser"].ToString() },
                { "to", string.Empty }
            });

            if (user.IsHR && idStatusNew == 3)
                data.Add(new Dictionary<string, string>
                {
                    { "subject", "Zmieniono status formularza " },
                    { "body", json["MailProperty"]["BodyMail"]["ChangeStatus"]["ToHelpDesk"].ToString() },
                    { "to", json["MailProperty"]["MailTo"]["HelpDesk"].ToString() }
                });

            foreach(Dictionary<string, string> item in data)
            {
                if(!SendMail(id, item))
                {
                    ErrorViewModel error = new ErrorViewModel()
                    {
                        RequestId = "006BD-SendError"
                    };

                    return RedirectToAction("SendApproval", "Home", error);
                }
            }

            return RedirectToRoute("ViewFormsDetails", new { @id = id });
        }

        [Route("/ResendMail/{id:int}", Name = "ResendMail")]
        public IActionResult ResendMail(int id)
        {
            dt = GetDataInDatabase.GetData($"SELECT * FROM [dbo].[ViewDetailsAllForms] WHERE [ID] = {id}");
            List<Dictionary<string, string>> data = new List<Dictionary<string, string>>();

            int idUser = int.Parse(dt.Rows[0]["IDUSER"].ToString());
            DataTable dtUser = GetDataInDatabase.GetData($"SELECT [Email] FROM [dbo].[Users] WHERE [ID] = {idUser}");

            int RealizeBy = int.Parse(dt.Rows[0]["RealizedBy"].ToString());
            int ConfirmBy = int.Parse(dt.Rows[0]["ConfirmBy"].ToString());

            if (ConfirmBy == 1 && RealizeBy == 1)
            {
                string xml = dt.Rows[0]["XML"].ToString();
                Regex reg = new Regex("(?<topper><Message>)(?<text>.{1,20})(?<fotter></Message>)");

                Match match = reg.Match(xml);
                if (!match.Success || match.Groups["text"].ToString() == string.Empty)
                {
                    //User
                    data.Add(new Dictionary<string, string>
                    {
                        { "subject", "Został wysłany formularz " },
                        { "body", json["MailProperty"]["BodyMail"]["NewForm"]["ToUser"].ToString() },
                        { "to", dtUser.Rows[0]["Email"].ToString() }
                    });
                    //HR
                    data.Add(new Dictionary<string, string>
                    {
                        { "subject", "Został wysłany formularz " },
                        { "body", json["MailProperty"]["BodyMail"]["NewForm"]["ToHR"].ToString() },
                        { "to", json["MailProperty"]["MailTo"]["HR"].ToString() }
                    });
                }
                else if(match.Success && match.Groups["text"].ToString() == "Zarząd")
                {
                    //user
                    data.Add(new Dictionary<string, string>
                    {
                        { "subject", "Został wysłany formularz " },
                        { "body", json["MailProperty"]["BodyMail"]["NewForm"]["ToUser"].ToString() },
                        { "to", dtUser.Rows[0]["Email"].ToString() }
                    });
                    //HR
                    data.Add(new Dictionary<string, string>
                    {
                        { "subject", "Został wysłany formularz " },
                        { "body", json["MailProperty"]["BodyMail"]["NewForm"]["ToHRalt"].ToString() },
                        { "to", json["MailProperty"]["MailTo"]["HR"].ToString() }
                    });
                    //Zarząd
                    data.Add(new Dictionary<string, string>
                    {
                        { "subject", "Został wysłany formularz " },
                        { "body", json["MailProperty"]["BodyMail"]["NewForm"]["ToHRalt"].ToString() },
                        { "to", json["MailProperty"]["MailTo"]["Zarzad"].ToString() }
                    });
                }
                else if (match.Success && match.Groups["text"].ToString() == "Jarosław Przytuła")
                {
                    //user
                    data.Add(new Dictionary<string, string>
                    {
                        { "subject", "Został wysłany formularz " },
                        { "body", json["MailProperty"]["BodyMail"]["NewForm"]["ToUser"].ToString() },
                        { "to", dtUser.Rows[0]["Email"].ToString() }
                    });
                    //HR
                    data.Add(new Dictionary<string, string>
                    {
                        { "subject", "Został wysłany formularz " },
                        { "body", json["MailProperty"]["BodyMail"]["NewForm"]["ToHRalt"].ToString() },
                        { "to", json["MailProperty"]["MailTo"]["HR"].ToString() }
                    });
                    //Jarosław Przytuła
                    data.Add(new Dictionary<string, string>
                    {
                        { "subject", "Został wysłany formularz " },
                        { "body", json["MailProperty"]["BodyMail"]["NewForm"]["ToHRalt"].ToString() },
                        { "to", json["MailProperty"]["MailTo"]["Dyrektor"].ToString() }
                    });
                }
            }
            else if(ConfirmBy != 1 && RealizeBy == 1)
            {
                //User
                data.Add(new Dictionary<string, string>
                    {
                        { "subject", "Zmieniono status formularza " },
                        { "body", json["MailProperty"]["BodyMail"]["ChangeStatus"]["ToUser"].ToString() },
                        { "to", dtUser.Rows[0]["Email"].ToString() }
                    });
                if (int.Parse(dt.Rows[0]["IDStatus"].ToString()) == 3)
                {
                    //HR
                    data.Add(new Dictionary<string, string>
                    {
                        { "subject", "Zmieniono status formularza " },
                        { "body", json["MailProperty"]["BodyMail"]["ChangeStatus"]["ToHelpDesk"].ToString() },
                        { "to", json["MailProperty"]["MailTo"]["HelpDesk"].ToString() }
                    });
                }
            }
            else if(ConfirmBy != 1 && RealizeBy != 1)
            {
                //User
                data.Add(new Dictionary<string, string>
                    {
                        { "subject", "Zmieniono status formularza " },
                        { "body", json["MailProperty"]["BodyMail"]["ChangeStatus"]["ToUser"].ToString() },
                        { "to", dtUser.Rows[0]["Email"].ToString() }
                    });
            }

            foreach (Dictionary<string, string> item in data)
            {
                if (!SendMail(id, item))
                {
                    ErrorViewModel error = new ErrorViewModel()
                    {
                        RequestId = "006BD-SendError"
                    };

                    return RedirectToAction("SendApproval", "Home", error);
                }
            }

            return RedirectToRoute("ViewFormsDetails", new { @id = id });
        }

        private bool SendMail(int id, Dictionary<string, string> data)
        {
            dt = GetDataInDatabase.GetData($"Select * From [dbo].[Forms] where [ID] = {id}");
            int senderID = int.Parse(dt.Rows[0]["IDUser"].ToString());
            DataTable senderDT = GetDataInDatabase.GetData($"Select * From [dbo].[Users] where [ID] = {senderID}");


            string subject = data["subject"] + "\"" + dt.Rows[0]["Title"].ToString() + "\"";
            string body = data["body"];

            string to;
            if (data["to"] == string.Empty)
                to = senderDT.Rows[0]["Email"].ToString();
            else
                to = data["to"];

            if(!MailSender.Send(
                subject,
                body,
                new List<string>
                {
                    to
                },
                idForm: id
                ))
            {
                return false;
            }

            return true;
        }
    }
}
