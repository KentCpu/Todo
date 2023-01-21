namespace Server.DTO
{
    public class AuthResponse
    {
        public Guid id  { get; set; }
        public string token { get; set; }
        public AuthResponse(Guid id, string token)
        {
            this.id = id;
            this.token = token;
        }
    }
}
