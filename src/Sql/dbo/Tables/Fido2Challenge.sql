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
CREATE NONCLUSTERED INDEX [IX_Fido2Challenge_CreationDate]
    ON [dbo].[Fido2Challenge]([CreationDate] ASC)


GO
CREATE NONCLUSTERED INDEX [IX_Fido2Challenge_UserId]
    ON [dbo].[Fido2Challenge]([UserId] ASC);

