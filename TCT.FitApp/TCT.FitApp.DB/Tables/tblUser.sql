CREATE TABLE [dbo].[tblUser]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Username] VARCHAR(50) NOT NULL UNIQUE,
    [Password] NVARCHAR(64) NOT NULL,
    [UniqueKey] UNIQUEIDENTIFIER NOT NULL,
    [CalorieGoal] INT NOT NULL, 
    [ProteinGoal] INT NOT NULL, 
    [DaysInARowSucceeded] INT NOT NULL, 
    [HeightInches] INT NOT NULL, 
    [WeightPounds] INT NOT NULL,
    [UserAccessLevelId] UNIQUEIDENTIFIER NOT NULL
)
