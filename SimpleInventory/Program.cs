class Program
{
    static List<Item> inventory = new List<Item>();

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to SimpleInventory!");

        AddItem(new Item { Name = "Laptop", Quantity = 10, Price = 999.99m });
        AddItem(new Item { Name = "Iphone", Quantity = 10, Price = 1999.99m });
        AddItem(new Item { Name = "Gun", Quantity = 10, Price = 3999.99m });

        ViewInventory();

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


class Item
{
    public string? Name { get; set; }
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
}
