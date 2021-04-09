/*
 Pre-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be executed before the build script.	
 Use SQLCMD syntax to include a file in the pre-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the pre-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DROP TABLE IF EXISTS dbo.DayActivies
DROP TABLE IF EXISTS dbo.DayItems
DROP TABLE IF EXISTS dbo.Days
DROP TABLE IF EXISTS dbo.Users
DROP TABLE IF EXISTS dbo.Activities
DROP TABLE IF EXISTS dbo.UserAccessLevels
DROP TABLE IF EXISTS dbo.Items
DROP TABLE IF EXISTS dbo.tblItemType