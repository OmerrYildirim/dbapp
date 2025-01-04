
go
CREATE TRIGGER trg_CHECK_MANAGER 
ON EMPLOYEE
AFTER INSERT,UPDATE
AS
BEGIN
	DECLARE @selectedManager int
	set @selectedManager =(	select i.ManagerID 
							from inserted i)
	
	update PERSON
	set PERSONTYPE = 'manager'
	from PERSON p
	where p.PersonID = @selectedManager

END

--SELECT *
--FROM EMPLOYEE