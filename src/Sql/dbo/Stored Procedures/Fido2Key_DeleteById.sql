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