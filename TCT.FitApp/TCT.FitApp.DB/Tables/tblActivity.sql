CREATE TABLE [dbo].[tblActivity]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [CaloriesPerHour] INT NOT NULL
)
