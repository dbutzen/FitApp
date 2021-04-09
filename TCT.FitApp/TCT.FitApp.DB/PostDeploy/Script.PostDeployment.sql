/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\DefaultData\ItemTypes.sql
:r .\DefaultData\Items.sql
:r .\DefaultData\UserAccessLevels.sql
:r .\DefaultData\Activities.sql
:r .\DefaultData\Users.sql
:r .\DefaultData\Days.sql
:r .\DefaultData\DayItems.sql
:r .\DefaultData\DayActivities.sql


