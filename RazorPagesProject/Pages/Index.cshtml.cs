using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
<<<<<<< HEAD
using RazorPagesProject.Helpers; // Utils sınıfı için doğru ad alanı
using RazorPagesProject.Models; // ClassInformationModel ve ClassInformationTable için doğru ad alanı
=======
using RazorPagesProject.Models;
>>>>>>> 3f60a6d692ccd693c59dc54a5a5c31603dfca2cb
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace RazorPagesProject.Pages
{
    public class IndexModel : PageModel
{
    // Filtreleme ve sayfalama için eklenen özellikler

    public List<ClassInformationTable> DisplayedClasses { get; set; } = new List<ClassInformationTable>();
    public int TotalPages { get; set; }
    public const int PageSize = 10;

    // Kullanıcıdan alınacak filtreleme kriteri
    [BindProperty(SupportsGet = true)]
    public string? FilterText { get; set; }

    // Sayfa numarası
    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    // Seçilen kolonları almak için property
    [BindProperty]
    public string SelectedColumns { get; set; }

    // Geçici veri listesi (burada sahte verilerle test edilecek)
    public static List<ClassInformationModel> ClassList { get; set; } = new List<ClassInformationModel>();

    public void OnGet()
    {
<<<<<<< HEAD
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
=======
        // Sahte veri ekleniyor
        if (!ClassList.Any()) // Eğer sınıf listesi boşsa, sahte veriler eklenir
        {
            for (int i = 1; i <= 100; i++)
            {
                ClassList.Add(new ClassInformationModel
>>>>>>> 3f60a6d692ccd693c59dc54a5a5c31603dfca2cb
                {
                    Id = i,
                    ClassName = $"Class {i}",
                    StudentCount = 20 + (i % 10),
                    Description = $"Description {i}"
                });
            }
        }

<<<<<<< HEAD
        // --- OnPostAdd ---
=======
        // Filtreleme işlemi
        var filteredClasses = string.IsNullOrWhiteSpace(FilterText)
            ? ClassList
            : ClassList.Where(c => c.ClassName.Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList();

        // Sayfalama işlemi
        TotalPages = (int)Math.Ceiling(filteredClasses.Count / (double)PageSize);
        DisplayedClasses = filteredClasses
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize)
            .Select(c => new ClassInformationTable
            {
                Id = c.Id,
                ClassName = c.ClassName,
                StudentCount = c.StudentCount,
                Description = c.Description
            }).ToList();
    }

     // Sınıf ekleme işlemi
        [BindProperty]
        public ClassInformationModel NewClass { get; set; } = new ClassInformationModel();

>>>>>>> 3f60a6d692ccd693c59dc54a5a5c31603dfca2cb
        public IActionResult OnPostAdd()
        {
            if (!ModelState.IsValid)
                return Page();

<<<<<<< HEAD
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
=======
            // ID otomatik artırılıyor
            NewClass.Id = ClassList.Count > 0 ? ClassList.Max(c => c.Id) + 1 : 1;
            ClassList.Add(NewClass);

            return RedirectToPage();
        }

        // Sınıf silme işlemi
>>>>>>> 3f60a6d692ccd693c59dc54a5a5c31603dfca2cb
        public IActionResult OnPostDelete(int id)
        {
            var classToRemove = ClassList.FirstOrDefault(c => c.Id == id);
            if (classToRemove != null)
                ClassList.Remove(classToRemove);

            return RedirectToPage(); 
        }

        // Düzenleme için formu doldurma
        public void OnGetEdit(int id)
        {
            var classToEdit = ClassList.FirstOrDefault(c => c.Id == id);
            if (classToEdit != null)
            {
                NewClass = classToEdit;
            }
        }

        // Güncellenmiş veriyi kaydetme
        public IActionResult OnPostEdit()
        {
            var classToUpdate = ClassList.FirstOrDefault(c => c.Id == NewClass.Id);
            if (classToUpdate != null)
            {
                classToUpdate.ClassName = NewClass.ClassName;
                classToUpdate.StudentCount = NewClass.StudentCount;
                classToUpdate.Description = NewClass.Description;
            }

            return RedirectToPage();
        }

<<<<<<< HEAD
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
=======
    public IActionResult OnPostSmartExportJson()
    {
        // Formdan gelen değerleri al
        var selectedColumnsList = string.IsNullOrEmpty(SelectedColumns) ? new List<string>() : SelectedColumns.Split(',').ToList();

        int pageNumber = string.IsNullOrEmpty(Request.Form["pageNumber"]) ? 1 : int.Parse(Request.Form["pageNumber"]);

        if (!string.IsNullOrWhiteSpace(FilterText) && selectedColumnsList.Any())
        {
            var filteredClasses = ClassList.Where(c => c.ClassName.Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList();

            var fpageClasses = filteredClasses.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();

            string json = Utils.Instance.ExportToJson(fpageClasses, selectedColumnsList);
            return File(Encoding.UTF8.GetBytes(json), "application/json", "filtered_columns.json");
        }

        // Sadece filtre varsa
        if (!string.IsNullOrWhiteSpace(FilterText))
        {
            var filteredClasses = ClassList.Where(c => c.ClassName.Contains(FilterText, StringComparison.OrdinalIgnoreCase)).ToList();

            string json = JsonSerializer.Serialize(filteredClasses);
            return File(Encoding.UTF8.GetBytes(json), "application/json", "filtered_rows.json");
        }

        // Sadece kolon seçimi varsa
        if (selectedColumnsList.Any())
        {
            var cpageClasses = ClassList.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();

            string json = Utils.Instance.ExportToJson(cpageClasses, selectedColumnsList);
            return File(Encoding.UTF8.GetBytes(json), "application/json", "columns_only.json");
        }

        // Hiçbiri yoksa
        var pageClasses = ClassList.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();
        string allJson = Utils.Instance.ExportToJson(pageClasses);
        return File(Encoding.UTF8.GetBytes(allJson), "application/json", "all_data.json");
>>>>>>> 3f60a6d692ccd693c59dc54a5a5c31603dfca2cb
    }
}
}