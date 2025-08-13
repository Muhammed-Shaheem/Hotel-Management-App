CREATE PROCEDURE [dbo].[spChecked_In]
	@id int
	
AS
begin
		set nocount on
		update Bookings
		set CheckedIn = 1
		where  id = @id	

end