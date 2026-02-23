using Microsoft.Data.SqlClient;

namespace database;

public static class UserRepository
{
  static string connectionString = DBConnection.Connection;

  public static User? GetUserById(int userId)
  {
    try
    {
      using (SqlConnection connection = new SqlConnection(connectionString))
      {
        connection.Open();

        const string query = "SELECT * from users WHERE @ID = user_id";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@ID", userId);

        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          int fetchedId = reader.GetInt32(reader.GetOrdinal("user_id"));
          string fetchedName = reader.GetString(reader.GetOrdinal("name"));
          Console.WriteLine($"ID Fetched: {fetchedId}, Name Fetched: {fetchedName}");
          Wallet wallet = WalletRepository.GetWalletById(userId);
          User fetchedUser = new(fetchedId, fetchedName, wallet);
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
}
