using System.ComponentModel.DataAnnotations;

namespace RazorPagesProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        // Yapıcı metod ekleyerek zorunlu alanları başlatıyoruz
        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
            IsActive = true; // Varsayılan değer
        }
    }
}
