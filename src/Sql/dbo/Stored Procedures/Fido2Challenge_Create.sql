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