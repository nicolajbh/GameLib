using Microsoft.Data.SqlClient;

namespace database;

public static class GameRepository
{
  static string connectionString = DBConnection.Connection;
  public static List<Game> GetAllGames()
  {
    List<Game> games = new List<Game>();
    try
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();
        const string query = "SELECT * from game";
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          int id = reader.GetInt32(reader.GetOrdinal("game_id"));
          string title = reader.GetString(reader.GetOrdinal("title"));
          decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
          double rating = reader.GetDouble(reader.GetOrdinal("rating"));
          Game game = new Game(
              id,
              title,
              price,
              rating
           );
          games.Add(game);
        }
        reader.Close();
      }
      return games;
    }
    catch (SqlException ex)
    {
      Console.WriteLine($"SQL Exception Occured: {ex.Message}");
      return null;
    }
  }
}
