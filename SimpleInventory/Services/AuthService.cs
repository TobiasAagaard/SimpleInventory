using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using SimpleInventory.Models;

namespace SimpleInventory.Data
{
    public class UserRepository
    {
        public void AddUser(string username, string password)
        {
            string salt = GenerateSalt();
            string passwordHash = HashPassword(password, salt);

            using (var connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Username, PasswordHash, Salt) VALUES (@Username, @PasswordHash, @Salt)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    command.Parameters.AddWithValue("@Salt", salt);
                    command.ExecuteNonQuery();
                }
            }
        }

        public User? GetUserByUsername(string username)
        {
            using (var connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Users WHERE Username = @Username";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = reader.GetInt32("UserId"),
                                Username = reader.GetString("Username"),
                                PasswordHash = reader.GetString("PasswordHash"),
                                Salt = reader.GetString("Salt"),
                                Role = reader.GetString("Role")
                            };
                        }
                    }
                }
            }
            return null;
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltedPassword = System.Text.Encoding.UTF8.GetBytes(password + salt);
                byte[] hashBytes = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyPassword(string enteredPassword, string storedHash, string salt)
        {
            string hashOfEnteredPassword = HashPassword(enteredPassword, salt);
            return hashOfEnteredPassword == storedHash;
        }
    }
}
