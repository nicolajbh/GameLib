using database.Utils;
using Microsoft.Data.SqlClient;

namespace database;

public static class StoreRepository
{
  static string connectionString = DBConnection.Connection;

  public static List<decimal> GetCreditOptions() => new List<decimal> { 5m, 10m, 20m, 30m, 50m, 100m };

  public static void PurchaseCredits(User user, decimal credits)
  {
    if (user == null)
    {
      Console.WriteLine("Purchase Failed: User data is missing.");
      return;
    }
    if (user.Wallet == null)
    {
      Console.WriteLine("Purchase Failed: User wallet is missing.");
      return;
    }
    if (user.Wallet.Balance < 0)
    {
      Console.WriteLine("Purchase Failed: Wallet balance is negative.");
      return;
    }

    bool result = UserRepository.UpdateBalance(user, credits);
    if (result)
    {
      user.Wallet.Balance = user.Wallet.Balance + credits;
    }
  }


  public static void PurchaseGame(Game game, User user)
  {
    if (game == null)
    {
      Console.WriteLine("Purchase Failed: Game data is missing.");
      return;
    }

    if (user == null)
    {
      Console.WriteLine("Purchase Failed: User data is missing.");
      return;
    }

    if (user.Wallet == null)
    {
      Console.WriteLine("Purchase Failed: User wallet is missing.");
      return;
    }

    if (user.Wallet.Balance < game.Price)
    {
      Console.WriteLine(
          $"Purchase Failed: {user.Wallet.Balance} is insufficient to buy: {game.Title}, costs: {game.Price}"
      );
      return;
    }

    try
    {
      string query =
          "INSERT INTO [order] (user_id, game_id, date) VALUES (@USERID, @GAMEID, @DATE)";

      using SqlConnection connection = new SqlConnection(connectionString);
      connection.Open();

      using SqlCommand command = new SqlCommand(query, connection);
      command.Parameters.AddRange(
          new SqlParameter[]
          {
                    new("@USERID", user.Id),
                    new("@GAMEID", game.Id),
                    new("@DATE", DateTime.Now),
          }
      );

      command.ExecuteNonQuery();
      Console.WriteLine(
          $"Successfully inserted {user.Id} {game.Id} {DateTime.Now} into the DB."
      );
      return;
    }
    catch (SqlException ex)
    {
      Console.WriteLine("Purchase Failed: A database error occurred: " + ex.Message);
    }
  }
}
