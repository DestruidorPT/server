CREATE PROCEDURE [dbo].[Fido2Challenge_DeleteManyExpired]
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @BatchSize INT = 100
    DECLARE @Threshold DATETIME2(7) = GETUTCDATE()

    WHILE @BatchSize > 0
    BEGIN
        DELETE TOP(@BatchSize)
        FROM
            [dbo].[Fido2Challenge]
        WHERE
            [TimeOutDate] IS NULL
            OR 
            [TimeOutDate] < @Threshold

        SET @BatchSize = @@ROWCOUNT
    END
END