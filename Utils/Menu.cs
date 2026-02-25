namespace database.Utils;

public static class Menu<T>
{
    public static void PrintMenu(T[] menu, int currentIndex)
    {
        for (int i = 0; i < menu.Length; i++)
        {
            if (i == currentIndex)
            {
                Console.WriteLine($"> {menu[i]}");
            }
            else
            {
                Console.WriteLine($"{menu[i]}"); // spaces to write over previously selected items
            }
        }
        Console.WriteLine("\nUse ^/v arrows to navigate, Enter to select");
    }

    public static int MenuSelect(T[] menu, int currentIndex)
    {
        return Console.ReadKey().Key switch
        {
            ConsoleKey.UpArrow => Math.Max(0, currentIndex - 1), // prevent negative index
            ConsoleKey.DownArrow => Math.Min(menu.Length - 1, currentIndex + 1), // prevent index going over lenght of menu
            ConsoleKey.Enter => -1,
            ConsoleKey.D1 => -2,
            ConsoleKey.D2 => -3,
            _ => currentIndex,
        };
    }
}
