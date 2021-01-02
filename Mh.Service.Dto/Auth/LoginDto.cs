namespace Mh.Service.Dto.Auth
{
    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Enums.Languages LanguageId { get; set; }
    }
}