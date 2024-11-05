# SimpleInventory Console Application

This is the Console-based application for managing inventory in the SimpleInventory system. It allows users to manage inventory through a command-line interface.

## Features

- **Add an item**: Enter details like name, quantity, and price, and the item will be added to the inventory with a unique ID.
- **Delete an item**: View a list of current items and delete one by providing its unique ID.
- **View inventory**: Display all items in the inventory, including their ID, name, quantity, and price.
- **Search for an item**: Search for items by entering a name or ID. The application retrieves matching items from the inventory, displaying their details.


The Console application interacts with a MySQL database to store and manage inventory data. It uses a connection string specified in the `appsettings.json`  ( *Read the [main README](../README.md) for more information.* ) file to establish a connection. The database stores information such as item ID, name, quantity, and price, allowing for persistent inventory management.

- The application performs SQL queries to:
  - Add new items to the `Items` table.
  - Delete items based on their unique ID.
  - Retrieve and display all items in the inventory.
 

## SimpleInventory Project Structure
```text
SimpleInventory/
├── Program.cs                          // Main entry point for the application
├── appsettings.json                    // Configuration file with database connection details

├── Models/
│   ├── Item.cs                         // Model class representing inventory items
│   └── User.cs                         // Model class representing a user (for login/signup)

├── Data/
│   ├── DatabaseConfig.cs               // Loads and manages the MySQL connection string
│   ├── ItemRepository.cs               // Handles CRUD operations for inventory items
│   └── UserRepository.cs               // Handles user-related database operations for login/signup

├── Services/
│   ├── InventoryService.cs             // Business logic for managing inventory items
│   ├── IdGenerator.cs                  // Generates unique IDs for items
│   └── AuthService.cs                  // Handles authentication logic for login and signup

├── UI/
│   ├── LoginScreen.cs                  // Manages the login/signup user interface in the console
│   └── Menu.cs                         // Handles the main inventory menu and user options

└── Utilities/
    └── ConsoleHelpers.cs               // Helper methods for reading and validating console input

```

## License

This project is licensed under the MIT  [LICENSE](../LICENSE.txt).
