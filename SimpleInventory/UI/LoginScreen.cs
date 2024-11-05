﻿using SimpleInventory.Services;

namespace SimpleInventory.UI
{
    public class LoginScreen
    {
        private readonly AuthService _authService;

        public LoginScreen(AuthService authService)
        {
            _authService = authService;
        }

        public void ShowLoginScreen()
        {
            while (true)
            {
                Console.WriteLine("\n--- Welcome to SimpleInventory ---");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Signup");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (Login())
                        {
                            return; // On successful login, break the loop and show the main menu
                        }
                        break;
                    case "2":
                        Signup();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private bool Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine() ?? "";
            Console.Write("Enter password: ");
            string password = Console.ReadLine() ?? "";

            if (_authService.Login(username, password))
            {
                Console.WriteLine("Login successful.");
                return true;
            }
            Console.WriteLine("Login failed.");
            return false;
        }

        private void Signup()
        {
            Console.Write("Enter new username: ");
            string username = Console.ReadLine() ?? "";
            Console.Write("Enter new password: ");
            string password = Console.ReadLine() ?? "";

            _authService.Signup(username, password);
        }
    }
}
