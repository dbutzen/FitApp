ALTER TABLE [dbo].[tblDayActivity]
	ADD CONSTRAINT [tblDayActivity_ActivityId]
	FOREIGN KEY (ActivityId)
	REFERENCES [tblActivity] (Id)
