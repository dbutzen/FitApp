﻿CREATE TABLE [dbo].[tblDayItem]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[DayId] UNIQUEIDENTIFIER NOT NULL,
	[ItemId] UNIQUEIDENTIFIER NOT NULL,
	[Servings] INT NOT NULL
)
