GO 
CREATE PROCEDURE pro_CREATE_LICENCE_UI
	@LicenceTermPar int,
	@ProductNamePar nvarchar(50),
	@CompanyNamePar nvarchar(50)
AS
BEGIN
	Declare @fee money
	Declare @calculatedStartDate datetime

	IF exists(
		SELECT 1
		FROM LICENCE li
		INNER JOIN PRODUCT_ pr ON li.ProductID = pr.ProductID
		INNER JOIN Company co ON co.CompanyID = li.CompanyID
		WHERE pr.PName = @ProductNamePar AND co.CompanyName = @CompanyNamePar
	)

	begin 
		set @calculatedStartDate = (
			select MAX(li.EndDate)
			FROM LICENCE li
			INNER JOIN PRODUCT_ pr ON li.ProductID = pr.ProductID
			INNER JOIN Company co ON co.CompanyID = li.CompanyID
			WHERE pr.PName = @ProductNamePar AND co.CompanyName = @CompanyNamePar
		)
	end
	else 
	begin
		set @calculatedStartDate = GETDATE()
	end


	Set @fee = CAST(@LicenceTermPar * 300 AS MONEY)
	EXEC dbo.pro_CREATE_LICENCE 
	    @LicenceTermPar, 
	    @fee, 
	    @calculatedStartDate, 
	    @ProductNamePar, 
	    @CompanyNamePar;
END
go


--usage
--
--
--exec dbo.pro_CREATE_LICENCE_UI 150, 'IRONIC-OSGB','Enerjisa Uretim'
--
--
