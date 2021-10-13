USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'ReadAllProducts'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.ReadAllProducts;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--============================================================================

CREATE PROCEDURE [dbo].[ReadAllProducts] 
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
END
GO