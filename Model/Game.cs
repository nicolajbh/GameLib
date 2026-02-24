namespace database;

public class Game
{
    public int Id { get; set; }

    public string Title { get; set; }

    public decimal Price { get; set; }

    public double Rating { get; set; }

    public List<GameCategory> Categories { get; set; } = [];

    public Game(int id, string title, decimal price, double rating)
    {
        Id = id;
        Title = title;
        Price = price;
        Rating = rating;
        Categories = [];
    }
}
