use AKADEMEDYA

GO

CREATE TRIGGER trg_InsertProductVersion
ON PRODUCT_
AFTER INSERT
AS
BEGIN

SET NOCOUNT ON;

INSERT INTO PRODUCT_VERSION(VersionID,ProductID,PVDate,PVDescription)
SELECT

0.1,
ProductID,
GETDATE(),
'Initial Version'
FROM inserted;
END;
GO

