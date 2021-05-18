BEGIN
	DECLARE @UsersId uniqueidentifier;

	SELECT @UsersId = Id from tblUser where Username = 'cvanhefty'

	INSERT INTO dbo.tblDay(Id, UserId, Date, Succeeded)
	VALUES
	(NEWID(), @UsersId, '2021-03-01', 0),
	(NEWID(), @UsersId, '2021-5-17', 0),
	(NEWID(), @UsersId, '2021-5-18', 0),
	(NEWID(), @UsersId, '2021-5-19', 0),
	(NEWID(), @UsersId, '2021-5-20', 0)

	SELECT @UsersId = Id from tblUser where Username = 'dbutzen'
	
	INSERT INTO dbo.tblDay(Id, UserId, Date, Succeeded)
	VALUES
	(NEWID(), @UsersId, '2021-02-21', 1),
	(NEWID(), @UsersId, '2021-02-22', 1),
	(NEWID(), @UsersId, '2021-02-23', 0),
	(NEWID(), @UsersId, '2021-5-17', 0),
	(NEWID(), @UsersId, '2021-5-18', 0),
	(NEWID(), @UsersId, '2021-5-19', 0),
	(NEWID(), @UsersId, '2021-5-20', 0)



	-- Jason's Data
	SELECT @UsersId = Id from tblUser where Username = 'jryan'
	
	INSERT INTO dbo.tblDay(Id, UserId, Date, Succeeded)
	VALUES
	(NEWID(), @UsersId, '2021-03-11', 1)

	INSERT INTO dbo.tblDay(Id, UserId, Date, Succeeded)
	VALUES
	(NEWID(), @UsersId, '2021-03-12', 0)

	INSERT INTO dbo.tblDay(Id, UserId, Date, Succeeded)
	VALUES
	(NEWID(), @UsersId, '2021-5-17', 0)

		INSERT INTO dbo.tblDay(Id, UserId, Date, Succeeded)
	VALUES
	(NEWID(), @UsersId, '2021-5-18', 0)

		INSERT INTO dbo.tblDay(Id, UserId, Date, Succeeded)
	VALUES
	(NEWID(), @UsersId, '2021-5-19', 0)

		INSERT INTO dbo.tblDay(Id, UserId, Date, Succeeded)
	VALUES
	(NEWID(), @UsersId, '2021-5-20', 0)

	-- End
END