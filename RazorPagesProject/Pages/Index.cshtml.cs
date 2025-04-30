using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesProject.Helpers; // Utils sınıfı için doğru ad alanı
using RazorPagesProject.Models; // ClassInformationModel ve ClassInformationTable için doğru ad alanı
using System.Collections.Generic;
using System.Linq;

namespace RazorPagesProject.Pages
{
    public class IndexModel : PageModel
    {
        // --- Bindable Properties ---
        [BindProperty(SupportsGet = true)]
        public string FilterClassName { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public bool ExportFiltered { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<string> SelectedColumns { get; set; } = new();

        // --- Data Lists ---
        public static List<ClassInformationModel> ClassList { get; set; } = new();
        public List<ClassInformationTable> FilteredClassList { get; set; }

        public int TotalPages { get; set; }

        // --- OnGet ---
        public void OnGet()
        {
            if (ClassList.Count == 0)
                ClassList = GenerateTestData();

            var query = ClassList.AsQueryable();

            if (!string.IsNullOrEmpty(FilterClassName))
                query = query.Where(c => c.ClassName.Contains(FilterClassName));

            int pageSize = 10;
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

        // --- OnPostAdd ---
        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
                return Page();

            var newClass = new ClassInformationModel
            {
                Id = ClassList.Count > 0 ? ClassList.Max(c => c.Id) + 1 : 1,
                ClassName = FilterClassName,
                StudentCount = 0,
                Description = "No description"
            };

            ClassList.Add(newClass);
            return RedirectToPage();
        }

        // --- OnPostDelete ---
        public IActionResult OnPostDelete(int id)
        {
            var classToRemove = ClassList.FirstOrDefault(c => c.Id == id);
            if (classToRemove != null)
                ClassList.Remove(classToRemove);

            return RedirectToPage();
        }

        // --- OnPostExportToJson ---
        public IActionResult OnPostExportToJson()
        {
            // Her iki listeyi aynı türde bir listeye dönüştür
            var exportList = ExportFiltered
                ? FilteredClassList.Select(c => new ClassInformationModel
                {
                    Id = c.Id,
                    ClassName = c.ClassName,
                    StudentCount = c.StudentCount,
                    Description = c.Description
                }).ToList()
                : ClassList;

            var selectedData = exportList.Select(item =>
            {
                var obj = new Dictionary<string, object>();
                var type = item.GetType();

                if (SelectedColumns == null || SelectedColumns.Count == 0)
                {
                    foreach (var prop in type.GetProperties())
                        obj[prop.Name] = prop.GetValue(item);
                }
                else
                {
                    foreach (var col in SelectedColumns)
                    {
                        var prop = type.GetProperty(col);
                        if (prop != null)
                            obj[prop.Name] = prop.GetValue(item);
                    }
                }

                return obj;
            }).ToList();

            string jsonString = Utils.Instance.ExportToJson(selectedData);
            var bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);
            return File(bytes, "application/json", "export.json");
        }

        // --- Generate Sample Data ---
        public static List<ClassInformationModel> GenerateTestData()
        {
            var random = new System.Random();
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