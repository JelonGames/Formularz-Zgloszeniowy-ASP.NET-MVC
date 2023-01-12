using System.ComponentModel;

namespace FormuarzRefresh.Models.FormModel.GlobalFormModels
{
    public class MailBox : DatabaseApp
    {
        [DisplayName("Dodawanie uprawnień do Mailbox")]
        public bool MailBoxCheckbox { get; set; }

        [DisplayName("MailBox1")]
        public bool MailBox1 { get; set; }
        public PerMB MailBox1Per { get; set; } = PerMB.Odczyt;

        [DisplayName("MailBox2")]
        public bool MailBox2 { get; set; }
        public PerMB MailBox2Per { get; set; } = PerMB.Odczyt;

        [DisplayName("MailBox3")]
        public bool MailBox3 { get; set; }
        public PerMB MailBox3Per { get; set; } = PerMB.Odczyt;

        [DisplayName("MailBox4")]
        public bool MailBox4 { get; set; }
        public PerMB MailBox4Per { get; set; } = PerMB.Odczyt;

        [DisplayName("MailBox5")]
        public bool MailBox5 { get; set; }
        public PerMB MailBox5Per { get; set; } = PerMB.Odczyt;

        [DisplayName("MailBox6")]
        public bool MailBox6 { get; set; }
        public PerMB MailBox6Per { get; set; } = PerMB.Odczyt;

        [DisplayName("MailBox7")]
        public bool MailBox7 { get; set; }
        public PerMB MailBox7Per { get; set; } = PerMB.Odczyt;

        [DisplayName("MailBox8")]
        public bool MailBox8 { get; set; }
        public PerMB MailBox8Per { get; set; } = PerMB.Odczyt;

        [DisplayName("MailBox9")]
        public bool MailBox9 { get; set; }
        public PerMB MailBox9Per { get; set; } = PerMB.Odczyt;

        [DisplayName("MailBox10")]
        public bool MailBox10 { get; set; }
        public PerMB MailBox10Per { get; set; } = PerMB.Odczyt;

        [DisplayName("MailBox11")]
        public bool MailBox11 { get; set; }
        public PerMB MailBox11Per { get; set; } = PerMB.Odczyt;

        [DisplayName("MailBox12")]
        public bool MailBox12 { get; set; }
        public PerMB MailBox12Per { get; set; } = PerMB.Odczyt;
    }
}
