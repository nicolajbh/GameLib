using Microsoft.Data.SqlClient;

namespace database;

public static class GameRepository
{
    static string connectionString = DBConnection.Connection;

    public static Library? GetLibrary(int userId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                const string query =
                    @"
					SELECT g.game_id, g.title, g.price, g.rating, c.name as category_name, c.category_id
					FROM game g
					INNER JOIN library l ON g.game_id = l.game_id
					LEFT JOIN game_category gc ON g.game_id = gc.game_id
					LEFT JOIN category c ON gc.category_id = c.category_id
					WHERE l.user_id = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", userId);

                SqlDataReader reader = command.ExecuteReader();
                var gamesDict = new Dictionary<int, Game>();

                while (reader.Read())
                {
                    int gameId = reader.GetInt32(reader.GetOrdinal("game_id"));

                    if (!gamesDict.TryGetValue(gameId, out Game? game))
                    {
                        string title = reader.GetString(reader.GetOrdinal("title"));
                        decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
                        double rating = reader.GetDouble(reader.GetOrdinal("rating"));
                        game = new Game(gameId, title, price, rating);
                        gamesDict[gameId] = game;
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("category_name")))
                    {
                        string category_name = reader.GetString(reader.GetOrdinal("category_name"));
                        int category_id = reader.GetInt32(reader.GetOrdinal("category_id"));
                        game.Categories.Add(new Category(category_name, category_id));
                    }
                }
                reader.Close();

                var games = new List<Game>(gamesDict.Values);
                User? user = UserRepository.GetUserById(userId);
                if (user != null)
                    return new Library(user, games);

                return null;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception Occured: {ex.Message}");
            return null;
        }
    }
}
