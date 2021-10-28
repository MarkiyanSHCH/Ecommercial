USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spOrders_GetOrderByUserId'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.spOrders_GetOrderByUserId;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--============================================================================

CREATE PROCEDURE [dbo].[spOrders_GetOrderByUserId]
	@UserId int
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT
		Orders.Id,
		Orders.TotalPrice,
		Orders.ShopId,
		Orders.OrderDate
	FROM Orders
	WHERE Orders.UserId = @UserId
END
GO