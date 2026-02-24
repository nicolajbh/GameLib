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
List<User>? availableUsers = UserRepository.GetUsers();

if (availableUsers == null)
    return;

User? selectedUserObject = null;

while (true)
{
    foreach (var user_ in availableUsers)
    {
        Console.WriteLine($"{user_.Id}: {user_.Name} - {user_.Wallet.Balance}");
    }

    Console.WriteLine();
    Console.WriteLine("Please select a user.\n");

    if (!int.TryParse(Console.ReadLine(), out int selectedUser))
    {
        Console.WriteLine("Invalid input");
        continue;
    }
    if (selectedUser < 1)
    {
        Console.WriteLine("Invalid Input. Please select a valid User ID.");
        continue;
    }

    User? user = UserRepository.GetUserById(selectedUser);

    if (user == null)
    {
        Console.WriteLine("Error Occured, user not found.");
    }
    selectedUserObject = user;
    break;
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
