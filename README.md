# SimpleInventory

**SimpleInventory** is a lightweight and user-friendly inventory management system built with C#.

## Features/Future Features

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

4. **Add `appsettings.json` to `.gitignore`**: Make sure that `appsettings.json` is not pushed to version control by adding it to your `.gitignore` file:

    ```
    appsettings.json
    ```

5. **Build the project**:

    ```bash
    dotnet build
    ```

6. **Run the application**:

    ```bash
    dotnet run
    ```

    ## Setting Up the MySQL Database and Configuration

    **Create the MySQL database and table**: Before running the application, you need to set up the MySQL database and table. Open MySQL Workbench (or any MySQL client), connect to your MySQL server, and run the following SQL code to create the `Items` table:

    ```sql
    CREATE TABLE Items (
        Id VARCHAR(6) PRIMARY KEY,
        Name VARCHAR(255) NOT NULL,
        Quantity INT NOT NULL,
        Price DECIMAL(10, 2) NOT NULL
    );
    ```

    **Create an `appsettings.json` file**: This file will store your database connection settings. In the root of the project directory, create a new file called `appsettings.json` and add the following content to it:

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

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for more details.
