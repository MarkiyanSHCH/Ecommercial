USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spShops_GetById'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.spShops_GetById;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--============================================================================

CREATE PROCEDURE [dbo].[spShops_GetById]
	@ShopId int
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
	WHERE Shops.Id = @ShopId
END
GO