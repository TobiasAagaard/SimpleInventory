# SimpleInventoryManager.GUI

This is the GUI project for managing inventory in the SimpleInventory system. It is built using WPF (Windows Presentation Foundation) as part of the SimpleInventory solution.

The GUI will allow users to:
- Add, edit, and delete inventory items using an intuitive graphical interface.
- View the current inventory in a user-friendly format.
- Easily manage items with clear forms and controls.

## Setup

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Set `SimpleInventoryManager.GUI` as the startup project.
4. Build and run the project to start the GUI.

## Users Table:

  ```sql
  CREATE TABLE Users (
    UserId INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    PasswordHash VARCHAR(64) NOT NULL,
    Salt VARCHAR(16) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

  ```

## License

This project is licensed under the MIT [LICENSE](../LICENSE.txt).
