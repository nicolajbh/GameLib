namespace database;

public class Order
{
    public int Id { get; set; }
    public User User { get; set; }
    public Game Game { get; set; }
    public DateTime Date { get; set; }

    public Order() { }
}
