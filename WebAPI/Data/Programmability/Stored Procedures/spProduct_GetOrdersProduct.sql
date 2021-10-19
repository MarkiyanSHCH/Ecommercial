USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spProduct_GetOrdersProduct'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.spProduct_GetOrdersProduct;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--============================================================================

CREATE PROCEDURE [dbo].[spProduct_GetOrdersProduct] 
    @UserId INT
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT 
		Products.[Id],
		Products.[Name],
		Products.[Description],
		Products.[Price],
		Products.[CategoryId],
		Products.[PhotoFileName]
	FROM Products
	INNER JOIN Orders
		ON Products.Id = Orders.ProductId
	INNER JOIN Users
		ON Orders.UserId = Users.Id
END
GO