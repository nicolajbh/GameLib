using Microsoft.Data.SqlClient;

namespace database;

public class WalletRepository
{
  static readonly string connectionString = DBConnection.Connection;

  public static Wallet GetWalletById(int user_id)
  {
    Wallet userWallet = new Wallet(user_id, 0);
    try
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

        const string query = "SELECT * from wallet WHERE @ID = user_id";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ID", user_id);

        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          int fetchedId = reader.GetInt32(reader.GetOrdinal("user_id"));
          decimal fetchedBalance = reader.GetDecimal(reader.GetOrdinal("balance"));
          Console.WriteLine($"Balance for user id {fetchedId}: {fetchedBalance}");
          userWallet = new(fetchedId, fetchedBalance);
        }
        reader.Close();
        return userWallet;
      }
    }
    catch (SqlException ex)
    {
      Console.WriteLine($"SQL Exception Occured: {ex.Message}");
      return userWallet;
    }
  }
}
