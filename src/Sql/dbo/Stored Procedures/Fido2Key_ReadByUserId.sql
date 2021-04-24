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