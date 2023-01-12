using FormuarzRefresh.Models.FormModel.GlobalFormModels;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace FormuarzRefresh.Models.FormModel
{
    public class DelPerson : DataAccess
    {
        [DisplayName("Imie i Nazwisko")]
        public string UserName { get; set; } = string.Empty;

        [DisplayName("Usunięcie użytkownika")]
        public bool DelUser { get; set; } = false;

        [DisplayName("Usunięcie dostępów")]
        public bool DelAccess { get; set; } = false;

        [DisplayName("Usunięcie dostępów do drukarek")]
        public bool DelAccessPrinters { get; set; } = false;

        [DisplayName("Usunięcie dostępu do mailbox")]
        public bool DelAccessToMailbox { get; set; } = false;

        [DisplayName("Usunięcie skrzynki pocztowej")]
        public bool DelEmail { get; set; } = false;

        [DisplayName("Usunięcie dysku Z")]
        public bool DelDiskZ { get; set; } = false;

        [DisplayName("Usunięcie archiwóm poczty")]
        public bool DelArchiveEmail { get; set; } = false;

        [DisplayName("Usunięce dostępu do pomieszczeń")]
        public bool DelAccessRoom { get; set; } = false;
    }
}
