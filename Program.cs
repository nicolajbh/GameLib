using database;

internal class Program
{
  public static void Main(string[] args)
  {
    PrintWelcomeScreen();
    ShowMainMenu();
  }

  static void ShowMainMenu()
  {
    var games = GameRepository.GetAllGames();
    int selectedIndex = 0; // index to track selected menu item
    while (true)
    {
      Console.Clear();
      PrintMenu(games, selectedIndex);
      int result = MenuSelect(games, selectedIndex);
      if (result == -1) break; // user made selection, exit menu loop
      selectedIndex = result;
    }
    Console.Clear();
  }

  static void PrintWelcomeScreen()
  {
    Console.Clear();
    Console.WriteLine(@"                                  Welcome to the GameLib v1.0!");
    Console.WriteLine(@"====================================================================================================");
    Console.WriteLine("                                    Press any key to start...");
    Console.ReadKey();
  }

  /// <summary>
  /// Loops through menu titles and prints to console
  /// Adds arrow to item currently selected from menu
  /// </summary>
  static void PrintMenu(List<Game> games, int currentIndex)
  {
    Console.WriteLine($"Library(1) | Store(2) | Logged in as: xx | Balance: xx");
    for (int i = 0; i < games.Count; i++)
    {
      if (i == currentIndex)
      {
        Console.WriteLine($"> {games[i].Title} - {games[i].Price}");
      }
      else
      {
        Console.WriteLine($"{games[i].Title} - {games[i].Price} "); // spaces to write over previously selected items
      }
    }
    Console.WriteLine("\nUse ^/v arrows to navigate, Enter to select");
  }

  static int MenuSelect(List<Game> games, int currentIndex)
  {
    return Console.ReadKey().Key switch
    {
      ConsoleKey.UpArrow => Math.Max(0, currentIndex - 1), // prevent negative index
      ConsoleKey.DownArrow => Math.Min(games.Count - 1, currentIndex + 1), // prevent index going over lenght of menu
      ConsoleKey.Enter => -1,
      _ => currentIndex,
    };
  }
}
