namespace Internet_1.Models
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string PhotoUrl { get; set; }

    }
}
