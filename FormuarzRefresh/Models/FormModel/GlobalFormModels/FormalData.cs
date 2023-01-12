using System.ComponentModel;

namespace FormuarzRefresh.Models.FormModel.GlobalFormModels
{
    public class FormalData
    {
        [DisplayName("Data Zgłoszenia:")]
        public DateTime SendDate { get; set; }
        [DisplayName("Firma:")]
        public string Business { get; set; } = "Firma XYZ";
        [DisplayName("Dział:")]
        public string Dept { get; set; } = string.Empty;
        [DisplayName("Osoba Zgłaszająca:")]
        public string Sender { get; set; } = string.Empty;
        [DisplayName("Wiadomość (Opcjonalna)")]
        public string Message { get; set; } = string.Empty;
    }
}
