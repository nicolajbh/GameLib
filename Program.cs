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
        ShowMainMenu(user);
    }

    static User Login()
    {
        List<User>? availableUsers = UserRepository.GetUsers();
        string[] userNames = availableUsers.Select(u => u.Name).ToArray();
        if (availableUsers == null)
            return null;

        int selectedIndex = 0; // index to track selected menu item
        while (true)
        {
            Console.Clear();
            PrintMenu(userNames, selectedIndex);
            int result = MenuSelect(userNames, selectedIndex);
            if (result == -1)
                break; // user made selection, exit menu loop
            selectedIndex = result;
        }
        Console.Clear();
        return availableUsers[selectedIndex];
    }

    static void ShowMainMenu(User user)
    {
        Library userLibrary = UserRepository.GetUserLibrary(user);

        string[] gameTitles = userLibrary.Games.Select(g => g.Title).ToArray();

        int selectedIndex = 0; // index to track selected menu item
        while (true)
        {
            Console.Clear();
            Console.WriteLine(
                $"Library(1) | Store(2) | Logged in as: {user.Name} | Balance: {user.Wallet.Balance}"
            );
            PrintMenu(gameTitles, selectedIndex);
            int result = MenuSelect(gameTitles, selectedIndex);
            if (result == -1)
                break; // user made selection, exit menu loop
            selectedIndex = result;
        }
        Console.Clear();
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

    static void PrintMenu(string[] menu, int currentIndex)
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

    static int MenuSelect(string[] menu, int currentIndex)
    {
        return Console.ReadKey().Key switch
        {
            ConsoleKey.UpArrow => Math.Max(0, currentIndex - 1), // prevent negative index
            ConsoleKey.DownArrow => Math.Min(menu.Length - 1, currentIndex + 1), // prevent index going over lenght of menu
            ConsoleKey.Enter => -1,
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
