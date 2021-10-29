USE [ProductDB]
GO

------------------------------------------------------------------------------
IF EXISTS(
	SELECT *
	FROM sys.table_types
	WHERE name = 'Lines'
		AND SCHEMA_NAME(schema_id) = 'dbo')
	BEGIN
		DROP TYPE dbo.Lines;
	END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
GO

--============================================================================
CREATE TYPE Lines AS TABLE(
	Note NVARCHAR(150),
	Quantity INT NOT NULL,
	ProductId INT NOT NULL
);
GO