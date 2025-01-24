namespace Mission_3;

public class FoodItems
{
    private string GetInput(string prompt)
    {
        string input = "";
        while (string.IsNullOrWhiteSpace(input)) // Ensure non-empty input
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine();
        }
        return input;
    }

    private List<FoodItem> Inventory = new List<FoodItem>();

    // Define the FoodItem class
    public class FoodItem
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string ExpirationDate { get; set; }
    }

    public void AddFoodItem()
    {
        // Create a new FoodItem object
        FoodItem foodItem = new FoodItem
        {
            Index = Inventory.Count + 1,
            Name = GetInput("Name: "),
            Category = GetInput("Category: ")
        };

        // Validate Quantity input
        int quantity;
        while (true)
        {
            string quantityInput = GetInput("Quantity (must be a positive number): ");
            if (int.TryParse(quantityInput, out quantity) && quantity >= 0)
            {
                foodItem.Quantity = quantity;
                break; // Exit loop if input is valid and non-negative
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid positive number for Quantity.");
            }
        }

        // Get Expiration Date
        foodItem.ExpirationDate = GetInput("Expiration Date (mm/dd/yyyy): ");

        // Add the FoodItem to the inventory
        Inventory.Add(foodItem);

        Console.WriteLine("Food item added successfully!");
    }

    public void FoodItemList()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("No inventory available.");
            return;
        }

        Console.WriteLine("\nCurrent Food Items:");
        foreach (var item in Inventory)
        {
            Console.WriteLine(
                $"Index: {item.Index}, Name: {item.Name}, Category: {item.Category}, Quantity: {item.Quantity} units, Expiration Date: {item.ExpirationDate}");
        }
    }

    public void DeleteFoodItem()
    {
        // Check if the inventory is empty
        if (Inventory.Count == 0)
        {
            Console.WriteLine("No items to delete. The inventory is empty.");
            return;
        }

        // Display current food items
        Console.WriteLine("\nCurrent Food Items:");
        FoodItemList();

        // Get the Index to delete from the user
        int indexToDelete;
        while (!int.TryParse(GetInput("Enter the Index of the food item to delete: "), out indexToDelete))
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
        }

        // Find the item with the specified Index
        var itemToDelete = Inventory.FirstOrDefault(item => item.Index == indexToDelete);

        if (itemToDelete != null)
        {
            // Remove the item from the inventory
            Inventory.Remove(itemToDelete);
            Console.WriteLine($"Item with Index {indexToDelete} deleted successfully!");

            // Update Indexes to maintain sequence
            UpdateIndexes();
        }
        else
        {
            Console.WriteLine($"No item found with Index {indexToDelete}.");
        }
    }

    // Method to update Indexes after deletion
    private void UpdateIndexes()
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            Inventory[i].Index = i + 1;
        }
    }
}