CREATE PROCEDURE [dbo].[spRoomTypes_GetAvailableRoomTypes]
	@startDate date ,
	@endDate date
AS
begin
 set nocount on;

	
select rt.Id, rt.title,rt.Description,rt.price 
from RoomTypes rt
join Rooms r
on rt.id=r.RoomTypeId
where r.Id not in(
select RoomId from Bookings
   where   (@startDate < StartDate and @endDate >EndDate)
or(@startDate >= StartDate and @startDate <EndDate)
or(@endDate >= StartDate and @endDate <EndDate)
)
group by rt.Title,rt.id,rt.Description,rt.Price


end