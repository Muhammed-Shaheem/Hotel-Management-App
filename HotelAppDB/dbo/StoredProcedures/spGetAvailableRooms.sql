CREATE PROCEDURE [dbo].[spGetAvailableRooms]
	@startDate date ,
	@endDate date,
	@roomTypeId int
AS
begin

		set nocount on


		select top 1 r.*  from RoomTypes rt
		join Rooms r
		on rt.id=r.RoomTypeId
		where  rt.Id = @roomTypeId and r.Id not in(
		select RoomId from Bookings
		   where   (@startDate < StartDate and @endDate >EndDate)
		or(@startDate >= StartDate and @startDate <EndDate)
		or(@endDate >= StartDate and @endDate <EndDate)
		)

end