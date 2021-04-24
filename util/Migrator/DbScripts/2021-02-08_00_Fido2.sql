GO
PRINT N'Initial Setup of Fido2 Start.';

GO
PRINT N'Creating TABLE [dbo].[Fido2Key]...';

GO
CREATE TABLE [dbo].[Fido2Key] (
    [Id]                    UNIQUEIDENTIFIER    NOT NULL,
    [UserId]                UNIQUEIDENTIFIER    NOT NULL,
    [Name]                  VARCHAR (200)       NOT NULL,
    [CredentialId]          VARCHAR (200)       NOT NULL,
    [PublicKey]             VARCHAR (200)       NOT NULL,
    [SignatureCounter]      BIGINT              NOT NULL,
    [CredentialType]        TINYINT             NOT NULL,
    [AuthenticatorType]     TINYINT             NOT NULL,
    [Transports]            VARCHAR (200)       NOT NULL,
    [Compromised]           BIT                 NOT NULL    DEFAULT 0,
    [CreationDate]          DATETIME2 (7)       NOT NULL,
    CONSTRAINT [PK_Fido2Key] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Fido2Key_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);

GO
PRINT N'Creating VIEW [dbo].[Fido2KeyView]...';

GO
CREATE VIEW [dbo].[Fido2KeyView]
AS
SELECT
    *
FROM
    [dbo].[Fido2Key]
GO

GO
PRINT N'Creating PROCEDURE [dbo].[Fido2Key_Create]...';

GO
CREATE PROCEDURE [dbo].[Fido2Key_Create]
    @Id UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @Name VARCHAR(200),
    @CredentialId VARCHAR(200),
    @PublicKey VARCHAR(200),
    @SignatureCounter BIGINT,
    @CredentialType TINYINT,
    @AuthenticatorType TINYINT,
    @Transports VARCHAR(200),
    @CreationDate DATETIME2(7)
AS
BEGIN
    SET NOCOUNT ON

    INSERT INTO [dbo].[Fido2Key]
    (
        [Id],
        [UserId],
        [Name],
        [CredentialId],
        [PublicKey],
        [SignatureCounter],
        [CredentialType],
        [AuthenticatorType],
        [Transports],
        [CreationDate]
    )
    VALUES
    (
        @Id,
        @UserId,
        @Name,
        @CredentialId,
        @PublicKey,
        @SignatureCounter,
        @CredentialType,
        @AuthenticatorType,
        @Transports,
        @CreationDate
    )
END

GO
PRINT N'Creating PROCEDURE [dbo].[Fido2Key_DeleteById]...';

GO
CREATE PROCEDURE [dbo].[Fido2Key_DeleteById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON

    DELETE
    FROM
        [dbo].[Fido2Key]
    WHERE
        [Id] = @Id
END

GO
PRINT N'Creating PROCEDURE [dbo].[Fido2Key_DeleteByUserId]...';

GO
CREATE PROCEDURE [dbo].[Fido2Key_DeleteByUserId]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON

    DELETE
    FROM
        [dbo].[Fido2Key]
    WHERE
        [UserId] = @UserId
END

GO
PRINT N'Creating PROCEDURE [dbo].[Fido2Key_ReadByUserId]...';

GO
CREATE PROCEDURE [dbo].[Fido2Key_ReadByUserId]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON

    SELECT
        *
    FROM
        [dbo].[Fido2KeyView]
    WHERE
        [UserId] = @UserId
END

GO
PRINT N'Creating PROCEDURE [dbo].[Fido2Key_UpdateSignatureCounter]...';

GO
CREATE PROCEDURE [dbo].[Fido2Key_UpdateSignatureCounter]
    @Id UNIQUEIDENTIFIER,
    @SignatureCounter BIGINT
AS
BEGIN
    SET NOCOUNT ON

    UPDATE
        [dbo].[Fido2Key]
    SET
        [SignatureCounter] = @SignatureCounter
    WHERE
        [Id] = @Id
END

GO
PRINT N'Creating TABLE [dbo].[Fido2Challenge]...';

GO
CREATE TABLE [dbo].[Fido2Challenge] (
    [Id]                INT              IDENTITY (1, 1)    NOT NULL,
    [UserId]            UNIQUEIDENTIFIER                    NOT NULL,
    [Origin]            NVARCHAR (MAX)                      NOT NULL,
    [Options]           NVARCHAR (MAX)                      NOT NULL,
    [Action]            SMALLINT                            NOT NULL,
    [CreationDate]      DATETIME2 (7)                       NOT NULL,
    [TimeOutDate]       DATETIME2 (7)                       NOT NULL,
    CONSTRAINT [PK_Fido2Challenge] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Fido2Challenge_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
);

GO
PRINT N'Creating VIEW [dbo].[Fido2ChallengeView]...';

GO
CREATE VIEW [dbo].[Fido2ChallengeView]
AS
SELECT
    *
FROM
    [dbo].[Fido2Challenge]
GO

GO
PRINT N'Creating PROCEDURE [dbo].[Fido2Challenge_Create]...';

GO
CREATE PROCEDURE [dbo].[Fido2Challenge_Create]
    @UserId UNIQUEIDENTIFIER,
    @Origin VARCHAR(MAX),
    @Options VARCHAR(MAX),
    @Action SMALLINT,
    @CreationDate DATETIME2(7),
    @TimeOutDate DATETIME2(7)
AS
BEGIN
    SET NOCOUNT ON

    INSERT INTO [dbo].[Fido2Challenge]
    (
        [UserId],
        [Origin],
        [Options],
        [Action],
        [CreationDate],
        [TimeOutDate]
    )
    VALUES
    (
        @UserId,
        @Origin,
        @Options,
        @Action,
        @CreationDate,
        @TimeOutDate
    )
END

GO
PRINT N'Creating PROCEDURE [dbo].[Fido2Challenge_DeleteById]...';

GO
CREATE PROCEDURE [dbo].[Fido2Challenge_DeleteById]
    @Id int
AS
BEGIN
    SET NOCOUNT ON

    DELETE
    FROM
        [dbo].[Fido2Challenge]
    WHERE
        [Id] = @Id
END



GO
PRINT N'Creating PROCEDURE [dbo].[Fido2Challenge_DeleteManyExpired]...';

GO
CREATE PROCEDURE [dbo].[Fido2Challenge_DeleteManyExpired]
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @BatchSize INT = 100
    DECLARE @Threshold DATETIME2(7) = GETUTCDATE()

    WHILE @BatchSize > 0
    BEGIN
        DELETE TOP(@BatchSize)
        FROM
            [dbo].[Fido2Challenge]
        WHERE
            [TimeOutDate] IS NULL
            OR 
            [TimeOutDate] < @Threshold

        SET @BatchSize = @@ROWCOUNT
    END
END



GO
PRINT N'Creating PROCEDURE [dbo].[Fido2Challenge_ReadLastCreatedByUserId]...';

GO
CREATE PROCEDURE [dbo].[Fido2Challenge_ReadLastCreatedByUserId]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON

    SELECT TOP 1
        *
    FROM
        [dbo].[Fido2Challenge]
    WHERE
        [UserId] = @UserId
    ORDER BY Id DESC
END

GO
PRINT N'Creating PROCEDURE [dbo].[Fido2Challenge_ReadManyNotExpired]...';

GO
CREATE PROCEDURE [dbo].[Fido2Challenge_ReadManyNotExpired]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @Threshold DATETIME2(7) = GETUTCDATE()
    
    BEGIN
        SELECT 
            *
        FROM
            [dbo].[Fido2Challenge]
        WHERE
            [UserId] = @UserId
            AND
            [TimeOutDate] IS NOT NULL
            AND
            [TimeOutDate] >= @Threshold
    END
END

GO
PRINT N'Initial Setup of Fido2 End.';

GO