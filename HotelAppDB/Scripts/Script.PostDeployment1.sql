/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

    
    if not exists(select 1 from dbo.RoomTypes)
    begin 

    insert into RoomTypes(Title,Desciption,Price)
    values('Single Bed','A room with king sized single bed with Bathroom,windows,Mirror desk And Television.',500),
    ('Double Bed','A room with king sized Double bed with Bathroom,windows,Mirror desk And Television.',800);

    end
    
    if not exists(select 1 from dbo.Rooms)
    begin 

    declare @roomId1 int;
    declare @roomId2 int;

    select @roomId1 =  id from dbo.RoomTypes where Title = 'Single Bed';
    select @roomId2 = id from dbo.RoomTypes where Title = 'Double Bed';

    insert into Rooms(RoomNumber,RoomTypeId)
    values('A1',@roomId2),
    ('A2',@roomId2),
    ('A3',@roomId1),
    ('A4',@roomId1),
    ('A5',@roomId1),
    ('A6',@roomId1),
    ('A7',@roomId1),
    ('A8',@roomId1),
    ('A9',@roomId1)

    end