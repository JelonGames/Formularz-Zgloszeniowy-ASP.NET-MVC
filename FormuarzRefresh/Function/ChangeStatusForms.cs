using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;

namespace FormuarzRefresh.Function
{
    public static class ChangeStatusForms
    {
        public static bool Change(int id, int idStatus, string loginUser, bool isHR)
        {
            JObject json = GetJson.Start();
            SqlConnection db;


            try
            {
                db = new SqlConnection((string)json["DatabaseConnection"]);
            }
            catch (ArgumentException ex)
            {
                ErrorLogSave.Save(ex.Message, ex.GetType().Name);
                return false;
            }
            catch (Exception ex)
            {
                ErrorLogSave.Save(ex.Message, ex.GetType().Name);
                return false;
            }

            try
            {
                db.Open();
                SqlCommand query = new SqlCommand()
                {
                    CommandText = "dbo.ChangeStatusForms",
                    CommandType = CommandType.StoredProcedure,
                    Connection = db
                };

                query.Parameters.Add("@id", SqlDbType.Int).Value = id;
                query.Parameters.Add("@idStatus", SqlDbType.Int).Value = idStatus;
                query.Parameters.Add("@login", SqlDbType.VarChar, 50).Value = loginUser;
                query.Parameters.Add("@isHR", SqlDbType.Bit).Value = isHR;

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
    }
}
