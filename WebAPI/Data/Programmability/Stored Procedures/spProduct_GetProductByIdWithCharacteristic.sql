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

CREATE PROCEDURE spProduct_GetProductByIdWithCharacteristic @id INT
as
SELECT 
	p.[Id],
	p.[Name], 
	p.[Description], 
	p.[Price], 
	p.[CategoryId], 
	p.[PhotoFileName], 
	cc.[Name] AS CharName, 
	pc.[Value] AS CharValue 
From Products AS p
INNER JOIN ProductCharacteristics AS pc
	ON p.Id = pc.ProductId
INNER JOIN CategoryCharacterictic AS cc
	ON pc.CharacteristicId = cc.Id
WHERE p.Id = @id