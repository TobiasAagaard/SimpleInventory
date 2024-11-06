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

        while (true)
        {
            
            string? userRole = loginScreen.ShowLoginScreen();

            if (userRole != null) 
            {
                var menu = new Menu(userRole); 
                bool isLoggedOut = menu.ShowMainMenu();

                if (isLoggedOut)
                {
                    Console.WriteLine("Returning to login screen...");
                }
            }
            else
            {
                Console.WriteLine("Login failed. Please try again.");
            }
        }
    }
}
