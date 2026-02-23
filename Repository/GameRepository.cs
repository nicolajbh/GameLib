using System.Security.Cryptography;
using Microsoft.Data.SqlClient;

namespace database;

public static class GameRepository
{
    static string connectionString = DBConnection.Connection;

    public static Library? GetLibrary(int userId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString)) { }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Exception Occured: {ex.Message}");
        }
    }
}
