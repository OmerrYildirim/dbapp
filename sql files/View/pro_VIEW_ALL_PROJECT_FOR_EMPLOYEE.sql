create PROCEDURE pro_VIEW_ALL_PROJECT_FOR_EMPLOYEE
    @empMail varchar(100)
AS
BEGIN
    SELECT p.PName, p.PDescription, p.ReleaseDate
    FROM EMPLOYEE e 
        inner join EMPLOYEE_PRODUCT ep on ep.EmployeeID = e.EmployeeID
        inner join PRODUCT_ p on p.ProductID = ep.ProductID
        inner join PERSON per on e.EmployeeID = per.PersonID
    WHERE per.Email = @empMail
END
