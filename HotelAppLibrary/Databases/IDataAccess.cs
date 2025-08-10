
namespace HotelAppLibrary.Databases;

public interface IDataAccess
{
    List<T> LoadData<T, U>(string sqlStatement,
                           U parameters,
                           string connectionStringName,
                           dynamic? options = null);
    void SaveData<T>(string sqlStatement,
                     T parameters,
                     string connectionStringName,
                     dynamic? options = null);
}