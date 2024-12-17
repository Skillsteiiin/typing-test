using System.IO;
using System.Text.Json;
using static HelloApp1.Views.UserEducation;

namespace HelloApp1.Common
{
    public class UserResultsManager
    {
        private string ResultsFilePath = "";

        public List<UserResult> LoadResults()
        {
            ResultsFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\user_results.json"));
            if (!File.Exists(ResultsFilePath))
                return new List<UserResult>();

            var json = File.ReadAllText(ResultsFilePath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<UserResult>();

            return JsonSerializer.Deserialize<List<UserResult>>(json) ?? new List<UserResult>();
        }

        public void SaveResults(List<UserResult> results)
        {
            ResultsFilePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\user_results.json"));
            var json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ResultsFilePath, json);
        }
    }
}
