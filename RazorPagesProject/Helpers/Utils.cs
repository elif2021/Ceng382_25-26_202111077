using System.Text.Json;

namespace RazorPagesProject.Helpers // Kendi namespace’in buysa
{
    public class Utils
    {
        private static Utils _instance;
        private static readonly object _lock = new object();

        private Utils() { }

        public static Utils Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new Utils();
                }
            }
        }

        public string ExportToJson<T>(List<T> data)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            return JsonSerializer.Serialize(data, options);
        }
    }
}