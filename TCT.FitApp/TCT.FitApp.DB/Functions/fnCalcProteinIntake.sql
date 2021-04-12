CREATE FUNCTION [dbo].[fnCalcProteinIntake] (@DayItemId uniqueidentifier) RETURNS INT
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
	
	DECLARE @Protein int;
	SELECT @Protein = Protein FROM tblItem WHERE Id = @ItemId

	RETURN @Protein * @Servings
END