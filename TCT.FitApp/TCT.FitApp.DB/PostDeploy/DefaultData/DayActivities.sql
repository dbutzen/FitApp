BEGIN

	DECLARE @DateId uniqueidentifier;
	DECLARE @ActivityId uniqueidentifier;
	SELECT @DateId = Id from tblDay where  Date = '2021-03-01'
	SELECT @ActivityId = Id from tblActivity where  Name = 'Swimming'

	INSERT INTO dbo.tblDayActivity(Id, DayId, ActivityId, Duration, DifficultyLevel)
	VALUES
	(NEWID(), @DateId, @ActivityId, 30, 2)

	
	SELECT @DateId = Id from tblDay where  Date = '2021-02-21'
	SELECT @ActivityId = Id from tblActivity where  Name = 'Running'
	
	INSERT INTO dbo.tblDayActivity(Id, DayId, ActivityId, Duration, DifficultyLevel)
	VALUES
	(NEWID(), @DateId, @ActivityId, 60, 1)


	-- Jason's activities
	SELECT @DateId = Id from tblDay where  Date = '2021-03-11'
	SELECT @ActivityId = Id from tblActivity where  Name = 'Walking'
	
	INSERT INTO dbo.tblDayActivity(Id, DayId, ActivityId, Duration, DifficultyLevel)
	VALUES
	(NEWID(), @DateId, @ActivityId, 90, 3)

	SELECT @DateId = Id from tblDay where  Date = '2021-03-11'
	SELECT @ActivityId = Id from tblActivity where  Name = 'Running'
	
	INSERT INTO dbo.tblDayActivity(Id, DayId, ActivityId, Duration, DifficultyLevel)
	VALUES
	(NEWID(), @DateId, @ActivityId, 60, 1)

	SELECT @DateId = Id from tblDay where  Date = '2021-03-12'
	SELECT @ActivityId = Id from tblActivity where  Name = 'Swimming'
	
	INSERT INTO dbo.tblDayActivity(Id, DayId, ActivityId, Duration, DifficultyLevel)
	VALUES
	(NEWID(), @DateId, @ActivityId, 30, 2)


	SELECT @DateId = Id from tblDay where  Date = '2021-05-16'
	SELECT @ActivityId = Id from tblActivity where  Name = 'Cycling'
	
	INSERT INTO dbo.tblDayActivity(Id, DayId, ActivityId, Duration, DifficultyLevel)
	VALUES
	(NEWID(), @DateId, @ActivityId, 45, 1)

	SELECT @ActivityId = Id from tblActivity where  Name = 'Walking'
	
	INSERT INTO dbo.tblDayActivity(Id, DayId, ActivityId, Duration, DifficultyLevel)
	VALUES
	(NEWID(), @DateId, @ActivityId, 30, 1)

	-- end of Jason's
END