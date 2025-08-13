CREATE PROCEDURE [dbo].[spBookings_Search]
	@lastName nvarchar(50)

AS
begin
	set nocount on

	select 
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
	and StartDate = CAST(GETDATE() as date)

end