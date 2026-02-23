namespace database;

public class Game
{
    public int Id { get; set; }

    public string Title { get; set; }

    public decimal Price { get; set; }

    public float Rating { get; set; }

    public List<Category> Categories { get; set; } = [];

    public Game(int id, string title, decimal price, float rating)
    {
        Id = id;
        Title = title;
        Price = price;
        Rating = rating;
    }
}
