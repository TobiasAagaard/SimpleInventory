namespace SimpleInventory.Utilities
{
    public static class ConsoleHelpers
    {
        public static int ReadInt(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result >= 0)
                    return result;
                Console.WriteLine("Invalid input. Please enter a non-negative integer.");
            }
        }

        public static decimal ReadDecimal(string prompt)
        {
            decimal result;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out result) && result >= 0)
                    return result;
                Console.WriteLine("Invalid input. Please enter a non-negative decimal.");
            }
        }

        public static int? ReadOptionalInt(string prompt)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out var result))
                return result;
            return null;
        }

        public static decimal? ReadOptionalDecimal(string prompt)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out var result))
                return result;
            return null;
        }
    }
}
