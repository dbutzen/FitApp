BEGIN
	DECLARE @DayId uniqueidentifier;
	DECLARE @ItemId uniqueidentifier;

	SELECT @DayId = Id FROM tblDay WHERE Date = '2021-03-01'
	SELECT @ItemId = Id FROM tblItem WHERE Name = 'Strawberry'

	INSERT INTO dbo.tblDayItem(Id, DayId, Itemid, Servings)
	VALUES
	(NEWID(), @DayId, @ItemId, 3)

	SELECT @DayId = Id FROM tblDay WHERE Date = '2021-02-21'
	SELECT @ItemId = Id FROM tblItem WHERE Name = 'Milk'

	INSERT INTO dbo.tblDayItem(Id, DayId, Itemid, Servings)
	VALUES
	(NEWID(), @DayId, @ItemId, 2)

	SELECT @DayId = Id FROM tblDay WHERE Date = '2021-03-11'
	SELECT @ItemId = Id FROM tblItem WHERE Name = 'Cauliflower'

	INSERT INTO dbo.tblDayItem(Id, DayId, Itemid, Servings)
	VALUES
	(NEWID(), @DayId, @ItemId, 1)
END