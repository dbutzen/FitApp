CREATE TABLE [dbo].[tblDay]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [UserId] UNIQUEIDENTIFIER NOT NULL, 
    [Date] DATE NOT NULL, 
    [CaloriesEaten] INT NOT NULL, 
    [Protein] INT NOT NULL, 
    [Succeeded] BIT NOT NULL, 
    [CaloriesBurned] INT NOT NULL
)
