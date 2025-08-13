using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace HotelAppLibrary.Data;

public class SqlData
{
    private IDataAccess db;
    private const string connectionStringName = "SqlDb";


    public SqlData(IDataAccess db)
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
        GuestModel guest = db.LoadData<GuestModel, dynamic>("spGuests_Insert",
                                          new { firstName, lastName },
                                          connectionStringName,
                                          true).First();

        int price = db.LoadData<int, dynamic>("select price from roomtypes where id = @roomTypeId",
                                          new { roomTypeId },
                                          connectionStringName).First();

        var stayingTime = endDate.Date.Subtract(startDate.Date);
        decimal totalCost = stayingTime.Days * price;
        

        RoomModel room = db.LoadData<RoomModel, dynamic>(
            "sp_GetAvailableRooms",
            new { startDate, endDate, roomTypeId },
            connectionStringName,
            true).First();

        db.SaveData(
            "spBookings_Insert",
            new { roomId = room.Id, guestId = guest.Id, startDate, endDate,totalCost },
            connectionStringName,
            true);


    }

    private void InsertIntoBookings(int guestId, DateTime startDate, DateTime endDate, int roomId, decimal totalCost)
    {
        db.SaveData<dynamic>("dbo.spInsertIntoBookings",
                    new { roomId, guestId, startDate, endDate, totalCost },
                    connectionStringName,
                    true);
    }

    private int GetRoomNumber(string roomTitle)
    {
        return db.LoadData<int, dynamic>("dbo.spGetAvailableRoomsForRoomTypes",
                                     new { roomTitle },
                                     connectionStringName,
                                     true).First();
    }
}
