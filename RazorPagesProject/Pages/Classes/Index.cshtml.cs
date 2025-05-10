using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YourProjectNamespace.Data;
using YourProjectNamespace.Models;

namespace YourProjectNamespace.Pages.Classes
{
    public class IndexModel : PageModel
    {
        private readonly SchoolDbContext _context;

        public IndexModel(SchoolDbContext context)
        {
            _context = context;
        }

        public IList<Class> ClassList { get; set; }

        public async Task OnGetAsync()
        {
            ClassList = await _context.Classes.ToListAsync();
        }
    }
}
