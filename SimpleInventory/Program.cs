class InventoryManager
{
    static List<Item> inventory = new List<Item>();

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to SimpleInventory!");

        AddItem(new Item { Name = "Laptop", Quantity = 10, Price = 999.99m });


        void AddItem(Item item)
        {
            inventory.Add(item);
            Console.WriteLine($"Added {item.Name} to inventory.");
        }

    }
}


class Item
{
    public string? Name { get; set; }
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
}
