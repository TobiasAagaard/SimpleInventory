using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace SimpleInventoryManager.GUI.Views
{
    public partial class LoginWindow : Window
    {
        private string connectionString;

        public LoginWindow()
        {
            InitializeComponent();
            LoadConnectionString();
        }

        private void LoadConnectionString()
        {
            var jsonText = File.ReadAllText("appsettings.json");  // Sørg for, at stien er korrekt
            var jsonObj = JObject.Parse(jsonText);

            var server = jsonObj["ConnectionStrings"]["MySqlConnection"]["Server"];
            var database = jsonObj["ConnectionStrings"]["MySqlConnection"]["Database"];
            var userId = jsonObj["ConnectionStrings"]["MySqlConnection"]["UserId"];
            var password = jsonObj["ConnectionStrings"]["MySqlConnection"]["Password"];

            connectionString = $"Server={server};Database={database};User ID={userId};Password={password};";
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (ValidateUser(username, password))
            {
                MessageBox.Show("Login successful!");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private bool ValidateUser(string username, string password)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT PasswordHash, Salt FROM Users WHERE Username = @Username";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader.GetString("PasswordHash");
                            string salt = reader.GetString("Salt");

                            string hashToVerify = HashPassword(password, salt);
                            return hashToVerify == storedHash;
                        }
                    }
                }
            }
            return false;
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltedPassword = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashBytes = sha256.ComputeHash(saltedPassword);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
