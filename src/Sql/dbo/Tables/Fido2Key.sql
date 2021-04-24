CREATE TABLE [dbo].[Fido2Key] (
    [Id]                    UNIQUEIDENTIFIER    NOT NULL,
    [UserId]                UNIQUEIDENTIFIER    NOT NULL,
    [Name]                  VARCHAR (200)       NOT NULL,
    [CredentialId]          VARCHAR (MAX)       NOT NULL,
    [PublicKey]             VARCHAR (MAX)       NOT NULL,
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
CREATE NONCLUSTERED INDEX [IX_Fido2Key_CreationDate]
    ON [dbo].[Fido2Key]([CreationDate] ASC)


GO
CREATE NONCLUSTERED INDEX [IX_Fido2Key_UserId]
    ON [dbo].[Fido2Key]([UserId] ASC);

