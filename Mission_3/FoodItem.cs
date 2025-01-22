namespace Mission_3;

public class FoodItem
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

    private List<string[]> Inventory = new List<string[]>();

    public void AddFoodItem()
    {
        List<string> FoodItem = new List<string>();

        // Collect validated inputs
        string index = (Inventory.Count + 1).ToString();
        string name = GetInput("Name: ");
        string category = GetInput("Category: ");
        string quantity = GetInput("Quantity: ");
        string expDate = GetInput("Expiration Date (mm/dd/yyyy): ");

        // Add inputs to FoodItem list
        FoodItem.AddRange(new string[] { index, name, category, quantity, expDate });

        // Add FoodItem as an array to the inventory
        Inventory.Add(FoodItem.ToArray());

        Console.WriteLine("Food item added successfully!");
    }

    public void FoodItemList()
    {
        foreach (var item in Inventory)
        {
            Console.WriteLine(
                $"ItemID: {item[0]}, Name: {item[1]}, Category: {item[2]}, Quantity: {item[3]}, Expiration Date: {item[4]}");
        }

        if (Inventory.Count == 0)
        {
            Console.WriteLine("No inventory available.");
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

        // Get the ItemID to delete from the user
        string itemId = GetInput("Enter the ItemID of the food item to delete: ");

        // Find the item with the specified ItemID
        var itemToDelete = Inventory.FirstOrDefault(item => item[0] == itemId);

        if (itemToDelete != null)
        {
            // Remove the item from the inventory
            Inventory.Remove(itemToDelete);
            Console.WriteLine($"Item with ItemID {itemId} deleted successfully!");

            // Update ItemIDs to maintain sequence
            UpdateItemIDs();
        }
        else
        {
            Console.WriteLine($"No item found with ItemID {itemId}.");
        }
    }

// Method to update ItemIDs after deletion
    private void UpdateItemIDs()
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            Inventory[i][0] = (i + 1).ToString();
        }
    }
}