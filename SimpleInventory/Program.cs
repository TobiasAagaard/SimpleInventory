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
        var menu = new Menu();

        while (true)
        {
            loginScreen.ShowLoginScreen();

            bool isLoggedOut = menu.ShowMainMenu();

            if (isLoggedOut)
            {
                Console.WriteLine("Returning to login screen...");
            }
        }
    }
}
