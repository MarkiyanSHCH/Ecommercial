--============================================================================
USE [ProductDB];
GO

------------------------------------------------------------------------------
IF EXISTS(
    SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
    WHERE [ROUTINE_NAME] = 'spUsers_UpdateName'
        AND [ROUTINE_TYPE] = 'PROCEDURE'
        AND [ROUTINE_BODY] = 'SQL'
        AND [SPECIFIC_SCHEMA] = 'dbo')
    BEGIN
        DROP PROCEDURE dbo.spUsers_UpdateName;
    END
GO
------------------------------------------------------------------------------

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

CREATE PROCEDURE dbo.spUsers_UpdateName
    @UserId INT,
    @Name NVARCHAR(40)
AS
BEGIN
    SET NOCOUNT ON;

    --========================================================================
    -- Validation:
    --========================================================================
    IF @UserId IS NULL OR @UserId <= 0
    BEGIN
        RAISERROR ('Must pass a valid @UserId parameter.', 11, 1);
        RETURN -1;
    END

    IF NOT EXISTS (SELECT 1 FROM [dbo].[Users] AS users WITH(NOLOCK) WHERE users.[Id] = @UserId)
    BEGIN
        RAISERROR ('Must pass an existing user via @UserId parameter.', 11, 2);
        RETURN -1;
    END

    IF @Name IS NULL
    BEGIN
        RAISERROR ('Must pass a @Name parameter.', 11, 3);
        RETURN -1;
    END

    --========================================================================
    -- Update:
    --========================================================================
    UPDATE [dbo].[Users]
    SET
        [Name] = @Name
    WHERE [Id] = @UserId;
END
GO