USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spUsers_Add'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.spUsers_Add;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--============================================================================

CREATE PROCEDURE [dbo].[spUsers_Add]
	@Name VARCHAR(40),
	@Email VARCHAR(50),
	@Password VARCHAR(200)
AS
BEGIN
		SET NOCOUNT OFF;
    	INSERT INTO Users 
		(
			[Name],
			[Email],
			[Password]
		)
		VALUES 
		(
			@Name,
			@Email,
			@Password
		);
		DECLARE @UserId INT = CONVERT(INT, SCOPE_IDENTITY());
		SELECT @UserId AS 'Id';
END
GO