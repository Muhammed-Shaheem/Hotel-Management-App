using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

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
        var connectionString = config.GetConnectionString(connectionStringName);
        CommandType cmdType = CommandType.Text;

        if (isStoreProcedure == true)
        {
            cmdType = CommandType.StoredProcedure;
        }

        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            List<T> rows = connection.Query<T>(sqlStatement, parameters, commandType: cmdType).ToList();
            return rows;
        }

    }

    public void SaveData<T>(string sqlStatement, T parameters, string connectionStringName, bool isStoreProcedure)
    {
        var connectionString = config.GetConnectionString(connectionStringName);
        CommandType cmdType = CommandType.Text;

        if (isStoreProcedure == true)
        {
            cmdType = CommandType.StoredProcedure;
        }

        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            connection.Execute(sqlStatement, parameters, commandType: cmdType);
        }
    }
}


