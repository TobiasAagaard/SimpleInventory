# SimpleInventory

**SimpleInventory** is a lightweight and user-friendly inventory management system built with C#.

This project originally started as a **console application**, but now there are plans to develop a **WPF (Windows Presentation Foundation) version** to provide a graphical user interface (GUI) for better user experience.

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

3. **Build the project**:

    ```bash
    dotnet build
    ```

4. **Run the application**:

    ```bash
    dotnet run
    ```

    ## Setting Up the MySQL Database and Configuration

    **Create the MySQL database and table**: Before running the application, you need to set up the MySQL database and table. Open MySQL Workbench (or any MySQL client), connect to your MySQL server, and run the following SQL code to create the `Database` and `Items` table:

    ```sql
    
    CREATE DATABASE SimpleInventoryDB;
    
    USE SimpleInventoryDB;
    
    CREATE TABLE Items (
        Id VARCHAR(6) PRIMARY KEY,
        Name VARCHAR(255) NOT NULL,
        Quantity INT NOT NULL,
        Price DECIMAL(10, 2) NOT NULL
    );
    
    CREATE TABLE Users (
        UserId INT AUTO_INCREMENT PRIMARY KEY,
        Username VARCHAR(50) UNIQUE NOT NULL,
        PasswordHash VARCHAR(64) NOT NULL,
        Salt VARCHAR(16) NOT NULL,
        CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        Role ENUM('Admin', 'User') DEFAULT 'User' NOT NULL,
        IsActive BOOLEAN DEFAULT TRUE
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

    **Place the `appsettings.json` file in the correct folder**: After creating the `appsettings.json` file, place it in the `net8.0` folder inside the `bin\Debug` directory of your SimpleInventory project. This is where the .NET runtime will look for the configuration file during development.

    Example file path:
    ```
    SimpleInventory/bin/Debug/net8.0/appsettings.json
    ```
   
    **Add `appsettings.json` to `.gitignore`**:

    ```
    appsettings.json
    ```




    ## Tools Needed
    - [MySQL Workbench](https://dev.mysql.com/downloads/workbench/)
    - [.NET SDK](https://dotnet.microsoft.com/download)

    
    ## License

   This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for more details.
