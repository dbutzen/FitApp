BEGIN

	-- Jason
	DECLARE @UserAccessLevelId uniqueidentifier;
	SELECT @UserAccessLevelId = Id from tblUserAccessLevel where Name = 'Admin'

	DECLARE @UniqueKey UNIQUEIDENTIFIER = NEWID();
	DECLARE @Password NVARCHAR(50) = 'password1';
	DECLARE @HashedPassword NVARCHAR(64) = CONVERT(NVARCHAR(MAX), HASHBYTES('SHA2_256', @Password+UPPER(CAST(@UniqueKey AS NVARCHAR(36)))), 2);

	INSERT INTO dbo.tblUser(Id, Name, Username, Password, UniqueKey, CalorieGoal, ProteinGoal, DaysInARowSucceeded, HeightInches, WeightPounds,UserAccessLevelId, Sex)
	VALUES
	(NEWID(), 'Jason Ryan', 'jryan', @HashedPassword, @UniqueKey, 2000, 72, 12, 72, 175, @UserAccessLevelId, 'Male')

	
	-- Dan
	SELECT @UserAccessLevelId = Id from tblUserAccessLevel where Name = 'Super User'

	SET @UniqueKey = NEWID();
	SET @Password  = 'password2';
	SET @HashedPassword = CONVERT(NVARCHAR(64), HASHBYTES('SHA2_256', @Password+UPPER(CAST(@UniqueKey AS NVARCHAR(36)))), 2);

	INSERT INTO dbo.tblUser(Id, Name, Username, Password, UniqueKey, CalorieGoal, ProteinGoal, DaysInARowSucceeded, HeightInches, WeightPounds,UserAccessLevelId, Sex)
	VALUES
	(NEWID(), 'Dan Butzen', 'dbutzen', @HashedPassword, @UniqueKey, 1800, 70, 16, 73, 179, @UserAccessLevelId, 'Male')


	-- Chris
	SELECT @UserAccessLevelId = Id from tblUserAccessLevel where Name = 'User'

	SET @UniqueKey = NEWID();
	SET @Password  = 'password3';
	SET @HashedPassword = CONVERT(NVARCHAR(64), HASHBYTES('SHA2_256', @Password+UPPER(CAST(@UniqueKey AS NVARCHAR(36)))), 2);

	INSERT INTO dbo.tblUser(Id, Name, Username, Password, UniqueKey, CalorieGoal, ProteinGoal, DaysInARowSucceeded, HeightInches, WeightPounds, UserAccessLevelId, Sex)
	VALUES
	(NEWID(), 'Chris Van Hefty', 'cvanhefty', @HashedPassword, @UniqueKey, 2200, 81, 11, 77, 209, @UserAccessLevelId, 'Male')

END