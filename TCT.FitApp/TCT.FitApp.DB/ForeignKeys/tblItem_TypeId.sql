ALTER TABLE [dbo].[tblItem]
	ADD CONSTRAINT [tblItem_TypeId]
	FOREIGN KEY (TypeId)
	REFERENCES [tblItemType] (Id)
