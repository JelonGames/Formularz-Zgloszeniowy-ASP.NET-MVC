using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;

namespace FormuarzRefresh.Function
{
    public static class GetDataInDatabase
    {
        public static DataTable GetData(string queryString)
        {
            JObject json = GetJson.Start();
            SqlConnection db;
            DataTable dt = new DataTable();

            try
            {
                db = new SqlConnection((string)json["DatabaseConnection"]);
            }
            catch (ArgumentException ex)
            {
                ErrorLogSave.Save(ex.Message, ex.GetType().Name);
                return null;
            }
            catch (Exception ex)
            {
                ErrorLogSave.Save(ex.Message, ex.GetType().Name);
                return null;
            }

            try
            {
                db.Open();
                SqlCommand query = new SqlCommand(queryString, db);
                var reader = query.ExecuteReader();

                dt.Load(reader);
                db.Close();
            }
            catch(Exception ex)
            {
                db.Close();
                ErrorLogSave.Save(ex.Message, ex.GetType().Name);
                return null;
            }

            return dt;
        }
    }
}
