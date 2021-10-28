USE [ProductDB];
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spProduct_GetCartItems'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.spProduct_GetCartItems;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE PROCEDURE [dbo].[spProduct_GetCartItems]
    @ItemId ProductCartSearch READONLY
AS
BEGIN
    SET NOCOUNT OFF

    --========================================================================
	-- Return:
	--========================================================================
    SELECT
        Products.[Id],
        Products.[Name], 
        Products.[Description], 
        Products.[Price], 
        Products.[CategoryId], 
        Products.[PhotoFileName]
    FROM Products WITH(NOLOCK)
    WHERE Products.Id IN (SELECT ItemId FROM @ItemId);
END