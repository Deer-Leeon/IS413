namespace Mission_3;

public class FoodItems
{
    // Helper method to get user input with a prompt and ensure it's not empty
    private string GetInput(string prompt)
    {
        string input = "";
        while (string.IsNullOrWhiteSpace(input)) // Ensure non-empty input
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine(); // Read input from the user
        }

        return input; // Return the validated input
    }

    // List to store food items as inventory
    private List<FoodItem> Inventory = new List<FoodItem>();

    // Class to represent a FoodItem
    public class FoodItem
    {
        public int Index { get; set; } // Unique identifier for the food item
        public string Name { get; set; } // Name of the food item
        public string Category { get; set; } // Category the food item belongs to
        public int Quantity { get; set; } // Quantity available in inventory
        public string ExpirationDate { get; set; } // Expiration date of the food item
    }

    // Method to add a new food item to the inventory
    public void AddFoodItem()
    {
        // Create a new FoodItem object and populate its properties
        FoodItem foodItem = new FoodItem
        {
            Index = Inventory.Count + 1, // Set Index as the next available number
            Name = GetInput("Name: "), // Get the name of the food item
            Category = GetInput("Category: ") // Get the category
        };

        // Validate Quantity input and ensure it's a non-negative integer
        int quantity;
        while (true)
        {
            string quantityInput = GetInput("Quantity (must be a positive number): ");
            if (int.TryParse(quantityInput, out quantity) && quantity >= 0)
            {
                foodItem.Quantity = quantity; // Assign valid quantity to the food item
                break; // Exit loop when input is valid
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid positive number for Quantity.");
            }
        }

        // Get expiration date from the user
        foodItem.ExpirationDate = GetInput("Expiration Date (mm/dd/yyyy): ");

        // Add the new food item to the inventory
        Inventory.Add(foodItem);

        Console.WriteLine("Food item added successfully!"); // Confirmation message
    }

    // Method to display the list of food items
    public void FoodItemList()
    {
        // Check if inventory is empty
        if (Inventory.Count == 0)
        {
            Console.WriteLine("No inventory available."); // Notify the user
            return;
        }

        // Print each food item in the inventory
        Console.WriteLine("\nCurrent Food Items:");
        foreach (var item in Inventory)
        {
            Console.WriteLine(
                $"Index: {item.Index}, Name: {item.Name}, Category: {item.Category}, Quantity: {item.Quantity} units, Expiration Date: {item.ExpirationDate}");
        }
    }

    // Method to delete a food item by its Index
    public void DeleteFoodItem()
    {
        // Check if inventory is empty
        if (Inventory.Count == 0)
        {
            Console.WriteLine("No items to delete. The inventory is empty.");
            return;
        }

        // Display the current food items
        Console.WriteLine("\nCurrent Food Items:");
        FoodItemList();

        // Get the Index of the item to delete
        int indexToDelete;
        while (!int.TryParse(GetInput("Enter the Index of the food item to delete: "), out indexToDelete))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }

        // Find the food item in the inventory with the given Index
        var itemToDelete = Inventory.FirstOrDefault(item => item.Index == indexToDelete);

        if (itemToDelete != null)
        {
            // Remove the food item from the inventory
            Inventory.Remove(itemToDelete);
            Console.WriteLine($"Item with Index {indexToDelete} deleted successfully!");

            // Update Indexes to maintain sequence
            UpdateIndexes();
        }
        else
        {
            Console.WriteLine($"No item found with Index {indexToDelete}."); // Notify if not found
        }
    }

    // Method to update Indexes after a deletion
    private void UpdateIndexes()
    {
        // Reassign Index values sequentially to maintain consistency
        for (int i = 0; i < Inventory.Count; i++)
        {
            Inventory[i].Index = i + 1;
        }
    }
}