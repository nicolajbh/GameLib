using database;

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
