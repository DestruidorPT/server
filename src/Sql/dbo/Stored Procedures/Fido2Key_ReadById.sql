CREATE PROCEDURE [dbo].[Fido2Key_ReadById]
    @Id UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON

    SELECT TOP 1
        *
    FROM
        [dbo].[Fido2KeyView]
    WHERE
        [Id] = @Id
END