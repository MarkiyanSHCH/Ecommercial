USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spOrderLines_GetOrderLinesByOrder'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.spOrderLines_GetOrderLinesByOrder;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--============================================================================

CREATE PROCEDURE [dbo].[spOrderLines_GetOrderLinesByOrder]
	@OrderId int
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT
		OrderLines.Id,
		OrderLines.OrderId,
		OrderLines.Note,
		OrderLines.Quantity,
		Products.Id as ProductId,
		Products.[Name],
		Products.Price
	FROM OrderLines
	FULL JOIN Products on OrderLines.ProductId = Products.Id
	WHERE OrderLines.OrderId = @OrderId
END
GO
