using FormuarzRefresh.Models.FormModel.GlobalFormModels;
using System.ComponentModel;
using System.Globalization;

namespace FormuarzRefresh.Models.FormModel
{
    public class ModPerson : DataAccess
    {
        [DisplayName("Imie i Nazwisko:")]
        public string UserName { get; set;} = string.Empty;

        [DisplayName("Telefon służbowy:")]
        public bool BusinessPhoneCheckbox { get; set; }
        public int BusinessPhone { get; set; }
        [DisplayName("Opisany na telefonie")]
        public bool DescribeOnPhone { get; set; }

        [DisplayName("Podpięcie do pętli:")]
        public bool VoipProjectCheckbox { get; set; }
        public string VoipProject { get; set; } = string.Empty;

        [DisplayName("Przedłużenie ważności konta:")]
        public bool ExpandActiveAccount { get; set; }
        public DateTime ExpandDateActive { get; set; }

        [DisplayName("Zmiana nazwiska w systemie KPL:")]
        public bool ChangeLastName { get; set; }
        public string NewLastName { get; set; } = string.Empty;

        [DisplayName("Przekierowanie skrzynki pocztowej")]
        public bool RedirectEmail { get; set; }
        public string ToEmail { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string MethodCopy { get; set; } = string.Empty;
        public DateTime WhenRedirect { get; set; }

        [DisplayName("Dostęp do skrzynki pocztowej innego użytkownika")]
        public bool AccessOtherEmail { get; set; }
        public string WhoseEmail { get; set; } = string.Empty;
        public string CommentEmail { get; set; } = string.Empty;

        [DisplayName("Dostęp do dysku z innego użytkownika")]
        public bool AccessOtherDiskZ { get; set; }
        public string WhoseDiskZ { get; set; } = string.Empty;
        public DateTime WhenAccessDiskZ { get; set; }
        public string PermissionDiskZ { get; set; } = string.Empty;

        [DisplayName("Powiększenie Dysku Z:")]
        public bool GrowupDiskZ { get; set; }
        public string WhyGrowupDiskZ { get; set; } = string.Empty;

        [DisplayName("Dostęp do internetu:")]
        public bool InternetAccessCheckbox { get; set; }
        public string InternetAccess { get; set; } = string.Empty;

        [DisplayName("Dodatkowe strony internetowe:")]
        public bool MorePageCheckbox { get; set; }
        public string MorePage { get; set; } = string.Empty;

        [DisplayName("Dostęp do pomieszczeń:")]
        public bool RoomAccessCheckbox { get; set; }
        [DisplayName("Room1")]
        public bool Room1Access { get; set; }
        [DisplayName("Room2")]
        public bool Room2Access { get; set; }
    }
}
