BEGIN
	INSERT INTO dbo.tblUserAccessLevel(Id, Name, Description)
	VALUES
	(NEWID(), 'Admin', 'Full'),
	(NEWID(), 'Super User', 'Modify'),
	(NEWID(), 'User', 'Read Only')
END