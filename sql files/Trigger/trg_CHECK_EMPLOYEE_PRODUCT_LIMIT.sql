CREATE TRIGGER trg_CHECK_EMPLOYEE_PRODUCT_LIMIT
ON EMPLOYEE_PRODUCT
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @EmployeeID INT;
    DECLARE @ProductID INT;

    SELECT @EmployeeID = EmployeeID, @ProductID = ProductID
    FROM INSERTED;

   
    IF (SELECT COUNT(*) 
        FROM EMPLOYEE_PRODUCT 
        WHERE EmployeeID = @EmployeeID) >= 3
    BEGIN
        
        PRINT 'An employee cannot work on more than two product.';
        ROLLBACK TRANSACTION;  
    END
END;