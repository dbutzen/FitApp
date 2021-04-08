ALTER TABLE [dbo].tblItem
	ADD CONSTRAINT [tblItem_UserId]
	FOREIGN KEY (UserId)
	REFERENCES [tblUser] (Id)
