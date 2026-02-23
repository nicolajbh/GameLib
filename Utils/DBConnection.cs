namespace database;

public static class DBConnection
{
  public static string Connection { get; private set; } =
      "Server=localhost\\SQLEXPRESS;Database=GameDB;Trusted_Connection=True;TrustServerCertificate=True;";
}
