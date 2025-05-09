using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesProject.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Kullanıcı çıkış yaptıysa, oturumu temizleyin
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Username");
            Response.Cookies.Delete("Token");

            // Çıkış işleminden sonra, login sayfasına yönlendir
            return RedirectToPage("/Login");
        }
    }
}
