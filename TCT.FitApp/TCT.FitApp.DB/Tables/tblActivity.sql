CREATE TABLE [dbo].[tblActivity]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [EasyCaloriesPerHour] INT NOT NULL, 
    [MediumCaloriesPerHour] INT NOT NULL, 
    [HardCaloriesPerHour] INT NOT NULL
)
