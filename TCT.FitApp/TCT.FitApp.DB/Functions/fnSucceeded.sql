CREATE FUNCTION [dbo].[fnSucceeded] 
(
	@CalorieGoal int,
	@CaloriesConsumed int,
	@CalorieBurnt int,
	@ProteinGoal int,
	@ProteinConsumed int
)
RETURNS BIT
AS
BEGIN
	DECLARE @CalorieGoalSucceeded bit = 0;
	IF ((@CaloriesConsumed - @CalorieBurnt) <= @CalorieGoal)
		SET @CalorieGoalSucceeded = 1;

	DECLARE @ProteinGoalSucceeded bit = 0
	IF  @ProteinConsumed >= @ProteinGoal
		SET @ProteinGoalSucceeded = 1;

	IF (@CalorieGoalSucceeded = 1 AND @ProteinGoalSucceeded = 1)
		RETURN 1;

	RETURN 0;
END
