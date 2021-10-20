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
	@UserId int,
	@ProductId int
AS
BEGIN
	SET NOCOUNT OFF;
	INSERT INTO [dbo].[Orders](
		UserId,
		ProductId
	)
	VALUES(
		@UserId,
		@ProductId
	)
END
GO
