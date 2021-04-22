BEGIN
	DECLARE @DayId uniqueidentifier;
	DECLARE @ItemId uniqueidentifier;

	SELECT @DayId = Id FROM tblDay WHERE Date = '2021-03-01'
	SELECT @ItemId = Id FROM tblItem WHERE Name = 'Strawberry'

	INSERT INTO dbo.tblDayItem(Id, DayId, Itemid, Servings)
	VALUES
	(NEWID(), @DayId, @ItemId, 3)

	-- Succeeded
	SELECT @UsersId = Id from tblUser where Username = 'dbutzen'
	SELECT @DayId = Id FROM tblDay WHERE Date = '2021-02-21' AND UserId = @UserId
	SELECT @ItemId = Id FROM tblItem WHERE Name = 'PROTEIN MAX'

	INSERT INTO dbo.tblDayItem(Id, DayId, Itemid, Servings)
	VALUES
	(NEWID(), @DayId, @ItemId, 2)

	SELECT @ItemId = Id FROM tblItem WHERE Name = 'Strawberry'
	INSERT INTO dbo.tblDayItem(Id, DayId, Itemid, Servings)
	VALUES
	(NEWID(), @DayId, @ItemId, 1)

	-- Succeeded
	SELECT @DayId = Id FROM tblDay WHERE Date = '2021-02-22' AND UserId = @UserId
	INSERT INTO dbo.tblDayItem(Id, DayId, Itemid, Servings)
	VALUES
	(NEWID(), @DayId, @ItemId, 1)

	SELECT @ItemId = Id FROM tblItem WHERE Name = 'PROTEIN MAX'
	INSERT INTO dbo.tblDayItem(Id, DayId, Itemid, Servings)
	VALUES
	(NEWID(), @DayId, @ItemId, 1)

	-- Failed
	SELECT @DayId = Id FROM tblDay WHERE Date = '2021-02-23' AND UserId = @UserId
	SELECT @ItemId = Id FROM tblItem WHERE Name = 'Strawberry'
	INSERT INTO dbo.tblDayItem(Id, DayId, Itemid, Servings)
	VALUES
	(NEWID(), @DayId, @ItemId, 1)



	SELECT @DayId = Id FROM tblDay WHERE Date = '2021-03-11'
	SELECT @ItemId = Id FROM tblItem WHERE Name = 'Cauliflower'

	INSERT INTO dbo.tblDayItem(Id, DayId, Itemid, Servings)
	VALUES
	(NEWID(), @DayId, @ItemId, 1)
END