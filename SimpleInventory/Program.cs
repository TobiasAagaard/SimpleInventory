using SimpleInventory.UI;

class Program
{
    static void Main(string[] args)
    {
        var loginScreen = new LoginScreen();
        loginScreen.ShowLoginScreen();

        var menu = new Menu();
        menu.ShowMainMenu();
    }
}
