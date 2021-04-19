ALTER TABLE [dbo].[tblUser]
	ADD CONSTRAINT [tblUser_UserAccessLevelId]
	FOREIGN KEY (UserAccessLevelId)
	REFERENCES [tblUserAccessLevel] (Id) ON DELETE CASCADE
