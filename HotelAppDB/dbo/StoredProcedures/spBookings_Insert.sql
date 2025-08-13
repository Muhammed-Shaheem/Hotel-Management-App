CREATE PROCEDURE [dbo].[spInsertIntoBookings]
	@roomId int ,
	@guestId int,
	@startDate date,
	@endDate date,
	@totalCost money
AS
begin
set nocount on

insert into bookings(RoomId,GuestId,StartDate,EndDate,TotalCost)
values(@roomId,@guestId,@startDate,@endDate,@totalCost)


end


