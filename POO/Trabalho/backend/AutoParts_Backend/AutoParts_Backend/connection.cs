using Npgsql;

namespace AutoParts_Backend;

public class connection
{
    #region PRIVATE PROPERTIES
    private static string Host = "fnlo3j9a7h.ckl6x663wq.tsdb.cloud.timescale.com";
    private static string User = "tsdbadmin";
    private static string DBname = "tsdb";
    private static string Port = "39931";
    private static string Password = "ykfgrhgfafd2830m";
    #endregion
    
    
    
    public static NpgsqlConnection GetConnection()
    {
        string connString = String.Format(
            "Server={0};Username={1};Database={2};Port={3};Password={4}",
            Host,
            User,
            DBname,
            Port,
            Password);
        NpgsqlConnection conn = new NpgsqlConnection(connString);
        return conn;
    }
}