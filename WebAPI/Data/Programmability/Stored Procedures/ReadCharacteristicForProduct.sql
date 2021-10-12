USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'ReadCharacteristicForProduct'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.ReadCharacteristicForProduct;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--============================================================================

CREATE PROCEDURE [dbo].[ReadCharacteristicForProduct] @idProduct int
as 
SELECT CategoryCharacterictic.Name, ProductCharacteristics.Value FROM ProductCharacteristics
INNER JOIN CategoryCharacterictic on ProductCharacteristics.CharacteristicId = CategoryCharacterictic.Id 
where ProductCharacteristics.ProductId = @idProduct
