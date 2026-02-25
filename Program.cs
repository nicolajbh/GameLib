using database;

internal class Program
{
  public static void Main(string[] args)
  {
    // Game game = new Game(1, "Elden Ring", 59.99m, 9.5);
    // User? user = UserRepository.GetUserById(2);
    // StoreRepository.PurchaseGame(game, user);

    PrintWelcomeScreen();
    User? user = Login();
    if (user == null)
    {
      Console.WriteLine("No users found or login failed. Exiting...");
      return;
    }
    ShowMainMenu(user);
  }

  static User Login()
  {
    List<User>? availableUsers = UserRepository.GetUsers();
    if (availableUsers == null || availableUsers.Count == 0)
      return null;

    List<string> userNames = availableUsers.Select(u => u.Name).ToList();
    int selectedIndex = 0;
    while (true)
    {
      Console.Clear();
      Console.WriteLine("Select a user to login:\n");
      PrintMenu(userNames, selectedIndex);

      ConsoleKey key = Console.ReadKey(intercept: true).Key;
      if (key == ConsoleKey.Enter) break;

      selectedIndex = HandleNavigation(key, selectedIndex, userNames.Count);
    }
    Console.Clear();
    return availableUsers[selectedIndex];
  }

  enum ViewMode { Library, Store }

  static void ShowMainMenu(User user)
  {

    ViewMode currentView = ViewMode.Library;
    List<Game> displayGames = UserRepository.GetUserLibrary(user)?.Games ?? new List<Game>();
    int selectedIndex = 0; // index to track selected menu item
    while (true)
    {
      Console.Clear();
      string header = $"Library(1) | Store(2) | Logged in as: {user.Name} | Balance: {user.Wallet.Balance:C}";
      Console.WriteLine(header);
      Console.WriteLine(new string('-', header.Length));

      List<string> menuItems = displayGames.Select(g => g.Title).ToList();

      if (menuItems.Count == 0)
      {
        Console.WriteLine($"No games found in {currentView}.");
      }
      else
      {
        PrintMenu(menuItems, selectedIndex);
      }
      ConsoleKey key = Console.ReadKey(intercept: true).Key;

      switch (key)
      {
        case ConsoleKey.UpArrow:
        case ConsoleKey.DownArrow:
          selectedIndex = HandleNavigation(key, selectedIndex, menuItems.Count);
          break;
        case ConsoleKey.Enter:
          if (currentView == ViewMode.Store && displayGames.Count > 0)
          {
            Console.WriteLine("Purchased game!");
            Console.ReadKey();
          }
          break;

        case ConsoleKey.D1:
          currentView = ViewMode.Library;
          displayGames = UserRepository.GetUserLibrary(user)?.Games ?? new List<Game>();
          selectedIndex = 0;
          break;

        case ConsoleKey.D2:
          currentView = ViewMode.Store;
          displayGames = GameRepository.GetAllGames() ?? new List<Game>();
          selectedIndex = 0;
          break;
      }
    }
  }

  static void PrintWelcomeScreen()
  {
    Console.Clear();
    Console.WriteLine(@"                                  Welcome to the GameLib v1.0!");
    Console.WriteLine(
        @"===================================================================================================="
    );
    Console.WriteLine("                                    Press any key to start...");
    Console.ReadKey();
  }

  static void PrintMenu(List<string> menu, int currentIndex)
  {
    for (int i = 0; i < menu.Count; i++)
    {
      if (i == currentIndex)
      {
        Console.WriteLine($"> {menu[i]}");
      }
      else
      {
        Console.WriteLine($"  {menu[i]}"); // spaces to write over previously selected items
      }
    }
    Console.WriteLine("\nUse ^/v arrows to navigate, Enter to select");
  }

  static int MenuSelect(string[] menu, int currentIndex)
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

  static int HandleNavigation(ConsoleKey key, int currentIndex, int maxItems)
  {
    if (maxItems == 0) return 0;

    return key switch
    {
      ConsoleKey.UpArrow => Math.Max(0, currentIndex - 1), // prevent negative index
      ConsoleKey.DownArrow => Math.Min(maxItems - 1, currentIndex + 1), // prevent index going over lenght of menu
      _ => currentIndex,
    };
  }
}


// User? user = UserRepository.GetUserById(4);
//
// if (user == null)
//     return;
//
// Library? library = UserRepository.GetUserLibrary(user);
//
// if (library != null)
// {
//     foreach (Game game in library.Games)
//     {
//         Console.WriteLine(
//             $"Game: {game.Id}, {game.Title}, {game.Price}, {game.Rating} {game.PrintGameCategories()}, Studio Behind The Game {game.Studio.Name} Size of Studio: {game.Studio.Size}"
//         );
//     }
// }
