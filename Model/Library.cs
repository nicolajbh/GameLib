using System.Collections;

namespace database;

public class Library : IEnumerable<Game>
{
    public User User { get; set; }
    public List<Game> Games { get; set; } = [];

    public Library(User user, List<Game> games)
    {
        User = user;
        Games = games;
    }

    public IEnumerator<Game> GetEnumerator()
    {
        return Games.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
