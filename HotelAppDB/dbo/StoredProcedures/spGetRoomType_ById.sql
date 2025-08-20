CREATE PROCEDURE [dbo].[spGetRoomType_ById]
	@id int = 0
	
AS
begin
set nocount on

select [Id], [Title], [Description], [Price]
from RoomTypes
where Id = @id


end
