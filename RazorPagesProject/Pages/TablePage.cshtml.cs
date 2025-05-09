using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesProject.Pages
{
    public class TablePageModel : PageModel
    {}
       public string? Username { get; set; }

    public void OnGet()
    {
    // Null kontrolü yaparak, hata almayı engelle
        Username = HttpContext.Session.GetString("Username") ?? "DefaultUsername";  // "DefaultUsername" gibi bir varsayılan değer kullanabilirsiniz.
    }


        public IActionResult OnPost()
        {
            // Çıkış yapıldığında session'ı temizle
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Username");
            Response.Cookies.Delete("Token");

            // Login sayfasına yönlendir
            return RedirectToPage("/Login");
        }
    }
}
