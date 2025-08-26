using HotelAppLibrary.Databases;
using HotelAppLibrary.Models;
using System.Xml.Linq;

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


        string sql = "select 1 from Guests where FirstName=@firstName and LastName=@lastName";
        int result = db.LoadData<GuestModel, dynamic>(sql, new { firstName, lastName }, connectionStringName).Count;

        if (result == 0)
        {

            sql = @"INSERT INTO Guests (FirstName, LastName)
                    VALUES (@firstName, @lastName)";
            db.SaveData(sql, new { firstName, lastName }, connectionStringName);

        }


        sql = @" select  [Id], [FirstName], [LastName]
	                from Guests
	                 where FirstName=@firstName and LastName = @lastName LIMIT 1";
        GuestModel guest = db.LoadData<GuestModel, dynamic>(sql, new { firstName, lastName }, connectionStringName).First();

        int price = db.LoadData<int, dynamic>("select price from roomtypes where id = @roomTypeId",
                                          new { roomTypeId },
                                          connectionStringName).First();

        var stayingTime = endDate.Date.Subtract(startDate.Date);
        decimal totalCost = (stayingTime.Days * price);

        sql = @"
		        select  r.*  from RoomTypes rt
		        join Rooms r
		        on rt.id=r.RoomTypeId
		        where  rt.Id = @roomTypeId and r.Id not in(
		        select RoomId from Bookings
		           where   (@startDate < StartDate and @endDate >EndDate)
		        or(@startDate >= StartDate and @startDate <EndDate)
		        or(@endDate >= StartDate and @endDate <EndDate)
		        ) LIMIT 1";

        RoomModel room = db.LoadData<RoomModel, dynamic>(
                                    sql,
                                    new { startDate, endDate, roomTypeId },
                                    connectionStringName).First();

        sql = "insert into bookings(RoomId,GuestId,StartDate,EndDate,TotalCost)\r\nvalues(@roomId,@guestId,@startDate,@endDate,@totalCost)";

        db.SaveData(
            sql,
            new { roomId = room.Id, guestId = guest.Id, startDate, endDate, totalCost },
            connectionStringName);

    }

    public void CheckInGuest(int id)
    {
        string sql = @"	update Bookings
		set CheckedIn = 1
		where  id = @id	";
        db.SaveData(sql, new { id }, connectionStringName);
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
        string sql = "select [Id], [Title], [Description], [Price]\r\nfrom RoomTypes\r\nwhere Id = @id";
        return db.LoadData<RoomTypeModel, dynamic>(sql,
                                             new { id },
                                             connectionStringName).FirstOrDefault();
    }

    public List<FullBookingModel> SearchBookings(string lastName)
    {
        string sql = @"select 
	                 b.Id, [b].[RoomId], [b].[GuestId], [b].[StartDate], [b].[EndDate], [b].[CheckedIn], [b].[TotalCost],
	                 [g].[FirstName], [g].[LastName],
	                 [r].[RoomNumber], [r].[RoomTypeId],
	                 [rt].[Title], [rt].[Description], [rt].[Price] 
	                 from Guests g
	                join Bookings b
	                on b.GuestId = g.id
	                join Rooms r
	                on r.Id = b.RoomId
	                join RoomTypes rt
	                on rt.Id=r.RoomTypeId
	                where LastName = @lastName
	                and StartDate = @startDate
	                and CheckedIn = 0";
         List<FullBookingModel> output = db.LoadData<FullBookingModel, dynamic>(sql,
                    new { lastName, startDate = DateTime.Now.Date },
                    connectionStringName);
        output.ForEach(x =>
        {
            x.TotalCost = x.TotalCost / 100;
            x.Price = x.Price / 100;
        });
        return output;
        
    }
}
