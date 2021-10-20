USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spOrders_RemoveOrderItem'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.spOrders_RemoveOrderItem;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--============================================================================

CREATE PROCEDURE [dbo].[spOrders_RemoveOrderItem]
	@UserId int,
	@ProductId int
AS
BEGIN
	SET NOCOUNT OFF;
	DELETE FROM [dbo].[Orders]
	WHERE [UserId] = @UserId AND [ProductId] = @ProductId
END
GO
