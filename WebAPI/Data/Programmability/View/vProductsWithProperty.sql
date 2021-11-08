USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
	SELECT *
	FROM INFORMATION_SCHEMA.VIEWS
	WHERE TABLE_NAME = 'vProductPrices'
		AND TABLE_SCHEMA = 'dbo')
	BEGIN
		DROP VIEW dbo.vProductPrices;
	END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO


CREATE VIEW vProductsWithProperty
AS
SELECT
		Products.[Id],
		Products.[Name], 
		Products.[Description], 
		Products.[Price], 
		Products.[CategoryId], 
		Products.[PhotoFileName], 
		CategoryCharacterictic.[Name] AS PropertyName, 
		ProductCharacteristics.[Value] AS PropertyValue 
	From Products
	Left JOIN ProductCharacteristics
		ON Products.Id = ProductCharacteristics.ProductId
	Left JOIN CategoryCharacterictic
		ON ProductCharacteristics.CharacteristicId = CategoryCharacterictic.Id

GO