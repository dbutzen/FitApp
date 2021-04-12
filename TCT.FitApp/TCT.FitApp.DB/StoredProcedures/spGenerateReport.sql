﻿CREATE PROCEDURE [dbo].[spGenerateReport]
	@UserId uniqueidentifier,
	@StartDate date,
	@EndDate date
AS
BEGIN
	SELECT
		u.Id,
		d.Date,
		u.CalorieGoal,
		ISNULL(SUM(dbo.fnCalcCalorieIntake(di.Id)), 0) as 'CaloriesConsumed',
		ISNULL(SUM(dbo.fnCalcBurntCal(da.Id)), 0) as 'CaloriesBurned',
		--(ISNULL(SUM(dbo.fnCalcCalorieIntake(di.Id)), 0) - ISNULL(SUM(dbo.fnCalcBurntCal(da.Id)), 0)) 'CaloriesToBurn',
		u.ProteinGoal,
		ISNULL(SUM(dbo.fnCalcProteinIntake(di.Id)), 0) as 'ProteinConsumed'
		--IIF(
		--	(ISNULL(SUM(dbo.fnCalcCalorieIntake(di.Id)), 0) - ISNULL(SUM(dbo.fnCalcBurntCal(da.Id)), 0)) <= u.CalorieGoal AND
		--	ISNULL(SUM(dbo.fnCalcProteinIntake(di.Id)), 0) >= u.ProteinGoal, 'Yes', 'No') as 'Succeded'
	FROM tblDay d
	LEFT JOIN tblDayActivity da on da.DayId = d.Id
	LEFT JOIN tblDayItem di on di.DayId = d.Id
	JOIN tblUser u on u.Id = d.UserId
	WHERE
		d.Date BETWEEN @StartDate AND @EndDate AND
		d.UserId = @UserId
	GROUP BY
		u.Id,
		d.Date,
		u.CalorieGoal,
		u.ProteinGoal
END
