USE AKADEMEDYA

GO
CREATE PROCEDURE pro_CREATE_LICENCE
  @LicenceTermPar int,
  @FeePar money,
  @StartDatePar datetime,
  @ProductNamePar nvarchar(50),
  @CompanyNamePar nvarchar(50)

  AS
  BEGIN

      insert into INVOICE(Fee) values
	  (@FeePar)

      insert into LICENCE(LicenceTerm,StartDate,InvoiceID,ProductID,CompanyID) values
	  (@LicenceTermPar, @StartDatePar,
	  (select max(i.InvoiceID)
	  from INVOICE i
	  ),
	  (select p.productID 
	  from PRODUCT_ p
	  where p.PName = @ProductNamePar),
	  (select c.CompanyID
	  from COMPANY c 
	  where c.CompanyName = @CompanyNamePar)
	  )
	  
   END
	
