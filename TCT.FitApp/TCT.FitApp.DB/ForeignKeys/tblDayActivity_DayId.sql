ALTER TABLE [dbo].[tblDayActivity]
	ADD CONSTRAINT [tblDayActivity_DayId]
	FOREIGN KEY (DayId)
	REFERENCES [tblDay] (Id)
