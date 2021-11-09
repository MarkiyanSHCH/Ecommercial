USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spProducts_GetByCategoryId'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.spProducts_GetByCategoryId;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--============================================================================

CREATE PROCEDURE [dbo].[spProducts_GetByCategoryId]
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
    		Products.[PhotoFileName]
    	FROM Products
    	WHERE CategoryId = @id
END
GO