# SimpleInventory Console Application

This is the Console-based application for managing inventory in the SimpleInventory system. It allows users to manage inventory through a command-line interface.

## Features

- **Add an item**: Enter details like name, quantity, and price, and the item will be added to the inventory with a unique ID.
- **Delete an item**: View a list of current items and delete one by providing its unique ID.
- **View inventory**: Display all items in the inventory, including their ID, name, quantity, and price.


The Console application interacts with a MySQL database to store and manage inventory data. It uses a connection string specified in the `appsettings.json`  ( *Read the [main README](../README.md) for more information.* ) file to establish a connection. The database stores information such as item ID, name, quantity, and price, allowing for persistent inventory management.

- The application performs SQL queries to:
  - Add new items to the `Items` table.
  - Delete items based on their unique ID.
  - Retrieve and display all items in the inventory.

## License

This project is licensed under the MIT  [LICENSE](../LICENSE.txt).
