using System.Globalization;
using database;
using database.Utils;

internal class Program
{
  public static void Main(string[] args)
  {
    Menu.PrintWelcomeScreen();
    User? user = Login();
    if (user == null)
    {
      Console.WriteLine("No users found or login failed. Exiting...");
      return;
    }
    ShowMainMenu(user);
  }

  private static readonly CultureInfo EuroCulture = new CultureInfo("fr-FR");

  static User? Login()
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
      Menu.PrintMenu(userNames, selectedIndex);
      ConsoleKey key = Console.ReadKey(intercept: true).Key;
      if (key == ConsoleKey.Enter)
        break;
      selectedIndex = Menu.HandleNavigation(key, selectedIndex, userNames.Count);
    }

    Console.Clear();
    return availableUsers[selectedIndex];
  }

  enum ViewMode
  {
    Library,
    Store,
    CreditShop,
  }

  static void ShowMainMenu(User user)
  {
    ViewMode currentView = ViewMode.Library;
    List<Game> displayGames = UserRepository.GetUserLibrary(user)?.Games ?? new List<Game>();
    List<decimal> creditOptions = new List<decimal>();
    int selectedIndex = 0;
    List<string> menuItems = displayGames.Select(g => g.Title).ToList();

    while (true)
    {
      Console.Clear();
      string header =
          $"Library(1) | Store(2) | Buy Credits(3) | Logged in as: {user.Name} | Balance: {user.Wallet.Balance.ToString("C", EuroCulture)}";
      Console.WriteLine(header);
      Console.WriteLine(new string('-', header.Length));

      if (menuItems.Count == 0)
      {
        Console.WriteLine($"No games found in {currentView}.");
      }
      else
      {
        Menu.PrintMenu(menuItems, selectedIndex);
      }

      ConsoleKey key = Console.ReadKey(intercept: true).Key;

      switch (key)
      {
        case ConsoleKey.UpArrow:
        case ConsoleKey.DownArrow:
          selectedIndex = Menu.HandleNavigation(key, selectedIndex, menuItems.Count);
          break;

        case ConsoleKey.Enter:
          if (currentView == ViewMode.Store && displayGames.Count > 0)
          {
            StoreRepository.PurchaseGame(displayGames[selectedIndex], user);
            // Console.WriteLine($"You bought: {displayGames[selectedIndex].Title}");
            Console.ReadKey();
          }
          if (currentView == ViewMode.CreditShop && creditOptions.Count > 0)
          {
            StoreRepository.PurchaseCredits(user, creditOptions[selectedIndex]);
          }
          break;

        case ConsoleKey.D1:
          currentView = ViewMode.Library;
          displayGames = UserRepository.GetUserLibrary(user)?.Games ?? new List<Game>();
          menuItems = displayGames.Select(g => g.Title).ToList();
          selectedIndex = 0;
          break;

        case ConsoleKey.D2:
          currentView = ViewMode.Store;
          displayGames = GameRepository.GetAllGames() ?? new List<Game>();
          menuItems = displayGames.Select(g => g.Title).ToList();
          selectedIndex = 0;
          break;

        case ConsoleKey.D3:
          currentView = ViewMode.CreditShop;
          creditOptions = StoreRepository.GetCreditOptions();
          menuItems = creditOptions.Select(c => c.ToString("C", EuroCulture)).ToList();
          selectedIndex = 0;
          break;
        case ConsoleKey.D:
          while (true)
          {
            Console.Clear();
            Console.WriteLine("Are you sure you want to delete your user? All data will be lost. [Y/N]");
            string response = Console.ReadLine();
            if (response.ToUpper().Trim() == "Y")
            {
              UserRepository.DeleteUser(user);
              Console.WriteLine("Goodbye!");
              Console.ReadKey();
              Environment.Exit(0);
              break;
            }
            else if (response.ToUpper().Trim() == "N")
            {
              break;
            }
            else
            {
              Console.WriteLine("Please enter a valid answer");
            }
          }
          break;
      }
    }
  }
}
