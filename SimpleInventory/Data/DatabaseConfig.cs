using Newtonsoft.Json.Linq;

namespace SimpleInventory.Data
{
    public static class DatabaseConfig
    {
        public static string? ConnectionString { get; private set; }

        static DatabaseConfig()
        {
            LoadConnectionString();
        }

        public static void LoadConnectionString()
        {
            if (!File.Exists("appsettings.json"))
            {
                Console.WriteLine("appsettings.json file not found.");
                return;
            }

            try
            {
                var jsonText = File.ReadAllText("appsettings.json");
                var jsonObj = JObject.Parse(jsonText);

                var server = jsonObj["ConnectionStrings"]?["MySqlConnection"]?["Server"]?.ToString();
                var database = jsonObj["ConnectionStrings"]?["MySqlConnection"]?["Database"]?.ToString();
                var userId = jsonObj["ConnectionStrings"]?["MySqlConnection"]?["UserId"]?.ToString();
                var password = jsonObj["ConnectionStrings"]?["MySqlConnection"]?["Password"]?.ToString();

                if (server == null || database == null || userId == null || password == null)
                {
                    Console.WriteLine("One or more required connection string fields are missing in appsettings.json.");
                    return;
                }

                ConnectionString = $"Server={server};Database={database};User ID={userId};Password={password};";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading connection string: {ex.Message}");
            }
        }
    }
}
