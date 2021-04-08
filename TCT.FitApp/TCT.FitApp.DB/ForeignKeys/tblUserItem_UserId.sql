ALTER TABLE [dbo].[tblUserItem]
	ADD CONSTRAINT [tblUserItem_UserId]
	FOREIGN KEY (UserId)
	REFERENCES [tblUser] (Id)
