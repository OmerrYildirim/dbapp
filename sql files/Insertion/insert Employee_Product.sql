--create Procedure pro_INSERT_EMPLOYEE_PRODUCT
--	@employeeName nvarchar(50),
--	@productName nvarchar(50)
--AS
--BEGIN
--	insert EMPLOYEE_PRODUCT values
--	( 
--		(
--			select p.PersonID
--			from PERSON p
--			where p.FullName = @employeeName
--		)
--	,
--		(
--			select p.ProductID
--			from PRODUCT_ p
--			where p.PName = @productName
--		)
--	)
--	
--END


exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Kadir Demir','AYS'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Selim Aksoy','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Leyla Çelik','IRONIC-OSGB'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Aysel Yilmaz','ISGPRO'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Berkcan Gül','MOBILISG'

exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Esra Arslan','ISGPRO'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Cemre Bayram','ISGPRO'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Murat Baykal','ISGPRO'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Burak Akin','ISGPRO'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Hüseyin Tok','ISGPRO'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Seda Arık','ISGPRO'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Selman Güngör','ISGPRO'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Hülya Aydın','ISGPRO'


exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Nisan Karabulut','MOBILISG'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Tugçe Özkan','MOBILISG'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Gökhan Demirtas','MOBILISG'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Büsra Özcan','MOBILISG'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Murat Tokgöz','MOBILISG'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Ömer Çakır','MOBILISG'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Mehmet Yılmaz','MOBILISG'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Emine Kılıç','MOBILISG'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Nihat Güngör','MOBILISG'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Serkan Gökalp','MOBILISG'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Berkay Çolak','MOBILISG'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Yusuf Çelik','MOBILISG'

exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Yusuf Demirtas','AYS'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Seda Çetin','AYS'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Berk Yilmaz','AYS'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Alev Uçar','AYS'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Merve Demirtas','AYS'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Orhan Sahin','AYS'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Murat Tuncer','AYS'

exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Serdar Yildirim','IRONIC-OSGB'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Sibel Demirci','IRONIC-OSGB'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Ece Aydin','IRONIC-OSGB'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Ahmet Öztürk','IRONIC-OSGB'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Sena Güler','IRONIC-OSGB'

exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Mustafa Aydin','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Derya Kaya','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Fikret Kara','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Pelin Özdemir','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Onur Akpinar','IRONIC'


exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Ömer Çakir','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Derya Kaya','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Fikret Kara','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Pelin Özdemir','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Onur Akpinar','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Mustafa Aydin','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Derya Kaya','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Fikret Kara','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Pelin Özdemir','IRONIC'
exec dbo.pro_INSERT_EMPLOYEE_PRODUCT 'Onur Akpinar','IRONIC'

select p.FullName, pm.FullName
from EMPLOYEE e inner join PERSON p on e.EmployeeID = p.PersonID
	left join EMPLOYEE m on e.ManagerID = m.EmployeeID
	left join PERSON pm on m.EmployeeID = pm.PersonID
order by pm.FullName

select p.PName
from PRODUCT_ p


