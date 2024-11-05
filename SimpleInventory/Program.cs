using SimpleInventory.Data;
using SimpleInventory.Services;
using SimpleInventory.UI;

class Program
{
    static void Main(string[] args)
    {
        var userRepository = new UserRepository();
        var authService = new AuthService(userRepository);
        var loginScreen = new LoginScreen(authService);

        loginScreen.ShowLoginScreen();

        var menu = new Menu(); 
        menu.ShowMainMenu();
    }
}
