using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;

namespace HotelAppLibrary.Data;

public class SqlData : IDatabaseData
{
    private ISqlDataAccess db;
    private const string connectionStringName = "SqlDb";


    public SqlData(ISqlDataAccess db)
    {

        this.db = db;
    }


    public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
    {


        return db.LoadData<RoomTypeModel, dynamic>("dbo.spRoomTypes_GetAvailableRoomTypes",
                                                           new { startDate, endDate },
                                                           connectionStringName,
                                                           true);
    }

    public void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId)
    {
        GuestModel guest = db.LoadData<GuestModel, dynamic>("sp_Guests_Insert",
                                         new { firstName, lastName },
                                         connectionStringName,
                                         true).First();

        int price = db.LoadData<int, dynamic>("select price from roomtypes where id = @roomTypeId",
                                          new { roomTypeId },
                                          connectionStringName).First();

        var stayingTime = endDate.Date.Subtract(startDate.Date);
        decimal totalCost = stayingTime.Days * price;


        RoomModel room = db.LoadData<RoomModel, dynamic>(
            "spGetAvailableRooms",
            new { startDate, endDate, roomTypeId },
            connectionStringName,
            true).First();

        db.SaveData(
            "spBookings_Insert",
            new { roomId = room.Id, guestId = guest.Id, startDate, endDate, totalCost },
            connectionStringName,
            true);


    }

    public List<FullBookingModel> SearchBookings(string lastName)
    {
        return db.LoadData<FullBookingModel, dynamic>("spBookings_Search",
                     new { lastName },
                     connectionStringName,
                     true);

    }


    public void CheckInGuest(int id)
    {
        db.SaveData("spBookings_CheckIn",
                    new { id },
                    connectionStringName,
                    true);
    }

    public RoomTypeModel? GetRoomTypeById(int id)
    {
        return db.LoadData<RoomTypeModel, dynamic>("spGetRoomType_ById",
                                              new { id },
                                              connectionStringName,
                                              true).FirstOrDefault();

        


    }
}
