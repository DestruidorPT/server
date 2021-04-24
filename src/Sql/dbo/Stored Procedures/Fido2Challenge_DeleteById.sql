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