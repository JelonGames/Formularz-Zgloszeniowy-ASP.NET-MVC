using FormuarzRefresh.Models.FormModel.GlobalFormModels;
using System.ComponentModel;

namespace FormuarzRefresh.Models.FormModel
{
    public class RestartPassword : FormalData
    {
        [DisplayName("Imie i Nazwisko")]
        public string UserName { get; set; } = string.Empty;
    }
}
