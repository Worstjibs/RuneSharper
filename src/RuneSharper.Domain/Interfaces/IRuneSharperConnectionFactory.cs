using System.Data;

public interface IRuneSharperConnectionFactory
{
    IDbConnection GetConnection();
}