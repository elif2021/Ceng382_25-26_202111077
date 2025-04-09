using System.ComponentModel.DataAnnotations;

public class ClassInformationTable
{
    // ID tabloda gösterilmeyecek ancak arka planda kullanılacak.
    public int Id { get; set; }

    [Required(ErrorMessage = "Class Name is required.")]
    public string ClassName { get; set; } = string.Empty;

    public int StudentCount { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = string.Empty;
}