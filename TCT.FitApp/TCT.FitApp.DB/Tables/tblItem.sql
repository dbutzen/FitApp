CREATE TABLE [dbo].[tblItem]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Type] VARCHAR(50) NOT NULL, 
    [Calories] INT NOT NULL, 
    [Protein] INT NOT NULL
)
