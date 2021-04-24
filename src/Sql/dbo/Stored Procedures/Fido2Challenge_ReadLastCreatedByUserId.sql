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