CREATE PROCEDURE [dbo].[spGenerateReport]
	@UserId uniqueidentifier,
	@StartDate date,
	@EndDate date
AS
BEGIN
	SELECT
		d.Id,
		u.Id as 'UserId',
		d.Date,
		(SELECT COUNT(id) FROM tblDayActivity WHERE UserId = u.Id AND DayId = d.Id) as 'ActivityCount',
		u.CalorieGoal,
		ISNULL(SUM(dbo.fnCalcCalorieIntake(di.Id)), 0) as 'CaloriesConsumed',
		ISNULL(SUM(dbo.fnCalcBurntCal(da.Id)), 0) as 'CaloriesBurned',
		u.ProteinGoal,
		ISNULL(SUM(dbo.fnCalcProteinIntake(di.Id)), 0) as 'ProteinConsumed',
		dbo.fnSucceeded
		(
			u.CalorieGoal,
			ISNULL(SUM(dbo.fnCalcCalorieIntake(di.Id)), 0),
			ISNULL(SUM(dbo.fnCalcBurntCal(da.Id)), 0),
			u.ProteinGoal,
			ISNULL(SUM(dbo.fnCalcProteinIntake(di.Id)), 0)
		) as 'Succeeded'
	FROM tblDay d
	LEFT JOIN tblDayActivity da on da.DayId = d.Id
	LEFT JOIN tblDayItem di on di.DayId = d.Id
	JOIN tblUser u on u.Id = d.UserId
	WHERE
		d.Date BETWEEN @StartDate AND @EndDate AND
		d.UserId = @UserId
	GROUP BY
		d.Id,
		u.Id,
		d.Date,
		UserId,
		u.CalorieGoal,
		u.ProteinGoal
	ORDER BY d.Date
END
