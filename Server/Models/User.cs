using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class User
    {
        public Guid id { get; set; } 
        public string email { get; set; }
        public string password { get; set; }
        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
