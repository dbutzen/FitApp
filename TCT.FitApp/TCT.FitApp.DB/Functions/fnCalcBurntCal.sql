CREATE FUNCTION [dbo].[fnCalcBurntCal] (@DayActivityId uniqueidentifier) RETURNS FLOAT
AS
BEGIN
	DECLARE @ActivityId uniqueidentifier;
	DECLARE @Duration int; -- minutes
	DECLARE @DifficultyLevel int; -- 1 - Easy, 2 - Medium, 3 - Hard

	SELECT
		@ActivityId = ActivityId,
		@Duration = Duration,
		@DifficultyLevel = DifficultyLevel
	FROM tblDayActivity
	WHERE
		Id = @DayActivityId

	DECLARE @CaloriesPerHour int;
	IF @DifficultyLevel = 1
		SELECT @CaloriesPerHour = EasyCaloriesPerHour FROM tblActivity WHERE Id = @ActivityId
	ELSE IF @DifficultyLevel = 2
		SELECT @CaloriesPerHour = MediumCaloriesPerHour FROM tblActivity WHERE Id = @ActivityId
	ELSE IF @DifficultyLevel = 3
		SELECT @CaloriesPerHour = MediumCaloriesPerHour FROM tblActivity WHERE Id = @ActivityId
	ELSE
		SET @CaloriesPerHour = 0;


	RETURN (CAST(@Duration as float) / 60) * @CaloriesPerHour
END