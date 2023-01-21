namespace Server.DTO
{
    public class RegistrationDto
    {
        public string email { get; set; } 
        public string password { get; set; }
        public RegistrationDto(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
