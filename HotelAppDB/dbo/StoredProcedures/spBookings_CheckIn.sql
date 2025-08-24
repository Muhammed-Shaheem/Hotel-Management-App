CREATE PROCEDURE [dbo].[spBookings_CheckIn]
	@id int
	
AS
begin
		set nocount on
		update Bookings
		set CheckedIn = 1
		where  id = @id	

end