namespace database;

public class Library
{
    public User User { get; set; }
    public List<Game> Games { get; set; } = new List<Game>();

    public Library() { }
}
