CREATE TABLE [dbo].[Guests]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL
)
