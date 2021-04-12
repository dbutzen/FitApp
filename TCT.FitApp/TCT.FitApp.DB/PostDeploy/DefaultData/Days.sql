BEGIN
	DECLARE @UsersId uniqueidentifier;

	SELECT @UsersId = Id from tblUser where Username = 'cvanhefty'

	INSERT INTO dbo.tblDay(Id, UserId, Date, Succeeded)
	VALUES
	(NEWID(), @UsersId, '2021-03-01', 0)

	SELECT @UsersId = Id from tblUser where Username = 'dbutzen'
	
	INSERT INTO dbo.tblDay(Id, UserId, Date, Succeeded)
	VALUES
	(NEWID(), @UsersId, '2021-02-21', 1)

	SELECT @UsersId = Id from tblUser where Username = 'jryan'
	
	INSERT INTO dbo.tblDay(Id, UserId, Date, Succeeded)
	VALUES
	(NEWID(), @UsersId, '2021-03-11', 1)

		INSERT INTO dbo.tblDay(Id, UserId, Date, Succeeded)
	VALUES
	(NEWID(), @UsersId, '2021-03-12', 0)

END