using database;

User? user = UserRepository.GetUserById(2);

Library? library = GameRepository.GetLibrary(1);

if (library != null)
{
    foreach (Game game in library.Games)
    {
        Console.WriteLine(
            $"Game: {game.Id}, {game.Title}, {game.Price}, {game.Rating} {game.PrintGameCategories()}"
        );
    }
}
