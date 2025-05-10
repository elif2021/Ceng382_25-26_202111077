using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesProject.Data;
using RazorPagesProject.Models;
using System.Text;
using System.Text.Json;

namespace RazorPagesProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SchoolDbContext _context;

        public IndexModel(SchoolDbContext context)
        {
            _context = context;
        }

        public List<ClassInformationTable> DisplayedClasses { get; set; } = new List<ClassInformationTable>();
        public int TotalPages { get; set; }
        public const int PageSize = 10;

        [BindProperty(SupportsGet = true)]
        public string? FilterText { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        [BindProperty]
        public string SelectedColumns { get; set; }

        [BindProperty]
        public ClassInformationModel NewClass { get; set; } = new ClassInformationModel();

        public async Task OnGetAsync()
        {
            IQueryable<ClassInformationModel> query = _context.Classes;

            if (!string.IsNullOrWhiteSpace(FilterText))
            {
                query = query.Where(c => c.ClassName.Contains(FilterText));
            }

            int totalCount = await query.CountAsync();
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

            var classes = await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            DisplayedClasses = classes.Select(c => new ClassInformationTable
            {
                Id = c.Id,
                ClassName = c.ClassName,
                StudentCount = c.StudentCount,
                Description = c.Description
            }).ToList();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Classes.Add(NewClass);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var classToRemove = await _context.Classes.FindAsync(id);
            if (classToRemove != null)
            {
                _context.Classes.Remove(classToRemove);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task OnGetEditAsync(int id)
        {
            var classToEdit = await _context.Classes.FindAsync(id);
            if (classToEdit != null)
            {
                NewClass = classToEdit;
            }
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            var classToUpdate = await _context.Classes.FindAsync(NewClass.Id);
            if (classToUpdate != null)
            {
                classToUpdate.ClassName = NewClass.ClassName;
                classToUpdate.StudentCount = NewClass.StudentCount;
                classToUpdate.Description = NewClass.Description;

                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSmartExportJsonAsync()
        {
            var selectedColumnsList = string.IsNullOrEmpty(SelectedColumns) ? new List<string>() : SelectedColumns.Split(',').ToList();

            IQueryable<ClassInformationModel> query = _context.Classes;

            if (!string.IsNullOrWhiteSpace(FilterText))
            {
                query = query.Where(c => c.ClassName.Contains(FilterText));
            }

            var pageClasses = await query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            string json;
            if (selectedColumnsList.Any())
            {
                json = Utils.Instance.ExportToJson(pageClasses, selectedColumnsList);
            }
            else
            {
                json = JsonSerializer.Serialize(pageClasses);
            }

            return File(Encoding.UTF8.GetBytes(json), "application/json", "export.json");
        }
    }
}
