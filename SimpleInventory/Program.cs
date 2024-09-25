class Program
{
    static List<Item> inventory = new List<Item>();

    static void Main(string[] args)
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
                inventory.Add(item);
                Console.WriteLine($"Added {item.Name} to inventory.");
            }

            void ViewInventory()
            {
                Console.WriteLine("\nCurrent Inventory");
                foreach (var item in inventory)
                {
                    Console.WriteLine($"Name: {item.Name} Pric: {item.Price}");
                }
            }

        }
      }
    }


    class Item
    {
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }

