using System.Text.Json;

namespace AutoServiceApp
{
    // Загальна структура з публічним доступом та методами JSON
    public static class FileManager 
    {
        public static void SaveOrderJson(Order order, string filePath) 
        {
            try
            {
                var json = JsonSerializer.Serialize(order);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

        public static Order LoadOrderJson(string filePath)
        {
            try
            {
                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<Order>(json);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return new Order();
            }
        }
    }
}
