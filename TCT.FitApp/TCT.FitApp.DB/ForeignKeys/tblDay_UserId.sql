﻿ALTER TABLE [dbo].[tblDay]
	ADD CONSTRAINT [tblDay_UserId]
	FOREIGN KEY (UserId)
	REFERENCES [tblUser] (Id)
