BEGIN
	INSERT INTO dbo.tblItemType(Id, Name)
	VALUES
	(NEWID(), 'Food'),
	(NEWID(), 'Drink')
END