using SimpleInventory.Services;
using SimpleInventory.Utilities;

namespace SimpleInventory.UI
{
    public class Menu
    {
        private readonly InventoryService _inventoryService = new InventoryService();
        private readonly string _userRole;

        public Menu(string userRole)
        {
            _userRole = userRole;
        }

        public bool ShowMainMenu()
        {
            bool isLoggedOut = false;
            while (!isLoggedOut)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Add an item");
                Console.WriteLine("2. Delete an item");
                Console.WriteLine("3. Update an item");
                Console.WriteLine("4. View inventory");
                Console.WriteLine("5. Search for an item");
                Console.WriteLine("6. Logout");
                Console.WriteLine("7. Exit");

                
                if (_userRole == "Admin")
                {
                    Console.WriteLine("\n--- Admin Panel ---");
                    Console.WriteLine("8. View all users");
                    Console.WriteLine("9. Delete a user account");
                }

                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine() ?? "";
                if (!HandleChoice(choice))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }

                isLoggedOut = HandleChoice(choice);
            }

            return isLoggedOut;
        }

        private bool HandleChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    AddItem();
                    return false;
                case "2":
                    DeleteItem();
                    return false;
                case "3":
                    UpdateItem();
                    return false;
                case "4":
                    ViewInventory();
                    return false;
                case "5":
                    SearchItem();
                    return false;
                case "6":
                    Console.WriteLine("Logging out...");
                    return true;
                case "7":
                    Environment.Exit(0);
                    return true;
                case "8":
                    if (_userRole == "Admin")
                    {
                        ViewAllUsers();
                        return false;
                    }
                    Console.WriteLine("Access denied. Admin only feature.");
                    return false;
                case "9":
                    if (_userRole == "Admin")
                    {
                        DeleteUserAccount();
                        return false;
                    }
                    Console.WriteLine("Access denied. Admin only feature.");
                    return false;
                default:
                    return false;
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

        private void ViewAllUsers()
        {
            Console.WriteLine("Viewing all users:");
           
        }

        private void DeleteUserAccount()
        {
            Console.Write("Enter the username of the account to delete: ");
            string username = Console.ReadLine() ?? "";
          
            Console.WriteLine($"User '{username}' has been deleted.");
        }
    }
}
