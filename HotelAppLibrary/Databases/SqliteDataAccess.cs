
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HotelAppLibrary.Databases;

public class SqliteDataAccess : ISqliteDataAccess
{
    private readonly IConfiguration config;

    public SqliteDataAccess(IConfiguration config)
    {
        SQLitePCL.Batteries.Init();
        this.config = config;
    }
    public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName)
    {
        var connectionString = config.GetConnectionString(connectionStringName);

        using (IDbConnection db = new SqliteConnection(connectionString))
        {
            List<T> rows = db.Query<T>(sqlStatement, parameters).ToList();
            return rows;
        }
    }

    public void SaveData<T>(string sqlStatement, T parameters, string connectionStringName)
    {
        var connectionString = config.GetConnectionString(connectionStringName);


        using (IDbConnection connection = new SqliteConnection(connectionString))
        {
            connection.Execute(sqlStatement, parameters);
        }
    }
}
