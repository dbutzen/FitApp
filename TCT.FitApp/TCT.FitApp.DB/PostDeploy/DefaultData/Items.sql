BEGIN
	
	DECLARE @UserId uniqueidentifier;
	SELECT @UserId = Id from tblUser where Username = 'jryan'

	DECLARE @TypeId uniqueidentifier;
	SELECT @TypeId = Id from tblItemType where Name = 'Food'

	INSERT INTO dbo.tblItem(Id, Name, TypeId, Calories, Protein, CreatedByUserId)
	VALUES
	(NEWID(), 'Strawberry', @TypeId, 4, 1, @UserId)



	SELECT @UserId = Id from tblUser where Username = 'dbutzen'

	INSERT INTO dbo.tblItem(Id, Name, TypeId, Calories, Protein, CreatedByUserId)
	VALUES
	(NEWID(), 'Cauliflower', @TypeId, 3, 2, @UserId),
	(NEWID(), 'PROTEIN MAX', @TypeId, 500, 80, @UserId)
	
	


	SELECT @UserId = Id from tblUser where Username = 'cvanhefty'
	SELECT @TypeId = Id from tblItemType where Name = 'Drink'

	INSERT INTO dbo.tblItem(Id, Name, TypeId, Calories, Protein, CreatedByUserId)
	VALUES
	(NEWID(), 'Milk', @TypeId, 10, 6, @UserId)


	-- Food
	SELECT @TypeId = Id from tblItemType where Name = 'Food'
	INSERT INTO dbo.tblItem(Id, Name, TypeId, Calories, Protein)
	VALUES
	(NEWID(), 'Egg', @TypeId, 75, 7),
	(NEWID(), 'Almond', @TypeId, 164, 6),
	(NEWID(), 'Oat', @TypeId, 303, 13),
	(NEWID(), 'Broccoli', @TypeId, 31, 3),
	(NEWID(), 'Shrimp', @TypeId, 76, 15),
	(NEWID(), 'Peanut', @TypeId, 161, 2)



	-- Drinks
	SELECT @TypeId = Id from tblItemType where Name = 'Drink'
	INSERT INTO dbo.tblItem(Id, Name, TypeId, Calories, Protein)
	VALUES
	(NEWID(), 'Coconut Water', @TypeId, 3, 2),
	(NEWID(), 'Cranberry Juice', @TypeId, 117, 1)



END