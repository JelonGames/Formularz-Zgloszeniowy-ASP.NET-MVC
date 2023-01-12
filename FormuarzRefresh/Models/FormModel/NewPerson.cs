using FormuarzRefresh.Models.FormModel.GlobalFormModels;
using System.ComponentModel;

namespace FormuarzRefresh.Models.FormModel
{
    public class NewPerson : DataAccess
    {
        [DisplayName("Imie i Nazwisko:")]
        public string UserName { get; set; } = string.Empty;

        [DisplayName("Telefon służbowy:")]
        public int BusinessPhone { get; set; } = 713747;
        [DisplayName("Opisany na telefonie")]
        public bool DescribeOnPhone { get; set; }

        [DisplayName("Pętla do której ma być dodany użytkownik:")]
        public string VoipProject { get; set; } = string.Empty;

        [DisplayName("Data Aktywacji:")]
        public DateTime ActiveDate { get; set; }

        [DisplayName("Data Deaktywacji (Domyślnie 15 dni):")]
        public DateTime DeactiveDate { get; set; }

        [DisplayName("Możliwość logowania na komputery w domenie Lexus:")]
        public string ComputerAccess { get; set; } = string.Empty;

        [DisplayName("Dostęp do Internetu:")]
        public string InternetAccess { get; set; } = string.Empty;

        [DisplayName("Room1")]
        public bool Room1Access { get; set; }
        [DisplayName("Room2")]
        public bool Room2Access { get; set; }
    }
}
