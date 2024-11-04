using SimpleInventory.UI;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to SimpleInventory!");
        var menu = new Menu();
        menu.ShowMainMenu();
    }
}
