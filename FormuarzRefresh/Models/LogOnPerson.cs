using Microsoft.AspNetCore.Mvc;

namespace FormuarzRefresh.Models
{
    public class LogOnPerson
    {
        public string Login { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public bool IsHR { get; set; } = false;
    }
}
