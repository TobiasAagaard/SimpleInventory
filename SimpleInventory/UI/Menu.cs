using SimpleInventory.Services;
using SimpleInventory.Utilities;

namespace SimpleInventory.UI
{
    public class Menu
    {
        private readonly InventoryService _inventoryService = new InventoryService();

        public void ShowMainMenu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add an item");
                Console.WriteLine("2. Delete an item");
                Console.WriteLine("3. Update an item");
                Console.WriteLine("4. View inventory");
                Console.WriteLine("5. Search for an item");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddItem();
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
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void AddItem()
        {
            Console.Write("Enter item name: ");
            string name = Console.ReadLine() ?? "";
            int quantity = ConsoleHelpers.ReadInt("Enter item quantity: ");
            decimal price = ConsoleHelpers.ReadDecimal("Enter item price: ");
            _inventoryService.AddNewItem(name, quantity, price);
        }

        private void DeleteItem()
        {
            Console.Write("Enter the ID of the item to delete: ");
            string id = Console.ReadLine() ?? "";
            _inventoryService.DeleteItem(id);
        }

        private void UpdateItem()
        {
            Console.Write("Enter the ID of the item to update: ");
            string id = Console.ReadLine() ?? "";
            Console.Write("Enter new name (or leave blank): ");
            string? newName = Console.ReadLine();
            int? newQuantity = ConsoleHelpers.ReadOptionalInt("Enter new quantity (or leave blank): ");
            decimal? newPrice = ConsoleHelpers.ReadOptionalDecimal("Enter new price (or leave blank): ");
            _inventoryService.UpdateItem(id, newName, newQuantity, newPrice);
        }

        private void ViewInventory()
        {
            var items = _inventoryService.GetInventory();
            if (items.Count == 0)
            {
                Console.WriteLine("Inventory is empty.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Price: {item.Price:C}");
                }
            }
        }

        private void SearchItem()
        {
            Console.Write("Enter item name or ID to search: ");
            string searchTerm = Console.ReadLine() ?? "";
            var item = _inventoryService.SearchItem(searchTerm);
            if (item != null)
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Price: {item.Price:C}");
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }
    }
}
