using Newtonsoft.Json.Linq;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace FormuarzRefresh.Function
{
    public static class MailSender
    {
        public static bool Send(string subject, string body, List<string> to, string senderForm = "", int idForm = -1)
        {
            JObject json = GetJson.Start();

            MailParams mailParams = new MailParams()
            {
                Domain = json["MailProperty"]["Settings"]["Domain"].ToString(),
                HostSmtp = json["MailProperty"]["Settings"]["Server"].ToString(),
                Port = int.Parse(json["MailProperty"]["Settings"]["Port"].ToString()),
                EnableSsl = bool.Parse(json["MailProperty"]["Settings"]["EnableSSl"].ToString()),
                Username = json["MailProperty"]["Settings"]["Username"].ToString(),
                Login = json["MailProperty"]["Settings"]["Login"].ToString(),
                Password = json["MailProperty"]["Settings"]["Password"].ToString()
            };

            string link = string.Empty;
            if(senderForm != "" )
            {
                link = GetLink(senderForm, json);
            }
            else if(idForm != -1)
            {
                link = json["MailProperty"]["LinkForm"].ToString() + idForm.ToString();
            }

            if (link == string.Empty)
            {
                ErrorLogSave.Save("I don't find link", "SearchLink");
                return false;
            }

            body = body + "<br />" + link + " <br /><br /><b>Ten Mail został wygenerowany automatycznie, proszę na niego nie odpowiadać.</b>";

            MailMessage message = new MailMessage();
            try
            {
                message.From = new MailAddress(mailParams.Login, mailParams.Password);
            }
            catch(Exception ex)
            {
                ErrorLogSave.Save(ex.Message, ex.GetType().Name);
                return false;
            }
            

            foreach(string mail in to)
            {
                message.To.Add(new MailAddress(mail));
            }
            message.Subject = subject;
            message.Body = body;
            message.BodyEncoding = Encoding.UTF8;
            message.SubjectEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient
            {
                Host = mailParams.HostSmtp,
                Port = mailParams.Port,
                EnableSsl = mailParams.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    mailParams.Username,
                    mailParams.Password,
                    mailParams.Domain)
            };


            try
            {
                smtpClient.Send(message);
            }
            catch(Exception ex)
            {
                smtpClient.Dispose();
                message.Dispose();
                ErrorLogSave.Save(ex.Message, ex.GetType().Name);

                return false;
            }

            smtpClient.Dispose();
            message.Dispose();

            return true;
        }

        private static string GetLink(string SenderForm, JObject Json)
        {
            DataTable dt = new DataTable();
            dt = GetDataInDatabase.GetData($"select Top(1) [ID] from [dbo].[ViewDetailsAllForms] where [User] = '{SenderForm}' order by [SendDate] desc");

            if (dt == null || dt.Rows.Count == 0)
                return string.Empty;

            int id = int.Parse(dt.Rows[0]["ID"].ToString());

            return Json["MailProperty"]["LinkForm"].ToString() + id;
        }
    }

    public class MailParams
    {
        public string Domain { get; set; }
        public string HostSmtp { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Username { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
