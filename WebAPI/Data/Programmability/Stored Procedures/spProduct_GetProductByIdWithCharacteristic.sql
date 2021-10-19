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

CREATE PROCEDURE spProduct_GetProductByIdWithCharacteristic 
	@id INT
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT
		Products.[Id],
		Products.[Name], 
		Products.[Description], 
		Products.[Price], 
		Products.[CategoryId], 
		Products.[PhotoFileName], 
		CategoryCharacterictic.[Name] AS CharName, 
		ProductCharacteristics.[Value] AS CharValue 
	From Products
	INNER JOIN ProductCharacteristics
		ON Products.Id = ProductCharacteristics.ProductId
	INNER JOIN CategoryCharacterictic
		ON ProductCharacteristics.CharacteristicId = CategoryCharacterictic.Id
	WHERE p.Id = @id
END
GO