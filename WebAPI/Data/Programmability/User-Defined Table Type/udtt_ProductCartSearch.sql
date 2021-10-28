USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
	SELECT *
	FROM sys.table_types
	WHERE name = 'ProductCartSearch'
		AND SCHEMA_NAME(schema_id) = 'dbo')
	BEGIN
		DROP TYPE dbo.ProductCartSearch;
	END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
GO

--============================================================================

CREATE TYPE [dbo].[ProductCartSearch] AS TABLE(
	[ItemId] int NOT NULL
);
GO