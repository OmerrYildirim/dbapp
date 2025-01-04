
go
create procedure pro_VIEW_BUG_REPORTS_MANAGER
	@managerMail varchar(100)
AS
BEGIN
	
	SELECT pro.PName, br.VersionID, fb.Message_, cmp.CompanyName, fb.FeedbackDate
	FROM FEEDBACK fb inner join BUGREPORT br on fb.FeedbackID = br.FeedbackID
		inner join Company cmp on cmp.CompanyID = fb.CompanyID
		inner join PRODUCT_ pro on pro.ProductID = fb.ProductID
	where fb.ProductID in (
		select p.ProductID
		from EMPLOYEE emp inner join PERSON per on emp.EmployeeID = per.PersonID
			inner join EMPLOYEE_PRODUCT ep on ep.EmployeeID = emp.EmployeeID
			inner join PRODUCT_ p on p.ProductID = ep.ProductID
		where per.Email = @managerMail
	
	order by pro.PName)
END
GO




go
create procedure pro_VIEW_FEATURE_REQUESTS_MANAGER
	@managerMail varchar(100)
AS
BEGIN
	SELECT pro.PName, fr.Rating, fb.Message_, cmp.CompanyName, fb.FeedbackDate
	FROM FEEDBACK fb inner join FEATUREREQUEST fr on fb.FeedbackID = fr.FeedbackID
		inner join Company cmp on cmp.CompanyID = fb.CompanyID
		inner join PRODUCT_ pro on pro.ProductID = fb.ProductID
	where fb.ProductID in (
		select p.ProductID
		from EMPLOYEE emp inner join PERSON per on emp.EmployeeID = per.PersonID
			inner join EMPLOYEE_PRODUCT ep on ep.EmployeeID = emp.EmployeeID
			inner join PRODUCT_ p on p.ProductID = ep.ProductID
		where per.Email = @managerMail
	)
	order by pro.PName
	
END
GO



go
create procedure pro_VIEW_AVG_RATING_MANAGER
	@managerMail varchar(100)
AS
BEGIN
	SELECT pro.PName, avg(fr.Rating)
	FROM FEEDBACK fb inner join FEATUREREQUEST fr on fb.FeedbackID = fr.FeedbackID
		inner join Company cmp on cmp.CompanyID = fb.CompanyID
		inner join PRODUCT_ pro on pro.ProductID = fb.ProductID
	where fb.ProductID in (
		select p.ProductID
		from EMPLOYEE emp inner join PERSON per on emp.EmployeeID = per.PersonID
			inner join EMPLOYEE_PRODUCT ep on ep.EmployeeID = emp.EmployeeID
			inner join PRODUCT_ p on p.ProductID = ep.ProductID
		where per.Email = @managerMail
	)
	group by pro.PName
	order by pro.PName
	
END
GO


CREATE PROCEDURE pro_VIEW_AVG_RATING_PRODUCT
	@productName varchar(100)
AS
BEGIN
	SELECT avg(fr.Rating)
	FROM FEEDBACK fb inner join FEATUREREQUEST fr on fb.FeedbackID = fr.FeedbackID
		inner join Company cmp on cmp.CompanyID = fb.CompanyID
		inner join PRODUCT_ pro on pro.ProductID = fb.ProductID
	where fb.ProductID in (
		select p.ProductID
		from PRODUCT_ p 
		where p.PName = @productName
	)
	group by pro.PName
	order by pro.PName
	
END
GO



-- usage 

-- exec pro_VIEW_BUG_REPORTS_MANAGER 'kadirdemir@hotmail.com'
-- exec pro_VIEW_FEATURE_REQUESTS_MANAGER 'selimaksoy@gmail.com'
-- exec pro_VIEW_FEATURE_REQUESTS_MANAGER 'leylacelik@gmail.com'
-- exec pro_VIEW_FEATURE_REQUESTS_MANAGER 'ayselyilmaz@gmail.com'
