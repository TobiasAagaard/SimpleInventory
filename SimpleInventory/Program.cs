using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Linq;
using MySql.Data.MySqlClient;
using Mysqlx.Expr;
using Newtonsoft.Json.Linq;


class Program
{
    static string? connectionString;

    static void LoadConnectionString()
    {
        var jsonText = File.ReadAllText("appsettings.json");
        var jsonObj = JObject.Parse(jsonText);

        var server = jsonObj["ConnectionStrings"]["MySqlConnection"]["Server"];
        var database = jsonObj["ConnectionStrings"]["MySqlConnection"]["Database"];
        var userId = jsonObj["ConnectionStrings"]["MySqlConnection"]["UserId"];
        var password = jsonObj["ConnectionStrings"]["MySqlConnection"]["Password"];

        connectionString = $"Server={server};Database={database};User ID={userId};Password={password};";
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to SimpleInventory!");
        LoadConnectionString();



        bool running = true;

        while (running)
        {
            // Menu
            Console.WriteLine("\nChoose an option");
            Console.WriteLine("1. Add an item");
            Console.WriteLine("2. Delete an item");
            Console.WriteLine("3. Update Item");
            Console.WriteLine("4. View inventory");
            Console.WriteLine("5 Search for item");
            Console.WriteLine("6. Clear console");
            Console.WriteLine("7. Exit");
            Console.Write("Enter your choice: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddItem(new Item {});
                    break;
                case "2":
                    DeleteItem();
                    break;
                case "3":
                    UpdateItem();
                    break;
                case "4":
                    ViewInventory();
                    break;
                case "5":
                    SearchItem();
                    break;
                case "6":
                    Console.Clear();
                    break;
                case "7":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select another option");
                    break;
            }

            void AddItem(Item item)
            {
                Console.WriteLine("\nYou need to enter a name for the product ");
                string? name = "";

                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.Write("\nEnter item name(or type 'exit' to exit to menu): ");
                    name = Console.ReadLine();

                    if (name?.ToLower() == "exit")
                    {
                        Console.WriteLine("Exit to menu");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("You have to type a name for the product");
                    }
                }

                Console.Write("Enter item quantity ");
                int quantity;

                while (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a non-negative number.");
                }

                Console.Write("Enter item price: ");
                decimal price;
                while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a non-negative number.");
                }

                string id = GenerateUniqueRandomId(6);
                
                
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Items (Id, Name, Quantity, Price) VALUES (@Id, @Name, @Quantity, @Price)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@Price", price);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                Console.WriteLine($"Item '{name}' added successfully!");

            }

            void DeleteItem()
            {
                //Renders Items 
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Name FROM Items";
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("\nInventory is empty, you can't DELETE any items.");
                            return;
                        }

                        Console.WriteLine("\nItems in inventory to DELETE");
                        Console.WriteLine();
                        while (reader.Read())
                        {
                          
                            string id = reader.GetString("Id");
                            string name = reader.GetString("Name");
                            Console.WriteLine($"ID: {id}, Name: {name}");
                            
                        }
                    }
                    connection.Close();
                }
                Console.WriteLine();

                Console.Write("Enter ID here to DELETE: ");
                string? identifier = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(identifier))
                {
                    Console.WriteLine("You need to enter a valid ID");
                    return;
                }

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Items WHERE Id = @Identifier";

                    using ( var command = new MySqlCommand(query,connection))
                    {
                        command.Parameters.AddWithValue("@Identifier", identifier);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Item deleted successfully");
                        } else
                        {
                            Console.WriteLine("Item not found or failed to delete");
                        }

                    }
                }
            }

            void UpdateItem()
            {
                //Renders out the inventory
                Console.WriteLine("\nCurrent Inventory:");
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Name, Quantity, Price FROM Items";
                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("\nInventory is empty.");
                            return;
                        }

                        while (reader.Read())
                        {
                            string id = reader.GetString("Id");
                            string name = reader.GetString("Name");
                            int quantity = reader.GetInt32("Quantity");
                            decimal price = reader.GetDecimal("Price");

                            Console.WriteLine($"ID: {id}, Name: {name}, Quantity: {quantity}, Price: {price:C}");
                        }
                    }
                }
                //Type id to select item to update
                Console.Write("\nEnter the ID of the item you want to update: ");
                string? identifier = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(identifier))
                {
                    Console.WriteLine("You need to enter a valid ID");
                    return;
                }
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Name, Quantity, Price FROM Items WHERE Id = @Id";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", identifier);
                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("Item not found.");
                                return;
                            }

                            reader.Read();
                            string name = reader.GetString("Name");
                            int quantity = reader.GetInt32("Quantity");
                            decimal price = reader.GetDecimal("Price");

                            Console.WriteLine($"\nCurrent item data - ID: {identifier}, Name: {name}, Quantity: {quantity}, Price: {price:C}");
                        }
                    }
                }
                //Input update 
                Console.WriteLine("\nEnter new values for the item (leave blank to keep the current value):");

                Console.Write("New name: ");
                string? newName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newName)) newName = null;

                Console.Write("New quantity: ");
                string? newQuantityInput = Console.ReadLine();
                int? newQuantity = null;
                if (!string.IsNullOrWhiteSpace(newQuantityInput) && int.TryParse(newQuantityInput, out int parsedQuantity))
                {
                    newQuantity = parsedQuantity;
                }

                Console.Write("New price: ");
                string? newPriceInput = Console.ReadLine();
                decimal? newPrice = null;
                if (!string.IsNullOrWhiteSpace(newPriceInput) && decimal.TryParse(newPriceInput, out decimal parsedPrice))
                {
                    newPrice = parsedPrice;
                }


                //Update in databas
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = "UPDATE Items SET Name = COALESCE(@NewName, Name), Quantity = COALESCE(@NewQuantity, Quantity), Price = COALESCE(@NewPrice, Price) WHERE Id = @Id";

                    using (var command = new MySqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", identifier);
                        command.Parameters.AddWithValue("@NewName", (object?)newName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@NewQuantity", (object?)newQuantity ?? DBNull.Value);
                        command.Parameters.AddWithValue("@NewPrice", (object?)newPrice ?? DBNull.Value);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Item updated successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to update the item.");
                        }
                    }
                }

            }

            void ViewInventory()
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Name, Quantity, Price FROM Items";

                    using (var command = new MySqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("\nInventory is empty.");
                        }
                        else
                        {
                            Console.WriteLine("\nCurrent Inventory:");
                            while (reader.Read())
                            {
                                string id = reader.GetString("Id");
                                string name = reader.GetString("Name");
                                int quantity = reader.GetInt32("Quantity");
                                decimal price = reader.GetDecimal("Price");

                                Console.WriteLine($"ID: {id}, Name: {name}, Quantity: {quantity}, Price: {price:C}");
                            }
                        }
                    }
                    connection.Close();
                }
            }

            //Search Function Matching ID or Name with Input
            void SearchItem()
            {
                Console.Write("\nEnter the name or ID of the item to search: ");
                string? searchTerm = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    Console.WriteLine("Please enter a valid search term.");
                    return;
                }

                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Id, Name, Quantity, Price FROM Items WHERE Name LIKE @SearchTerm OR Id = @SearchTerm";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SearchTerm", $"%{searchTerm}%");

                        using (var reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                Console.WriteLine("\nNo items found matching the search criteria.");
                            }
                            else
                            {
                                Console.WriteLine("\nSearch Results:");
                                while (reader.Read())
                                {
                                    string id = reader.GetString("Id");
                                    string name = reader.GetString("Name");
                                    int quantity = reader.GetInt32("Quantity");
                                    decimal price = reader.GetDecimal("Price");

                                    Console.WriteLine($"ID: {id}, Name: {name}, Quantity: {quantity}, Price: {price:C}");
                                }
                            }
                        }
                    }
                    connection.Close();
                }
            }


            static string GenerateUniqueRandomId(int length)
            {
                string newId;
                do
                {
                    newId = GenerateRandomId(length);
                } while (CheckIfIdExists(newId));

                return newId;
            }


            static string GenerateRandomId(int length)
            {
                const string chars = "0123456789";
                char[] id = new char[length];
                byte[] randomBytes = new byte[length];
                RandomNumberGenerator.Fill(randomBytes);

                for (int i = 0; i < id.Length; i++)
                {
                    id[i] = chars[randomBytes[i] % chars.Length];
                }

                return new string(id);
            }

            static bool CheckIfIdExists(string id)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Items WHERE Id = @Id";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
        }
    }
    }



