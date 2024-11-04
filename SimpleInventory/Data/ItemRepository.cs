using MySql.Data.MySqlClient;
using SimpleInventory.Models;

namespace SimpleInventory.Data
{
    public class ItemRepository
    {
        public void AddItem(Item item)
        {
            using (var connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                string query = "INSERT INTO Items (Id, Name, Quantity, Price) VALUES (@Id, @Name, @Quantity, @Price)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Quantity", item.Quantity);
                    command.Parameters.AddWithValue("@Price", item.Price);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteItem(string id)
        {
            using (var connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                string query = "DELETE FROM Items WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateItem(Item item)
        {
            using (var connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                string query = "UPDATE Items SET Name = @Name, Quantity = @Quantity, Price = @Price WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", item.Id);
                    command.Parameters.AddWithValue("@Name", item.Name);
                    command.Parameters.AddWithValue("@Quantity", item.Quantity);
                    command.Parameters.AddWithValue("@Price", item.Price);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Item> GetInventory()
        {
            var items = new List<Item>();
            using (var connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Quantity, Price FROM Items";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new Item
                        {
                            Id = reader.GetString("Id"),
                            Name = reader.GetString("Name"),
                            Quantity = reader.GetInt32("Quantity"),
                            Price = reader.GetDecimal("Price")
                        });
                    }
                }
            }
            return items;
        }

        public Item? GetItemByIdOrName(string searchTerm)
        {
            using (var connection = new MySqlConnection(DatabaseConfig.ConnectionString))
            {
                connection.Open();
                string query = "SELECT Id, Name, Quantity, Price FROM Items WHERE Name LIKE @SearchTerm OR Id = @SearchTerm";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Item
                            {
                                Id = reader.GetString("Id"),
                                Name = reader.GetString("Name"),
                                Quantity = reader.GetInt32("Quantity"),
                                Price = reader.GetDecimal("Price")
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
