USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spOrders_AddOrderItem'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.spOrders_AddOrderItem;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--============================================================================

CREATE PROCEDURE [dbo].[spOrders_AddOrderItem]
	@TotalPrice FLOAT,
	@UserId INT,
	@ShopId INT,
	@OrderLines Lines READONLY
AS
BEGIN
	SET NOCOUNT OFF;
	--========================================================================
    -- Insert:
    --========================================================================
	BEGIN TRANSACTION;

		INSERT INTO Orders 
		(
			[TotalPrice],
			[UserId],
			[ShopId]
		)
		VALUES 
		(
			@TotalPrice,
			@UserId,
			@ShopId
		);

		DECLARE @OrderId INT = CONVERT(INT, SCOPE_IDENTITY());
		
		INSERT INTO OrderLines
		(
			[OrderId],
			[ProductId],
			[Note],
			[Quantity]
		)
		SELECT 
			@OrderId,
			ProductId,
			Note,
			Quantity
		FROM @OrderLines;

	COMMIT TRANSACTION;

	SELECT @OrderId AS 'Id';
END
