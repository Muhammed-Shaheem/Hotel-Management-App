using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;
using Microsoft.Extensions.Configuration;

namespace HotelAppLibrary.Data;

public class SqlData
{
    private IDataAccess db;
    private const string connectionStringName = "SqlDb";


    public SqlData(IDataAccess db)
    {

        this.db = db;
    }
    public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate,DateTime endDate)
    {
        

        return db.LoadData<RoomTypeModel,dynamic>("dbo.spRoomTypes_GetAvailableRoomTypes",
                                                           new { startDate, endDate },
                                                           connectionStringName,
                                                           true);
    }
}
