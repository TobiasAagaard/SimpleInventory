# SimpleInventory

**SimpleInventory** is a lightweight and user-friendly inventory management system built with C#.

## Features

- **Add New Products**: Quickly add new items to your inventory with essential details such as name, quantity, price, and category.
- **Update Inventory**: Edit existing product details or update stock levels as items are sold or received.
- **Delete Items**: Remove outdated or discontinued products from your inventory.
- **View Inventory**: Display a list of all items in the inventory, including current stock levels and prices.
- **Search Functionality**: Easily find items by name, category, or other criteria.

## Installation

1. **Clone the repository**: Open your terminal and clone the repository using Git:

    ```bash
    git clone https://github.com/yourusername/SimpleInventory.git
    ```

2. **Navigate to the project directory**:

    ```bash
    cd SimpleInventory
    ```

3. **Create an `appsettings.json` file**: This file will store your database connection settings. In the root of the project directory, create a new file called `appsettings.json` and add the following content to it:

    ```json
    {
      "ConnectionStrings": {
        "MySqlConnection": {
          "Server": "localhost",
          "Database": "SimpleInventoryDB",
          "UserId": "your_mysql_username",
          "Password": "your_mysql_password"
        }
      }
    }
    ```

    Replace the values with your actual MySQL server details!

4. **Add `appsettings.json` to `.gitignore`**: Make sure that `appsettings.json` is not pushed to version control by adding it to your `.gitignore` file:

    ```
    appsettings.json
    ```

5. **Restore dependencies** (if using .NET Core):

    ```bash
    dotnet restore
    ```

6. **Build the project**:

    ```bash
    dotnet build
    ```

7. **Run the application**:

    ```bash
    dotnet run
    ```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for more details.
