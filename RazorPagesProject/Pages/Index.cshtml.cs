using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace RazorPagesProject.Pages
{
    public class IndexModel : PageModel
    {
        // Sınıfların saklanacağı liste (Geçici veritabanı)
        public static List<ClassInformationModel> ClassList { get; set; } = new List<ClassInformationModel>();

        // Filtreleme ve sayfalama özellikleri
        [BindProperty(SupportsGet = true)]
        public string FilterClassName { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public int TotalPages { get; set; }
        public List<ClassInformationTable> FilteredClassList { get; set; }
        

        // Sayfa yüklendiğinde filtreleme ve sayfalama yapılacak
        public void OnGet()
        {
            if (ClassList.Count == 0)
            {
                ClassList = GenerateTestData();
            }
            // Filtreleme ve sayfalama işlemi
            var query = ClassList.AsQueryable();

            // Filtreleme
            if (!string.IsNullOrEmpty(FilterClassName))
            {
                query = query.Where(c => c.ClassName.Contains(FilterClassName));
            }

            // Sayfalama
            int pageSize = 10;  // Sayfa başına gösterilecek öğe sayısı
            TotalPages = (int)System.Math.Ceiling(query.Count() / (double)pageSize);

            FilteredClassList = query
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new ClassInformationTable
                {
                    Id = c.Id,
                    ClassName = c.ClassName,
                    StudentCount = c.StudentCount,
                    Description = c.Description
                })
                .ToList();
        }

        // Formdan gelen veriyi listeye ekle
        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // ID otomatik artırılıyor
            var newClass = new ClassInformationModel
            {
                Id = ClassList.Count > 0 ? ClassList.Max(c => c.Id) + 1 : 1,
                ClassName = FilteredClassList.FirstOrDefault()?.ClassName,
                StudentCount = FilteredClassList.FirstOrDefault()?.StudentCount ?? 0,
                Description = FilteredClassList.FirstOrDefault()?.Description
            };
            ClassList.Add(newClass);
            return RedirectToPage();
        }

        // Sınıfı silme işlemi
        public IActionResult OnPostDelete(int id)
        {
            var classToRemove = ClassList.FirstOrDefault(c => c.Id == id);
            if (classToRemove != null)
            {
                ClassList.Remove(classToRemove);
            }

            return RedirectToPage();
        }
        public static List<ClassInformationModel> GenerateTestData()
{
    var random = new Random();
    var classList = new List<ClassInformationModel>();

    for (int i = 1; i <= 100; i++)
    {
        classList.Add(new ClassInformationModel
        {
            Id = i,
            ClassName = "Class " + i,
            StudentCount = random.Next(10, 50),
            Description = "Description for class " + i
        });
    }

    return classList;
    }
    }
}