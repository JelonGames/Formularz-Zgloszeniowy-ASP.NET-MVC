using System.ComponentModel;

namespace FormuarzRefresh.Models
{
    public class ListFormsSearch
    {
        public DateTime SendDateFrom { get; set; }
        public DateTime SendDateTo { get; set; }
        public DateTime CompleteDateFrom { get; set; }
        public DateTime CompleteDateTo { get; set; }
        [DisplayName("Departament")]
        public string Dept { get; set; } = string.Empty;
        [DisplayName("Użytkownik")]
        public string User { get; set; } = string.Empty;
        [DisplayName("Status")]
        public string Status { get; set; } = string.Empty;
        [DisplayName("Tytuł")]
        public string Title { get; set; } = string.Empty;
    }
}
