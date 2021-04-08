CREATE TABLE [dbo].[tblUser]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Username] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [CalorieGoal] INT NOT NULL, 
    [ProteinGoal] INT NOT NULL, 
    [DaysInARowSucceeded] INT NOT NULL, 
    [HeightInches] INT NOT NULL, 
    [WeightPounds] INT NOT NULL
)
