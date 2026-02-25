using database.Utils;
using Microsoft.Data.SqlClient;

namespace database;

public static class StoreRepository
{
  static string connectionString = DBConnection.Connection;

  public static List<string> GetCreditOptions()
  {
    decimal[] fundsToChoose = [5m, 10m, 20m, 30m, 50m, 100m];
    List<string> fundsToChooseString = fundsToChoose.Select(f => f.ToString()).ToList();
    return fundsToChooseString;
  }

  public static void PurchaseCredits(User user)
  {
    decimal[] fundsToChoose = [5m, 10m, 20m, 30m, 50m, 100m];
    List<string> fundsToChooseString = fundsToChoose.Select(f => f.ToString()).ToList();
    int selectedIndex = 0;

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
      Console.WriteLine("Wallet Balance is negative. Please contact support.");
        decimal[] fundsToChoose = [5m, 10m, 20m, 30m, 50m, 100m];
        string[] fundsToChooseString = fundsToChoose.Select(f => f.ToString()).ToArray();
        int selectedIndex = 0;

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
            Console.WriteLine("Wallet Balance is negative. Please contact support.");
        }

        int result = Menu<decimal>.MenuSelect(fundsToChoose, selectedIndex);
        Menu<string>.PrintMenu(fundsToChooseString, selectedIndex);
        decimal credits = 0;
        // Fix the ability to be able to pick an amount from the menu.
        // use in command.Params

        const string query = @"UPDATE wallet SET balance = @BALANCE WHERE user_id = @userId";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@BALANCE", user.Wallet.Balance + credits);
                command.Parameters.AddWithValue("@userID", user.Id);

                command.ExecuteNonQuery();
                Console.WriteLine(
                    $"Successfully updated wallet balance for: {user.Id} balance is now: {user.Wallet.Balance}"
                );
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Purchase Failed: A database error occurred: " + ex.Message);
            return;
        }

        UserRepository.UpdateBalance(user, credits);
    }


    ConsoleKey key = Console.ReadKey(intercept: true).Key;
    int maxItems = fundsToChoose.Count();
    int result = Menu.HandleNavigation(key, selectedIndex, maxItems);
    Menu.PrintMenu(fundsToChooseString, selectedIndex);
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
      return;
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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
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
            }
            return;
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Purchase Failed: A database error occurred: " + ex.Message);
            return;
        }
    }
  }
}
