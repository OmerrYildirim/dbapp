GO 
CREATE PROCEDURE pro_CREATE_LICENCE_UI
	@LicenceTermPar int,
	@StartDatePar datetime,
	@ProductNamePar nvarchar(50),
	@CompanyNamePar nvarchar(50)
AS
BEGIN
	Declare @fee money
	Set @fee = CAST(@LicenceTermPar * 300 AS MONEY)
	EXEC dbo.pro_CREATE_LICENCE 
	    @LicenceTermPar, 
	    @fee, 
	    @StartDatePar, 
	    @ProductNamePar, 
	    @CompanyNamePar;
END
go


--usage
--
--
--exec dbo.pro_CREATE_LICENCE_UI 150, '2025-01-30', 'IRONIC-OSGB','Enerjisa Uretim'
--
--