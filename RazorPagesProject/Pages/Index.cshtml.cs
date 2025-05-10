using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesProject.Data;
using RazorPagesProject.Models;

namespace RazorPagesProject.Pages.Classes
{
    public class IndexModel : PageModel
    {
        private readonly SchoolDbContext _context;

        public IndexModel(SchoolDbContext context)
        {
            _context = context;
        }

        public IList<Class> ClassList { get; set; }

        [BindProperty]
        public Class NewClass { get; set; }

        public async Task OnGetAsync()
        {
            ClassList = await _context.Classes.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Classes.Add(NewClass);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var classToDelete = await _context.Classes.FindAsync(id);
            if (classToDelete != null)
            {
                _context.Classes.Remove(classToDelete);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            var classToUpdate = await _context.Classes.FindAsync(NewClass.Id);
            if (classToUpdate != null)
            {
                classToUpdate.Name = NewClass.Name;
                classToUpdate.PersonCount = NewClass.PersonCount;
                classToUpdate.Description = NewClass.Description;
                classToUpdate.IsActive = NewClass.IsActive;

                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
