BEGIN
	INSERT INTO dbo.tblActivity(Id, Name, EasyCaloriesPerHour, MediumCaloriesPerHour, HardCaloriesPerHour)
	VALUES
	(NEWID(), 'Swimming', 327, 572, 899),
	(NEWID(), 'Running', 817, 1103, 1471),
	(NEWID(), 'Walking', 163, 311, 654)
END