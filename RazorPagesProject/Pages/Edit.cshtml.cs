using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesProject; // 'Models' yerine doğru ad alanını kullanın
using System.ComponentModel.DataAnnotations;

namespace RazorPagesProject.Pages
{
    public class EditModel : PageModel
    {
        // Edit sayfasına gelen sınıf bilgileri
        [BindProperty]
        [Required(ErrorMessage = "ClassName is required")]
        public string ClassName { get; set; }

        [BindProperty]
        public int StudentCount { get; set; }

        [BindProperty]
        public string Description { get; set; }

        // Sınıf ID'si
        public int Id { get; set; }

        // Bu işlem sayfa yüklendiğinde yapılacak
        public IActionResult OnGet(int id)
        {
            var classToEdit = FakeDataStore.Data.FirstOrDefault(c => c.Id == id);
            if (classToEdit == null)
            {
                return NotFound(); // Eğer sınıf bulunamazsa, hata döndür.
            }

            // Sınıf bilgilerini formu doldurmak için yüklüyoruz
            Id = classToEdit.Id;
            ClassName = classToEdit.ClassName;
            StudentCount = classToEdit.StudentCount;
            Description = classToEdit.Description;

            return Page();
        }

        // Düzenleme işlemi post işlemi
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)  // Model doğrulaması yapılır
            {
                return Page();  // Hatalıysa sayfa yeniden yüklenir
            }

            var classToEdit = FakeDataStore.Data.FirstOrDefault(c => c.Id == Id);
            if (classToEdit == null)
            {
                return NotFound(); // Eğer sınıf bulunamazsa, hata döndür.
            }

            // Sınıf bilgisini güncelle
            classToEdit.ClassName = ClassName;
            classToEdit.StudentCount = StudentCount;
            classToEdit.Description = Description;

            return RedirectToPage("/Index"); // Geri ana sayfaya yönlendir
        }
    }
}