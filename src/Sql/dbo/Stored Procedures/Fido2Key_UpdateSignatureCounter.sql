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