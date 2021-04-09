BEGIN

	DECLARE @UserAccessLevelId uniqueidentifier;
	SELECT @UserAccessLevelId = Id from tblUserAccessLevel where Name = 'Admin'

	INSERT INTO dbo.tblUser(Id, Name, Username, Password, CalorieGoal, ProteinGoal, DaysInARowSucceeded, HeightInches, WeightPounds,UserAccessLevelId)
	VALUES
	(NEWID(), 'Jason Ryan', 'jryan', 'password1', 2000, 72, 12, 72, 175, @UserAccessLevelId)

	
	SELECT @UserAccessLevelId = Id from tblUserAccessLevel where Name = 'Super User'
	INSERT INTO dbo.tblUser(Id, Name, Username, Password, CalorieGoal, ProteinGoal, DaysInARowSucceeded, HeightInches, WeightPounds,UserAccessLevelId)
	VALUES
	(NEWID(), 'Dan Butzen', 'dbutzen', 'password2', 1800, 70, 16, 73, 179, @UserAccessLevelId)

	SELECT @UserAccessLevelId = Id from tblUserAccessLevel where Name = 'User'
	INSERT INTO dbo.tblUser(Id, Name, Username, Password, CalorieGoal, ProteinGoal, DaysInARowSucceeded, HeightInches, WeightPounds, UserAccessLevelId)
	VALUES
	(NEWID(), 'Chris Van Hefty', 'cvanhefty', 'password3', 2200, 81, 11, 77, 209, @UserAccessLevelId)

END