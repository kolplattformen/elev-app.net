namespace Skolplattformen.ElevApp.Models
{
    internal class LoginDetails
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public int Platform { get; set; }
    }
}
