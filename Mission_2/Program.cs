using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Welcome to the dice throwing simulator!");
        Console.Write("How many dice rolls would you like to simulate? ");
        int rolls;

        // Validate user input
        while (!int.TryParse(Console.ReadLine(), out rolls) || rolls <= 0)
        {
            Console.WriteLine("Please enter a valid positive number.");
            Console.Write("How many dice rolls would you like to simulate? ");
        }

        Console.WriteLine("\nDICE ROLLING SIMULATION RESULTS");
        Console.WriteLine("Each \"*\" represents 1% of the total number of rolls.");
        Console.WriteLine($"Total number of rolls = {rolls}.\n");

        // Use DiceSimulator class to perform dice rolling
        DiceSimulator simulator = new DiceSimulator();
        int[] results = simulator.SimulateRolls(rolls);

        // Display histogram
        for (int i = 0; i < results.Length; i++)
        {
            int percentage = (int)Math.Round((double)results[i] * 100 / rolls);
            Console.WriteLine($"{i + 2}: {new string('*', percentage)}");
        }

        Console.WriteLine("\nThank you for using the dice throwing simulator. Goodbye!");
    }
}

public class DiceSimulator
{
    private Random random = new Random();

    // Method to simulate dice rolls and return the results
    public int[] SimulateRolls(int rolls)
    {
        int[] outcomes = new int[11]; // Array to track sums from 2 to 12

        for (int i = 0; i < rolls; i++)
        {
            int dice1 = random.Next(1, 7); // Simulate dice 1
            int dice2 = random.Next(1, 7); // Simulate dice 2
            int sum = dice1 + dice2;

            outcomes[sum - 2]++; // Increment count for the sum
        }

        return outcomes;
    }
}