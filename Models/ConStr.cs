namespace Antique_Store_API.Models;
class ConStr {
    public static string GetConStr() {
        return $"Data Source={Directory.GetCurrentDirectory()}/antiquestoreapi.db";
    }
}