ALTER TABLE [dbo].[tblDayItem]
	ADD CONSTRAINT [tblDayItem_ItemId]
	FOREIGN KEY (ItemId)
	REFERENCES [tblItem] (Id)
