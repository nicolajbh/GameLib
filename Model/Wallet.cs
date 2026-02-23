namespace database;

public class Wallet
{
  public int Id { get; set; }
  public decimal Balance { get; set; }

  public Wallet(int id, decimal balance)
  {
    Id = id;
    Balance = balance;
  }
}
