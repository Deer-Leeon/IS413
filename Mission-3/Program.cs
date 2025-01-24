namespace Mission_3;

public class Program
{
    public static void Main()
    {
        // Create an instance of the FoodItems class to manage food inventory
        FoodItems foodItemManager = new FoodItems();
        
        // Initialize a variable to keep track of the program state
        string? state = "";

        // Run a loop until the user types 'exit'
        while (!string.Equals(state, "exit", StringComparison.OrdinalIgnoreCase))
        {
            // Display the menu options to the user
            Console.WriteLine("Do you want to print the list of current Food Items? (p), add a Food Item? (a), or delete Food Item? (d). Please type 'exit' to exit program");

            // Get the user's input
            state = Console.ReadLine();

            // If the user wants to add a food item
            if (string.Equals(state, "a", StringComparison.OrdinalIgnoreCase))
            {
                foodItemManager.AddFoodItem(); // Call the method to add a new food item
            }
            // If the user wants to delete a food item
            else if (string.Equals(state, "d", StringComparison.OrdinalIgnoreCase))
            {
                foodItemManager.DeleteFoodItem(); // Call the method to delete a food item
            }
            // If the user wants to print the list of food items
            else if (string.Equals(state, "p", StringComparison.OrdinalIgnoreCase))
            {
                foodItemManager.FoodItemList(); // Call the method to display the list of food items
            }
            // If the user types 'exit', terminate the program
            else if (string.Equals(state, "exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exit Program"); // Confirm program exit
                break; // Exit the while loop
            }
        }
    }
}