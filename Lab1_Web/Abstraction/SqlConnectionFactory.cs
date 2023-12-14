using Microsoft.Data.Sqlite;

namespace Lab1_Web.Abstraction;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
        => _configuration = configuration;

    /// <summary>
    /// Create new sql connection without bothering yourself
    /// about connection string
    /// </summary>
    /// <returns></returns>
    public SqliteConnection CreateConnection()
    {
        return new SqliteConnection(
            _configuration.GetConnectionString("DefaultConnection"));
    }
}