using System.Data;
using Microsoft.Data.SqlClient;

namespace RuneSharper.Data.Connections;

public class RuneSharperConnectionFactory : IRuneSharperConnectionFactory, IDisposable
{
    private readonly IDbConnection _connection;

    public RuneSharperConnectionFactory(string connectionString)
    {
        _connection = new SqlConnection(connectionString);
    }

    public IDbConnection GetConnection()
    {
        return _connection;
    }

    public void Dispose()
    {
        if (_connection.State != ConnectionState.Closed)
            _connection.Dispose();
    }
}
