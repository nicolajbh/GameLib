using Microsoft.Data.SqlClient;

namespace database;

public static class StoreRepository
{
    static string connectionString = DBConnection.Connection;

    public static void PurchaseGame(Game game, User user)
    {
        if (user.Wallet.Balance < game.Price)
        {
            StoreRepository.PurchaseGameStatus(
                $"Purchase Failed: {user.Wallet.Balance} is insufficient to buy {game.Title}, costs {game.Price}"
            );
            return;
        }
        try
        {
            string query =
                "INSERT into order (user_id, game_id, date) VALUES (@USERID, @GAMEID, @DATE)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddRange(
                    new SqlParameter[]
                    {
                        new("@USERID", user.Id),
                        new("@GAMEID", game.Id),
                        new("@DATE", DateTime.Now),
                    }
                );
                command.ExecuteNonQuery();
                StoreRepository.PurchaseGameStatus(
                    $"Successfully inserted {user.Id} {game.Id} {DateTime.Now} into the DB."
                );
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception Occured: {ex.Message}");
        }
    }

    private static string PurchaseGameStatus(string msg)
    {
        return msg;
    }
}
