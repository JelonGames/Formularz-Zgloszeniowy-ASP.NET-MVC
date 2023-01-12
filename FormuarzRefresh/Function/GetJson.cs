using Newtonsoft.Json.Linq;

namespace FormuarzRefresh.Function
{
    public static class GetJson
    {
        public static JObject Start()
        {
            string dataJson = string.Empty;
            try
            {
                dataJson = File.ReadAllText(@".\appsettings.json");
            }
            catch(Exception ex)
            {
                ErrorLogSave.Save(ex.Message, ex.GetType().Name);
            }

            JObject json = JObject.Parse(dataJson);
            return json;
        }
    }
}
