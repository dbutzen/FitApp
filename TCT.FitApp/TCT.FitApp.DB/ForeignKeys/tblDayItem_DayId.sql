﻿ALTER TABLE [dbo].[tblDayItem]
	ADD CONSTRAINT [tblDayItem_DayId]
	FOREIGN KEY (DayId)
	REFERENCES [tblDay] (Id)
