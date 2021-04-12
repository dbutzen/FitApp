CREATE FUNCTION [dbo].[fnCalcCalorieIntake] (@DayItemId uniqueidentifier) RETURNS INT
AS
BEGIN
	DECLARE @ItemId uniqueidentifier;
	DECLARE @Servings int;

	SELECT
		@ItemId = ItemId,
		@Servings = Servings
	FROM tblDayItem
	WHERE
		Id = @DayItemId
	
	DECLARE @Calories int;
	SELECT @Calories = Calories FROM tblItem WHERE Id = @ItemId

	RETURN @Calories * @Servings
END