using FormuarzRefresh.Models.FormModel.GlobalFormModels;
using System.ComponentModel;

namespace FormuarzRefresh.Models.FormModel
{
    public class TempResources : FormalData
    {
        public AddOrDelResources AddOrDelResources { get; set; }

        //Dodawanie Zasobu
        [DisplayName("Nazwa/Cel Zasobu:")]
        public string NameAndGoalResources_Add { get; set; } = string.Empty;

        [DisplayName("Lista użytkowników który mają mieć dostęp do zasobu oraz poziom uprawnień:")]
        public string UserPermission_Add { get; set; } = string.Empty;

        [DisplayName("Dodatkowe informacje:")]
        public string MoreInformation_Add { get; set; } = string.Empty;

        //Usuwanie zasobu
        [DisplayName("Nazwa zasobu:")]
        public string NameResources_Del { get; set; } = string.Empty;

        [DisplayName("Ścieżka zasobu (serwer):")]
        public string PathResources_Del { get; set; } = string.Empty;

        [DisplayName("Zawartość bazy danych")]
        public string DatabaseData_Del { get; set; } = string.Empty;

        [DisplayName("Dodatkowe informacje:")]
        public string MoreInformation_Del { get; set; } = string.Empty;
    }
}
