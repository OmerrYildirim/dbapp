
USE AKADEMEDYA

go
CREATE VIEW VIEW_ALL_PRODUCT_AND_VERSIONS 
AS
	SELECT p.PName, pv.VersionID, pv.PVDate, pv.PVDescription
	FROM PRODUCT_VERSION pv INNER JOIN PRODUCT_ p ON pv.ProductID = p.ProductID

go
CREATE VIEW VIEW_EMPLOYEE_AND_PRODUCT_NAME AS
	SELECT per.FullName, pp.PName
	FROM EMPLOYEE e inner join PERSON per on e.EmployeeID = per.PersonID
		left outer join EMPLOYEE_PRODUCT ep on e.EmployeeID = ep.EmployeeID
		inner join PRODUCT_ pp on pp.ProductID= ep.ProductID
go


GO
CREATE PROCEDURE END_ALL_LICENCES_FOR_PRODUCT
	@PRODUCTID INT
AS
BEGIN
	UPDATE lcn
	SET lcn.LicenceTerm = 0
	FROM LICENCE lcn
	WHERE lcn.ProductID = @PRODUCTID
END;



go
CREATE PROCEDURE pro_CreateEmployee
	@FullNamePar nvarchar(50),
	@HireDatePar Date,
	@EmailPar varchar(100),
	@PasswordPar VARCHAR(16),
	@ManagerMail varchar(100)
AS
BEGIN
	insert into PERSON(FullName,HireDate,Email,PASSWORD_, PERSONTYPE) values
	(@FullNamePar,@HireDatePar,@EmailPar, @PasswordPar, 'employee' )
	
	insert into EMPLOYEE(EmployeeID,ManagerID) values
	(
		(	select p.PersonID
			from PERSON p
			where p.Email = @EmailPar),
		(	select m.PersonID
			from PERSON m
			where m.Email = @ManagerMail
		)
	)
END


go
CREATE PROCEDURE pro_CreateCustomer
	@FullNamePar nvarchar(50),
	@HireDatePar Date,
	@EmailPar varchar(100),
	@PasswordPar VARCHAR(16),
	@CompanyName nvarchar(50)
AS
BEGIN
	insert into PERSON(FullName,HireDate,Email,PASSWORD_, PERSONTYPE) values
	(@FullNamePar,@HireDatePar,@EmailPar, @PasswordPar, 'customer' )
	
	insert into CUSTOMER(CustomerID,CompanyID) values
	(
		(	select p.PersonID
			from PERSON p
			where p.Email = @EmailPar),
		(	select c.CompanyID
			from Company c
			where c.CompanyName = @CompanyName )
	)
END


go
CREATE PROCEDURE pro_CREATE_BUG_REPORT
    @messagePar nvarchar(500),
    @fdatePar date,
    @productnamePar nvarchar(50),
    @companynamePar nvarchar(50),
    @versionIDPar decimal(5,2)
as
BEGIN
    insert into FEEDBACK(FeedbackDate,Message_,ProductID, CompanyID, Feedback_Type) values
        (@fdatePar, @messagePar, 
            (
                select p.ProductID
                from PRODUCT_ p
                where p.PName = @productnamePar
            ),
            (
                select c.CompanyID
                from Company c
                where c.CompanyName = @companynamePar
            ),'bug report'
        )
    insert into BUGREPORT(FeedbackID,VersionID,ProductID) values
        (
            (
                select MAX(f.FeedbackID)
                from FEEDBACK f
            ), @versionIDPar,
            (
                select p.ProductID
                from PRODUCT_ p
                where p.PName = @productnamePar
            )
        )
END


go
CREATE PROCEDURE pro_CREATE_FEATURE_REQUEST
    @messagePar nvarchar(500),
    @fdatePar date,
    @productnamePar nvarchar(50),
    @companynamePar nvarchar(50),
    @ratingPar int
as
BEGIN
    insert into FEEDBACK(FeedbackDate,Message_,ProductID, CompanyID, Feedback_Type) values
        (@fdatePar, @messagePar, 
            (
                select p.ProductID
                from PRODUCT_ p
                where p.PName = @productnamePar
            ),
            (
                select c.CompanyID
                from Company c
                where c.CompanyName = @companynamePar
            ),'feature request'
        )
    insert into FEATUREREQUEST(FeedbackID,Rating) values
        (
            (
                select MAX(f.FeedbackID)
                from FEEDBACK f
            ), @ratingPar
        )
END

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
		(	select max(i.InvoiceID)
			from INVOICE i
		),
		(	select p.productID 
			from PRODUCT_ p
			where p.PName = @ProductNamePar),
		(	select c.CompanyID
			from COMPANY c 
			where c.CompanyName = @CompanyNamePar)
		)

END


