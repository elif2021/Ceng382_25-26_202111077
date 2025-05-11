using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesProject.Data;  // Bu namespace'i kullanarak DbContext'e erişiyorsunuz
using RazorPagesProject.Models; // Class modelinin namespace'i
using Microsoft.EntityFrameworkCore;  // Entity Framework Core için

namespace RazorPagesProject.Pages.Classes
{
    public class DeleteModel : PageModel
    {
        private readonly SchoolDbContext _context;

        public DeleteModel(SchoolDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Class Class { get; set; }

        // GET: /Classes/Delete/{id}
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // ID'ye göre veriyi buluyoruz
            Class = await _context.Classes.FindAsync(id);

            // Eğer sınıf bulunamazsa, 404 döner
            if (Class == null)
            {
                return NotFound();
            }

            return Page();
        }

        // POST: /Classes/Delete
        public async Task<IActionResult> OnPostAsync()
        {
            // Silinecek sınıfı buluyoruz
            var classToDelete = await _context.Classes.FindAsync(Class.Id);

            if (classToDelete != null)
            {
                // Silme işlemi
                _context.Classes.Remove(classToDelete);
                await _context.SaveChangesAsync();
            }

            // Silme işleminden sonra Index sayfasına yönlendirme
            return RedirectToPage("./Index");
        }
    }
}
