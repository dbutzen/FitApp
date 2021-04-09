BEGIN
	
	DECLARE @UserId uniqueidentifier;
	SELECT @UserId = Id from tblUser where Username = 'jryan'

	DECLARE @TypeId uniqueidentifier;
	SELECT @TypeId = Id from tblItemType where Name = 'Food'

	INSERT INTO dbo.tblItem(Id, Name, TypeId, Calories, Protein, UserId)
	VALUES
	(NEWID(), 'Strawberry', @TypeId, 4, 1, @UserId)

	SELECT @UserId = Id from tblUser where Username = 'dbutzen'

	INSERT INTO dbo.tblItem(Id, Name, TypeId, Calories, Protein, UserId)
	VALUES
	(NEWID(), 'Cauliflower', @TypeId, 3, 2, @UserId)
	
	SELECT @UserId = Id from tblUser where Username = 'cvanhefty'
	SELECT @TypeId = Id from tblItemType where Name = 'Drink'

	INSERT INTO dbo.tblItem(Id, Name, TypeId, Calories, Protein, UserId)
	VALUES
	(NEWID(), 'Milk', @TypeId, 10, 6, @UserId)

END