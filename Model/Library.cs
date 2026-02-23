namespace database;

public class Library
{
    public User User { get; set; }
    public List<Game> Games { get; set; } = [];

    public Library(User user, List<Game> games)
    {
        User = user;
        Games = games;
    }
}
