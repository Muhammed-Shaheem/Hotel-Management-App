using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace HotelAppLibrary.Databases;

public class SqlDataAccess : IDataAccess
{
    private readonly IConfiguration config;

    public SqlDataAccess(IConfiguration config)
    {
        this.config = config;
    }

    public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName, bool isStoreProcedure = false)
    {
        config.GetConnectionString(connectionStringName);
        CommandType cmdType = CommandType.Text;
        if (isStoreProcedure == true)
        {
            cmdType = CommandType.StoredProcedure;
        }
        using (IDbConnection connection = new SqlConnection())
        {
            List<T> rows = connection.Query<T>(sqlStatement, parameters, commandType: cmdType).ToList();
            return rows;
        }

    }

    public void SaveData<T>(string sqlStatement, T parameters, string connectionStringName, bool isStoreProcedure)
    {
        CommandType cmdType = CommandType.Text;
        if (isStoreProcedure == true)
        {
            cmdType = CommandType.StoredProcedure;
        }
        using (IDbConnection connection = new SqlConnection(config.GetConnectionString(connectionStringName)))
        {
            connection.Execute(sqlStatement, parameters, commandType: cmdType);
        }
    }
}


