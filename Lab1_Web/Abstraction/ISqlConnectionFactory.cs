using Microsoft.Data.Sqlite;

namespace Lab1_Web.Abstraction;

public interface ISqlConnectionFactory
{
    /// <summary>
    /// Create new sql connection without bothering yourself
    /// about connection string
    /// </summary>
    /// <returns></returns>
    SqliteConnection CreateConnection();
}