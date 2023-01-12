using System.ComponentModel;

namespace FormuarzRefresh.Models.FormModel.GlobalFormModels
{
    public class DataAccess : MailBox
    {
        [DisplayName("Baza1")]
        public bool Baza1 { get; set; }
        [DisplayName("Baza2")]
        public bool Baza2 { get; set; }
        [DisplayName("Baza3")]
        public bool Baza3 { get; set; }
        [DisplayName("Inne:")]
        public string OtherDatabase { get; set; } = string.Empty;

        [DisplayName("Dostęp do drukarek")]
        public List<string> Printers { get; set; } = new List<string>();

        [DisplayName("Inne Aplikacje")]
        public bool OtherAppsCheckbox { get; set; }
        public List<string> SelectedApps { get; set; } = new List<string> { string.Empty };
        public string OtherApps { get; set; } = string.Empty;

        [DisplayName("Specjalne zasoby sieciowe na serwerach (Inne niż domyślne dla każdego negocjatora)")]
        public bool NetResourcesCheckbox { get; set; }
        public string NetResources { get; set; } = string.Empty;
        public ReadResources NetResPer { get; set; } = ReadResources.Brak;

        [DisplayName("Foldery publiczne (Inne niż domyślne dla każdego negocjatora)")]
        public bool PublicFolderCheckbox { get; set; }
        public string PathPublicFolder { get; set; } = string.Empty;
        [DisplayName("Odczyt plików:")]
        public bool ReadPublicFolder { get; set; }
        [DisplayName("Tworzenie plików:")]
        public bool CreateFileOnPublicFolder { get; set; }
        [DisplayName("Tworzenie folderów:")]
        public bool CreateDirectoryOnPublicFolder { get; set; }
        [DisplayName("Eydcja:")]
        public PerPublicFolder Editing { get; set; } = PerPublicFolder.Brak;
        [DisplayName("Kasowanie:")]
        public PerPublicFolder Deleting { get; set; } = PerPublicFolder.Brak;

        [DisplayName("Uwagi:")]
        public string Comment { get; set; } = string.Empty;
    }
}
