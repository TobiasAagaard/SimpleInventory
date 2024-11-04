using Newtonsoft.Json.Linq;

namespace SimpleInventory.Data
{
    public static class DatabaseConfig
    {
        public static string? ConnectionString { get; private set; }

        public static void LoadConnectionString()
        {
            var jsonText = File.ReadAllText("appsettings.json");
            var jsonObj = JObject.Parse(jsonText);

            var server = jsonObj["ConnectionStrings"]["MySqlConnection"]["Server"];
            var database = jsonObj["ConnectionStrings"]["MySqlConnection"]["Database"];
            var userId = jsonObj["ConnectionStrings"]["MySqlConnection"]["UserId"];
            var password = jsonObj["ConnectionStrings"]["MySqlConnection"]["Password"];

            ConnectionString = $"Server={server};Database={database};User ID={userId};Password={password};";
        }
    }
}
