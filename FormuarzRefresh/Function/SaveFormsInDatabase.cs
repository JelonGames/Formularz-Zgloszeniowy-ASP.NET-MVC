using FormuarzRefresh.Models;
using FormuarzRefresh.Models.FormModel;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace FormuarzRefresh.Function
{
    public static class SaveFormsInDatabase
    {
        public static bool Push(LogOnPerson user, string xmlObject, object objectModel, string stringSqlConnection)
        {
            JObject json = GetJson.Start();
            string departments = string.Empty;
            string title = string.Empty;
            string version = json["ProjectVerson"].ToString();
            int idType = 0;

            if (objectModel.GetType().Name.ToString() == "NewPerson")
            {
                NewPerson model = (NewPerson)objectModel;
                departments = model.Dept;
                title = $"Nowy użytkownik {model.UserName}";
                idType = 1; //NewUser
            }
            else if (objectModel.GetType().Name.ToString() == "ModPerson")
            {
                ModPerson model = (ModPerson)objectModel;
                departments = model.Dept;
                title = $"Modyfikacja użytkownika {model.UserName}";
                idType = 2; //ModUser
            }
            else if (objectModel.GetType().Name.ToString() == "DelPerson")
            {
                DelPerson model = (DelPerson)objectModel;
                departments = model.Dept;
                title = $"Usunięcie użytkownika {model.UserName}";
                idType = 3; //DelUser
            }
            else if (objectModel.GetType().Name.ToString() == "RestartPassword")
            {
                RestartPassword model = (RestartPassword)objectModel;
                departments = model.Dept;
                title = $"Reset hasła użytkownika {model.UserName}";
                idType = 4; //RestartPassword
            }
            else if (objectModel.GetType().Name.ToString() == "Resources")
            {
                Resources model = (Resources)objectModel;
                departments = model.Dept;
                if (model.AddOrDelResources == AddOrDelResources.Add)
                    title = $"Dodawanie nowego zasobu {model.NameAndGoalResources_Add}";
                else if (model.AddOrDelResources == AddOrDelResources.Del)
                    title = $"Usunięcie zasobu {model.NameResources_Del}";
                idType = 5; //Resources
            }

            SqlConnection db = new SqlConnection(stringSqlConnection);
            try
            {
                db.Open();
                SqlCommand query = new SqlCommand
                {
                    CommandText = "dbo.AddForm",
                    CommandType = CommandType.StoredProcedure,
                    Connection = db
                };

                #region Parameters
                query.Parameters.Add("@UserName", SqlDbType.VarChar, 100).Value = user.Login;
                query.Parameters.Add("@Departments", SqlDbType.VarChar, 100).Value = departments;
                query.Parameters.Add("@Title", SqlDbType.NVarChar).Value = title;
                query.Parameters.Add("@XML", SqlDbType.Xml).Value = new SqlXml(XmlReader.Create(new StringReader(xmlObject)));
                query.Parameters.Add("@Version", SqlDbType.VarChar, 10).Value = version;
                query.Parameters.Add("@IDType", SqlDbType.Int).Value = idType;
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
    }
}
