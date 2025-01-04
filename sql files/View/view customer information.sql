
GO
CREATE PROCEDURE pro_VIEW_CUSTOMER_INFORMATION
	@CustomerEmail varchar(100)
AS
BEGIN
	SELECT p.FullName, cmp.CompanyName , p.Email, p.HireDate
	FROM CUSTOMER cus inner join PERSON p on cus.CustomerID = p.PersonID
		inner join Company cmp on cmp.CompanyID = cus.CompanyID
	where p.Email = @CustomerEmail
END

--usage

-- exec dbo.pro_VIEW_CUSTOMER_INFORMATION 'ardaguler@gmail.com'