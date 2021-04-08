ALTER TABLE [dbo].[tblUserItem]
	ADD CONSTRAINT [tblUserItem_ItemId]
	FOREIGN KEY (ItemId)
	REFERENCES [tblItem] (Id)
