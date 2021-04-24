CREATE PROCEDURE [dbo].[Fido2Key_Create]
    @Id UNIQUEIDENTIFIER,
    @UserId UNIQUEIDENTIFIER,
    @Name VARCHAR(200),
    @CredentialId VARCHAR(MAX),
    @PublicKey VARCHAR(MAX),
    @SignatureCounter BIGINT,
    @CredentialType TINYINT,
    @AuthenticatorType TINYINT,
    @Transports VARCHAR(200),
    @CreationDate DATETIME2(7)
AS
BEGIN
    SET NOCOUNT ON

    INSERT INTO [dbo].[Fido2Key]
    (
        [Id],
        [UserId],
        [Name],
        [CredentialId],
        [PublicKey],
        [SignatureCounter],
        [CredentialType],
        [AuthenticatorType],
        [Transports],
        [CreationDate]
    )
    VALUES
    (
        @Id,
        @UserId,
        @Name,
        @CredentialId,
        @PublicKey,
        @SignatureCounter,
        @CredentialType,
        @AuthenticatorType,
        @Transports,
        @CreationDate
    )
END