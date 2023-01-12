using FormuarzRefresh.Function;
using FormuarzRefresh.Models;
using System.DirectoryServices.AccountManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace FormuarzRefresh.Controllers
{
    public class LogOnController : Controller
    {
        //pobranie jsona
        private readonly JObject json = GetJson.Start();

        //zmienne użytkownika do autoryzacji z domeną
        private string loginLDAP = string.Empty;
        private string passwordLDAP = string.Empty;

        //String do połączenia się z bazą
        string sqlStringConnection = string.Empty;

        public LogOnController()
        {
            sqlStringConnection = (string)json["DatabaseConnection"];
        }

        //funkcja do sprawdzania czy uzytkownik jest zalogowany
        private bool IsLogon()
        {
            object? person = null;
            if(TempData.TryGetValue("LogOnPerson", out person))
            {
                TempData.Keep("LogOnPerson");
                ViewData["Person"] = person.ToString();
                return true;
            }

            ViewData["Person"] = string.Empty;
            return false;
        }

        public IActionResult LoginPage()
        {
            if (IsLogon())
                return RedirectToAction("Index", "Home");

            return View("LoginPage");
        }

        public IActionResult LogOut()
        {
            if (!IsLogon())
                return View("LoginPage");
            TempData.Remove("LogOnPerson");
            TempData.Clear();
            return RedirectToAction("Index", "Home");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogOn(LoginData data)
        {
            if (IsLogon())
                return RedirectToAction("Index", "Home");

            GetLDAPUser();

            LogOnPerson logOnPerson; //Model logującego się uzytkownika

            using (PrincipalContext? context = new PrincipalContext(ContextType.Domain, "ELF", loginLDAP, passwordLDAP))
            {
                bool isValid;
                try
                {
                    isValid = context.ValidateCredentials(data.Login, data.Password, ContextOptions.ServerBind);
                }
                catch (ArgumentException)
                {
                    // Błąd logowania
                    ViewData["LogError"] = "Błędna nazwa logowania lub hasło";
                    return View("LoginPage");
                }

                if (isValid)
                {
                    using(UserPrincipal? user = UserPrincipal.FindByIdentity(context, data.Login))
                    {
                        if(user.IsMemberOf(GroupPrincipal.FindByIdentity(context, "Grupa1")))
                        {
                            // użytkownika jest adminem
                            logOnPerson = SetLogOnPerson_Model(user, true, false);
                        }
                        else if(user.IsMemberOf(GroupPrincipal.FindByIdentity(context, "Grupa2")) ||
                            user.IsMemberOf(GroupPrincipal.FindByIdentity(context, "Grupa3")))
                        {
                            // użytkownik jest HR
                            logOnPerson = SetLogOnPerson_Model(user, false, true);
                        }
                        else if(user.IsMemberOf(GroupPrincipal.FindByIdentity(context, "Grupa4")))
                        {
                            // jest zwykłym użytkownikiem
                            logOnPerson = SetLogOnPerson_Model(user, false, false);
                        }
                        else
                        {
                            // Brak Przynależności do odpowiedniej grupy
                            ViewData["LogError"] = "Brak dostepu";
                            return View("LoginPage");
                        }

                        user.Dispose();
                        context.Dispose();
                    }
                }
                else
                {
                    // Błąd logowania
                    ViewData["LogError"] = "Błędna nazwa logowania lub hasło";
                    return View("LoginPage");
                }
            }

            //Sprawdzenie czy użytkownik już istnieje w bazie
            DataTable dt = new DataTable();
            dt = GetDataInDatabase.GetData($"select * from [dbo].[Users] where [UserName] = '{data.Login}'");
            if(dt.Rows.Count == 0)
            {
                bool addUser = AddUserToDatabase(logOnPerson);
                if (!addUser)
                {
                    ViewData["LogError"] = "Błąd połaczenia z bazą danych.";
                    return View("LoginPage");
                }
            }
            else if(dt == null)
            {
                ViewData["LogError"] = "Błąd połaczenia z bazą danych.";
                return View("LoginPage");
            }
            else
            {
                bool updateUser = UpdateUser(logOnPerson);
                if (!updateUser)
                {
                    ViewData["LogError"] = "Błąd połaczenia z bazą danych.";
                    return View("LoginPage");
                }
            }

            //Zapisywanie do bazy Logu z logowania i prawdzanie czy zostalo dodane porpawnie
            bool LogIsInDatabase = SaveLogInDataBase(data);
            if (!LogIsInDatabase)
            {
                //Błąd dodawania logów do bazy danych
                ViewData["LogError"] = "Błąd połaczenia z bazą danych.";
                return View("LoginPage");
            }

            //stworzenie sesji
            TempData["LogOnPerson"] = JsonConvert.SerializeObject(logOnPerson);

            object idForm;
            if (TempData.TryGetValue("IDForms", out idForm))
            {
                idForm = int.Parse(idForm.ToString());
                return RedirectToRoute("ViewFormsDetails", new { @id = idForm });
            }

            return RedirectToAction("Index", "Home");
        }

        //Dodawanie użytkownika do bazy
        private bool AddUserToDatabase(LogOnPerson logOnPerson)
        {
            SqlConnection db = new SqlConnection(sqlStringConnection);
            try
            {
                db.Open();
                SqlCommand query = new SqlCommand
                {
                    CommandText = "dbo.AddUser",
                    CommandType = CommandType.StoredProcedure,
                    Connection = db
                };
                #region Parameters Procedure
                query.Parameters.Add("@UserName", SqlDbType.VarChar).Value = logOnPerson.Login;
                query.Parameters.Add("FullName", SqlDbType.VarChar).Value = logOnPerson.FirstName + " " + logOnPerson.LastName;
                query.Parameters.Add("@Email", SqlDbType.VarChar).Value = logOnPerson.Email;
                #endregion

                query.ExecuteNonQuery();

                db.Close();
            }
            catch(Exception ex)
            {
                db.Close();
                ErrorLogSave.Save(ex.Message, ex.GetType().Name);
                return false;
            }

            return true;
        }

        //Aktualizowanie użytkownika w bazie
        private bool UpdateUser(LogOnPerson logOnPerson)
        {
            SqlConnection db = new SqlConnection(sqlStringConnection);
            try
            {
                db.Open();
                SqlCommand query = new SqlCommand
                {
                    CommandText = "dbo.UpdateUser",
                    CommandType = CommandType.StoredProcedure,
                    Connection = db
                };
                #region Parameters Procedure
                query.Parameters.Add("@UserName", SqlDbType.VarChar).Value = logOnPerson.Login;
                query.Parameters.Add("FullName", SqlDbType.VarChar).Value = logOnPerson.FirstName + " " + logOnPerson.LastName;
                query.Parameters.Add("@Email", SqlDbType.VarChar).Value = logOnPerson.Email;
                #endregion

                query.ExecuteNonQuery();

                db.Close();
            }
            catch (Exception ex)
            {
                db.Close();
                ErrorLogSave.Save(ex.Message, ex.GetType().Name);
                return false;
            }

            return true;
        }

        //Dodawanie logu z logowania do bazy
        private bool SaveLogInDataBase(LoginData data)
        {
            SqlConnection db = new SqlConnection(sqlStringConnection);
            try
            {
                db.Open();
                
                SqlCommand query = new SqlCommand($"INSERT INTO [dbo].[LoginLogs] ([Date], [Login]) VALUES (CURRENT_TIMESTAMP ,'{data.Login}')", db);
                query.ExecuteNonQuery();

                query.Dispose();
                db.Close();
            }
            catch(Exception ex)
            {
                db.Close();
                ErrorLogSave.Save(ex.Message, ex.GetType().Name);
                return false;
            }

            return true;
        }

        //Ustawianie modelu użytkownika który się loguje
        private static LogOnPerson SetLogOnPerson_Model(UserPrincipal user, bool isAdmin, bool isHR)
        {
            LogOnPerson logOnPerson = new LogOnPerson();
            logOnPerson.Login = user.SamAccountName;
            string[] displayName = user.DisplayName.ToString().Split(" ");
            logOnPerson.FirstName = displayName[0];
            logOnPerson.LastName = displayName[1];
            logOnPerson.Email = user.EmailAddress.ToString();
            logOnPerson.IsAdmin = isAdmin;
            logOnPerson.IsHR = isHR;

            return logOnPerson;
        }

        //Pobranie danych o użytkowniku LDAP
        private void GetLDAPUser()
        {
            loginLDAP = (string)json["LdapUser"]["SamAccountName"];
            passwordLDAP = (string)json["LdapUser"]["Password"];
        }
    }
}
