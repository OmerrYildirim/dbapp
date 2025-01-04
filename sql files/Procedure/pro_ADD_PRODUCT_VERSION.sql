CREATE PROCEDURE pro_ADD_PRODUCT_VERSION
    @ProductID INT,
    @VersionID DECIMAL(5,2),
    @PVDate DATETIME = NULL, 
    @PVDescription NVARCHAR(500) = 'New Version' 
AS
BEGIN
    SET NOCOUNT ON;

    IF @PVDate IS NULL
        SET @PVDate = GETDATE();

    INSERT INTO PRODUCT_VERSION (VersionID, ProductID, PVDate, PVDescription)
    VALUES (@VersionID, @ProductID, @PVDate, @PVDescription);

END;
GO