<<<<<<< HEAD
public class ClassInformationTable
{
    public int Id { get; set; }
    public string ClassName { get; set; }
    public int StudentCount { get; set; }
    public string Description { get; set; }
=======
namespace RazorPagesProject.Models
{
    public class ClassInformationTable
    {
        public int Id { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
>>>>>>> 3f60a6d692ccd693c59dc54a5a5c31603dfca2cb
}