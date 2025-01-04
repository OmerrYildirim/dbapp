use AKADEMEDYA
go
CREATE PROCEDURE pro_VIEW_CALCULATE_TOTAL_LICENCE_COST
    @CompanyID int
AS
BEGIN
        SET NOCOUNT ON;
		DECLARE @TotalCost MONEY;

		SELECT c.CompanyID, SUM(i.Fee)
		FROM COMPANY c inner join LICENCE l on c.CompanyID = l.CompanyID inner join INVOICE i on i.InvoiceID = l.InvoiceID
		WHERE c.CompanyID = @CompanyID
		GROUP BY c.CompanyID

END





