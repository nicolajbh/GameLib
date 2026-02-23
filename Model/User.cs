namespace database;

public class User
{
  public int Id { get; set; }
  public string Name { get; set; }
  public Wallet Wallet { get; set; }

  public User(int id, string name, Wallet wallet)
  {
    Id = id;
    Name = name;
    Wallet = wallet;
  }
}
