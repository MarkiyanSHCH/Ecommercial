USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spProduct_GetProductByIdWithCharacteristic'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.spProduct_GetProductByIdWithCharacteristic;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--============================================================================

CREATE PROCEDURE [dbo].[spProduct_GetProductByIdWithCharacteristic] 
	@id INT
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT
		Product.[Id],
		Product.[Name], 
		Product.[Description], 
		Product.[Price], 
		Product.[CategoryId], 
		Product.[PhotoFileName], 
		Product.[PropertyName], 
		Product.[PropertyValue] 
	From vProductsWithProperty AS Product
	WHERE Product.Id = @id
END
GO
