using Microsoft.Data.SqlClient;

namespace database;

public static class UserRepository
{
  static string connectionString = DBConnection.Connection;

  public static List<User>? GetUsers()
  {
    List<User> users = new List<User>();
    try
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

        const string query = "SELECT user_id, name, wallet_id, balance from vw_UserDetails";

        SqlCommand command = new SqlCommand(query, connection);

        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          int userId = reader.GetInt32(reader.GetOrdinal("user_id"));
          string userName = reader.GetString(reader.GetOrdinal("name"));
          int walletId = reader.GetInt32(reader.GetOrdinal("wallet_id"));
          decimal balance = reader.GetDecimal(reader.GetOrdinal("balance"));

          Wallet wallet = new Wallet(walletId, balance);
          users.Add(new User(userId, userName, wallet));
        }
        return users;
      }
    }
    catch (SqlException ex)
    {
      Console.WriteLine($"SQL Exception Occured: {ex.Message}");
      return null;
    }
  }

  public static User? GetUserById(int userId)
  {
    try
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

        const string query = "SELECT * from vw_UserDetails WHERE user_id = @ID";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ID", userId);

        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          int fetchedUserId = reader.GetInt32(reader.GetOrdinal("user_id"));
          string fetchedName = reader.GetString(reader.GetOrdinal("name"));
          decimal fetchedBalance = reader.GetDecimal(reader.GetOrdinal("balance"));
          int fetchedWalletId = reader.GetInt32(reader.GetOrdinal("wallet_id"));
          Wallet wallet = new Wallet(fetchedWalletId, fetchedBalance);
          Console.WriteLine($"ID Fetched: {fetchedUserId}, Name Fetched: {fetchedName}");
          User fetchedUser = new(fetchedUserId, fetchedName, wallet);
          return fetchedUser;
        }
        reader.Close();
      }
      return null;
    }
    catch (SqlException ex)
    {
      Console.WriteLine($"SQL Exception Occured: {ex.Message}");
      return null;
    }
  }

  public static Library GetUserLibrary(User user)
  {
    {
      Library library = new Library(user, new List<Game>());
      var gameMap = new Dictionary<int, Game>();
      try
      {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
          const string query =
              @"
                    SELECT user_id, game_id, GameTitle, Price, Rating, category_id, CategoryName, StudioName, studio_size, developer_id 
                    FROM vw_UserGameLibrary 
                    WHERE user_id = @UserId";

          SqlCommand command = new SqlCommand(query, connection);
          command.Parameters.AddWithValue("@UserId", user.Id);
          connection.Open();
          using (SqlDataReader reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              int gameId = reader.GetInt32(reader.GetOrdinal("game_id"));
              if (!gameMap.TryGetValue(gameId, out Game? game))
              {
                game = new Game(
                    gameId,
                    reader.GetString(reader.GetOrdinal("GameTitle")),
                    reader.GetDecimal(reader.GetOrdinal("Price")),
                    reader.GetDouble(reader.GetOrdinal("Rating"))
                );
                int developer_id = reader.GetInt32(
                    reader.GetOrdinal("developer_id")
                );
                string studioName = reader.GetString(
                    reader.GetOrdinal("StudioName")
                );
                int studioSize = reader.GetInt32(reader.GetOrdinal("studio_size"));
                Developer developer = new Developer(
                    studioName,
                    studioSize,
                    developer_id
                );
                game.Studio = developer;
                gameMap.Add(gameId, game);
                library.Games.Add(game);
              }
              if (!reader.IsDBNull(reader.GetOrdinal("category_id")))
              {
                var category = new Category(
                    reader.GetString(reader.GetOrdinal("CategoryName")),
                    reader.GetInt32(reader.GetOrdinal("category_id"))
                );
                game.Categories.Add(category);
              }
            }
          }
        }
        return library;
      }
      catch (SqlException ex)
      {
        Console.WriteLine($"SQL Exception Occurred: {ex.Message}");
        return null;
      }
    }
  }

  public static bool UpdateBalance(User user, decimal balanceChange)
  {
    try
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        const string sql =
            "UPDATE wallet SET balance = balance + @NewBalance WHERE user_id = @UserId";
        SqlCommand cmd = new SqlCommand(sql, connection);

        cmd.Parameters.AddWithValue("@NewBalance", balanceChange);
        cmd.Parameters.AddWithValue("@UserId", user.Id);

        connection.Open();
        return cmd.ExecuteNonQuery() > 0;
      }
    }
    catch (SqlException ex)
    {
      Console.WriteLine($"SQL Exception Occured: {ex.Message}");
      return false;
    }
  }
}
