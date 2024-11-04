using SimpleInventory.Data;
using SimpleInventory.Models;
using System.Reflection.Emit;

namespace SimpleInventory.Services
{
    public class InventoryService
    {
        private readonly ItemRepository _repository = new ItemRepository();

        public void AddNewItem(string name, int quantity, decimal price)
        {
            string id = IdGenerator.GenerateUniqueRandomId(6);
            var item = new Item { Id = id, Name = name, Quantity = quantity, Price = price };
            _repository.AddItem(item);
        }

        public void DeleteItem(string id) => _repository.DeleteItem(id);

        public void UpdateItem(string id, string? newName, int? newQuantity, decimal? newPrice)
        {
            var item = _repository.GetItemByIdOrName(id);
            if (item != null)
            {
                item.Name = newName ?? item.Name;
                item.Quantity = newQuantity ?? item.Quantity;
                item.Price = newPrice ?? item.Price;
                _repository.UpdateItem(item);
            }
        }

        public List<Item> GetInventory() => _repository.GetInventory();

        public Item? SearchItem(string searchTerm) => _repository.GetItemByIdOrName(searchTerm);
    }
}
