using MySql.Data.MySqlClient;
using SimpleInventory.Models;

namespace SimpleInventory.Data
{
    public class ItemRepository
    {
        public void AddItem(Item item) { /* Logic for adding an item */ }
        public void DeleteItem(string id) { /* Logic for deleting an item */ }
        public void UpdateItem(Item item) { /* Logic for updating an item */ }
        public List<Item> ViewInventory() { /* Logic for viewing all items */ }
        public Item? GetItemByIdOrName(string searchTerm) { /* Logic for finding an item */ }
        public bool CheckIfIdExists(string id) { /* Logic for checking item existence */ }
    }
}
