CREATE TABLE [dbo].[RoomTypes]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [Title] NVARCHAR(50) NOT NULL, 
    [Desciption] NVARCHAR(2000) NOT NULL, 
    [Price] MONEY NOT NULL
)
