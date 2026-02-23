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
                SELECT g.game_id, g.title, g.price, g.rating
                FROM game g
                INNER JOIN library l ON g.game_id = l.game_id
                WHERE l.user_id = @ID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", userId);

                SqlDataReader reader = command.ExecuteReader();
                var games = new List<Game>();

                while (reader.Read())
                {
                    int gameId = reader.GetInt32(reader.GetOrdinal("game_id"));
                    string title = reader.GetString(reader.GetOrdinal("title"));
                    decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
                    float rating = reader.GetFloat(reader.GetOrdinal("rating"));
                    games.Add(new Game(gameId, title, price, rating));
                }
                reader.Close();

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
