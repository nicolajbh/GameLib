using database;

User? user = UserRepository.GetUserById(4);

if (user == null)
  return;

Library? library = UserRepository.GetUserLibrary(user);

if (library != null)
{
  foreach (Game game in library.Games)
  {
    Console.WriteLine(
        $"Game: {game.Id}, {game.Title}, {game.Price}, {game.Rating} {game.PrintGameCategories()}, Studio Behind The Game {game.Studio.Name} Size of Studio: {game.Studio.Size}"
    );
  }
}
