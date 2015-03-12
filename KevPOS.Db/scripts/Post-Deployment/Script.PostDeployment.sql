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

declare @param bigint
set @param = (select * from control.IdBlock)
IF @param IS NULL
	INSERT INTO control.IdBlock (CurrentBlock)
	VALUES (0)
ELSE
	print 'not empty dont insert a 0'
