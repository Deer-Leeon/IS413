namespace Mission_3;

public class Program
{
    public static void Main()
    {
        FoodItems foodItemManager = new FoodItems();
        string? state = "";
        while (!string.Equals(state, "exit", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Do you want to print the list of current Food Items? (p), add a Food Item? (a), or delete Food Item? (d). Please type 'exit' to exit program");
            state = Console.ReadLine();
            if (string.Equals(state, "a", StringComparison.OrdinalIgnoreCase))
            {
                foodItemManager.AddFoodItem();
            }
            else if (string.Equals(state, "d", StringComparison.OrdinalIgnoreCase))
            {
                foodItemManager.DeleteFoodItem();
            }
            else if (string.Equals(state, "p", StringComparison.OrdinalIgnoreCase))
            {
                foodItemManager.FoodItemList();
            }
            else if (string.Equals(state, "exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exit Program");
                break;
            }
        }
    }
}