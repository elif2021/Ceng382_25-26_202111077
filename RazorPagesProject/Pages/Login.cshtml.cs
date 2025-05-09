using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesProject.Models;

namespace RazorPagesProject.Pages
{
    public class LoginModel : PageModel
    {
        public string ErrorMessage { get; set; } = string.Empty;

        public void OnGet()
        {
            // Sayfa yüklendiğinde hata mesajı varsa göster
        }

        public IActionResult OnPost(string Username, string Password)
        {
            // JSON dosyasındaki kullanıcıları oku
            var usersFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/data/users.json");
            var jsonString = System.IO.File.ReadAllText(usersFile);
            var users = JsonSerializer.Deserialize<List<User>>(jsonString);

            var user = users?.FirstOrDefault(u => u.Username == Username && u.Password == Password && u.IsActive);

            if (user != null)
            {
                // Kullanıcı bulundu ve giriş başarılı
                // Token üret
                var token = Guid.NewGuid().ToString();

                // Session'a veri ekle
                HttpContext.Session.SetString("Username", user.Username);
                HttpContext.Session.SetString("Token", token);
                HttpContext.Session.SetString("SessionId", HttpContext.Session.Id);

                // Cookie'lere veriyi ekle
                Response.Cookies.Append("Username", user.Username, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
                });
                Response.Cookies.Append("Token", token, new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict
                });

                // Yönlendirme
                return RedirectToPage("/TablePage"); // TablePage sayfasına yönlendir
            }

            // Giriş başarısız
            ErrorMessage = "Username or password is incorrect.";
            return Page();
        }
    }
}
