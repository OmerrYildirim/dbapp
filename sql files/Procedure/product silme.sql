
go
CREATE PROCEDURE pro_DELETE_ALL_LICENCES_FOR_PRODUCT
	@productName nvarchar(50)
AS
BEGIN
	declare @productID int
	set @productID = (
		select p.ProductID
		from PRODUCT_ p
		where p.PName = @productName
	)
	
	DELETE FROM LICENCE
	WHERE ProductID = @productID

END
GO

select *
from LICENCE



GO
CREATE PROCEDURE pro_DELETE_PRODUCT
	@productName nvarchar(50)
AS
BEGIN
	exec dbo.pro_DELETE_ALL_LICENCES_FOR_PRODUCT @productName
	DELETE FROM PRODUCT_
	WHERE PRODUCT_.PName = @productName
END
go

--USAGE

--exec dbo.pro_DELETE_PRODUCT 'IRONIC'


