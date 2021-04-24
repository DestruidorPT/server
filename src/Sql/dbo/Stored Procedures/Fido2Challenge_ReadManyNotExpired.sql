CREATE PROCEDURE [dbo].[Fido2Challenge_ReadManyNotExpired]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @Threshold DATETIME2(7) = GETUTCDATE()
    
    BEGIN
        SELECT 
            *
        FROM
            [dbo].[Fido2Challenge]
        WHERE
            [UserId] = @UserId
            AND
            [TimeOutDate] IS NOT NULL
            AND
            [TimeOutDate] >= @Threshold
    END
END