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