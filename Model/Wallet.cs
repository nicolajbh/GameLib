namespace database;

public class Wallet
{
    public int Id { get; set; }
    public User User { get; set; }
    public decimal Balance { get; set; }

    public Wallet(int id, User user, decimal balance)
    {
        Id = id;
        User = user;
        Balance = balance;
    }
}
