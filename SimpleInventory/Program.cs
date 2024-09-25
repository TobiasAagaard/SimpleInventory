using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Linq;

class Program
{
    static List<Item> inventory = new List<Item>();

    static void Main()
    {
        Console.WriteLine("Welcome to SimpleInventory!");

        bool running = true;

        while (running)
        {
            Console.WriteLine("\nChoose an option");
            Console.WriteLine("1. Add an item");
            Console.WriteLine("2. View inventory");
            Console.WriteLine("3. Exit");
            Console.Write("Enter your choice: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddItem(new Item { Name = "Laptop", Quantity = 10, Price = 999.99m });
                    break;
                case "2":
                    ViewInventory();
                    break;
                case "3":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select 1, 2, or 3.");
                    break;
            }

            void AddItem(Item item)
            {
                Console.WriteLine("\nYou need to enter a name for the product ");
                string? name = "";

                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.Write("\nEnter item name: ");
                    name = Console.ReadLine();

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

                inventory.Add(new Item { Id = GenerateRandomId(6), Name = name, Quantity = quantity, Price = price });
                Console.WriteLine($"Item '{name}' added successfully!");

            }

            void ViewInventory()
            {
                if (inventory.Count == 0)
                {
                    Console.WriteLine("\nInventory is empty");
                }
                else
                {

                    Console.WriteLine("\nCurrent Inventory");
                    foreach (var item in inventory)
                    {
                        Console.WriteLine($"ID: {item.Id} Name: {item.Name} Price: {item.Price:C}");
                    }
                }
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

        }
      }
    }


    class Item
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }

