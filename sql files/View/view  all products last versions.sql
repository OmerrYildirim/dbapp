go
CREATE VIEW view_ALL_PRODUCTS_AND_VERSIONS 
as
	select p.PName, prover.maxVersion 'Last Version'
	from PRODUCT_ p inner join (
		select pv.ProductID, MAX(pv.VersionID) 'maxVersion'
		from PRODUCT_VERSION pv
		group by pv.ProductID
	) prover on p.ProductID = prover.ProductID
go

--usage 

-- select * from dbo.view_ALL_PRODUCTS_AND_VERSIONS