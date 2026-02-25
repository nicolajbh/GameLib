namespace database.Utils;

public static class Menu
{
  public static void PrintMenu<T>(List<T> menu, int currentIndex)
  {
    for (int i = 0; i < menu.Count; i++)
    {
      if (i == currentIndex)
        Console.WriteLine($"> {menu[i]}");
      else
        Console.WriteLine($"  {menu[i]}");
    }
    Console.WriteLine("\nUse ^/v arrows to navigate, Enter to select");
  }

  public static int HandleNavigation(ConsoleKey key, int currentIndex, int maxItems)
  {
    if (maxItems == 0) return 0;

    return key switch
    {
      ConsoleKey.UpArrow => Math.Max(0, currentIndex - 1),
      ConsoleKey.DownArrow => Math.Min(maxItems - 1, currentIndex + 1),
      _ => currentIndex,
    };
  }

  public static void PrintWelcomeScreen()
  {
    Console.Clear();
    Console.WriteLine(@"                                 Welcome to the GameLib v1.0!");
    Console.WriteLine(@"====================================================================================================");
    Console.WriteLine(@"                                 Press any key to start...");
    Console.ReadKey(intercept: true);
  }
}
