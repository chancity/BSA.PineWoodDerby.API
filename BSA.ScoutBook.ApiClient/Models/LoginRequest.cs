using System.Collections.Generic;

namespace BSA.ScoutBook.ApiClient.Models
{
    public class LoginRequest
    {
        internal string CSRF { get; set; }
        public string Email { get; }
        public string Password { get; }

        public LoginRequest(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public Dictionary<string, object> ToDictionary() => new Dictionary<string, object>()
        {
            {"CSRF",CSRF },
            {"Email",Email },
            {"Password",Password },
            {"RememberEmail","off" },
            {"Action","Login" },
        };
        
     
    }
}
