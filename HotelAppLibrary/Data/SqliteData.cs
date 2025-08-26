using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;

namespace HotelAppLibrary.Data;

public class SqliteData : IDatabaseData
{
    private readonly ISqliteDataAccess db;
    private string connectionStringName = "SqliteDb";

    public SqliteData(ISqliteDataAccess db)
    {
        this.db = db;
    }
    public void BookGuest(string firstName, string lastName, DateTime startDate, DateTime endDate, int roomTypeId)
    {
        throw new NotImplementedException();
    }

    public void CheckInGuest(int id)
    {
        throw new NotImplementedException();
    }

    public List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate)
    {
        string sql = @"	
                    select rt.Id, rt.title,rt.Description,rt.price from RoomTypes rt
                    join Rooms r
                    on rt.id=r.RoomTypeId
                    where r.Id not in(
                    select RoomId from Bookings
                        where   (@startDate < StartDate and @endDate >EndDate)
                    or(@startDate >= StartDate and @startDate <EndDate)
                    or(@endDate >= StartDate and @endDate <EndDate)
                    )
                    group by rt.Title,rt.id,rt.Description,rt.Price";

        var output = db.LoadData<RoomTypeModel, dynamic>(sql, new { startDate, endDate }, connectionStringName);
        output.ForEach(x => x.Price = x.Price / 100);
        return output;
    }

    public RoomTypeModel? GetRoomTypeById(int id)
    {
        throw new NotImplementedException();
    }

    public List<FullBookingModel> SearchBookings(string lastName)
    {
        throw new NotImplementedException();
    }
}
