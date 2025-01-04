GO
CREATE PROCEDURE pro_VIEW_CUSTOMER_COMPANY_LICENCES
	@CustomerEmail VARCHAR(100)
AS
BEGIN
	SELECT pr.PName, MIN(li.StartDate) 'Start Date', MAX(li.EndDate) 'End Date', SUM(i.Fee) 'Total Fee'
	FROM CUSTOMER cus inner join PERSON p on p.PersonID = cus.CustomerID
		inner join LICENCE li on li.CompanyID = cus.CompanyID
		inner join PRODUCT_ pr on pr.ProductID = li.ProductID
		inner join INVOICE i on i.InvoiceID = li.InvoiceID
	where p.Email = @CustomerEmail
	group by li.ProductID, pr.PName
END
GO

--usage
--
--exec dbo.pro_VIEW_CUSTOMER_COMPANY_LICENCES 'ardaguler@gmail.com'
--
--