namespace database;

public class Wallet
{
    public int Id { get; set; }
    public User User { get; set; }
    public decimal Balance { get; set; }

    public Wallet() { }
}
