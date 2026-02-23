namespace database;

public static class DBConnection
{
    public static string Connection { get; private set; } =
        "Server=DESKTOP-QMO62VG\\SQLEXPRESS;Database=GameDB;Trusted_Connection=True;TrustServerCertificate=True;";
}
