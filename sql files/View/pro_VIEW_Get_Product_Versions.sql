CREATE PROCEDURE pro_VIEW_Get_Product_Versions
    @ProductID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        P.ProductID,
        P.PName AS ProductName,
        PV.VersionID,
        PV.PVDate AS VersionDate,
        PV.PVDescription AS VersionDescription
    FROM 
        PRODUCT_ P
    INNER JOIN 
        PRODUCT_VERSION PV ON P.ProductID = PV.ProductID
    WHERE 
        P.ProductID = @ProductID;
END;
GO