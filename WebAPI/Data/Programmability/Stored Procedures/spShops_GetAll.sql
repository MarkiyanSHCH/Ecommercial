USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spShops_GetAll'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.spShops_GetAll;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--============================================================================

CREATE PROCEDURE [dbo].[spShops_GetAll]
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT 
		[Id],
		[City],
		[Address],
		[ZipCode],
		[Phone],
		[Email]
	FROM Shops
END
GO

EXEC spShops_GetAll