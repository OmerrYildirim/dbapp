
CREATE DATABASE AKADEMEDYA
GO

use AKADEMEDYA

CREATE TABLE PERSON (
	PersonID int IDENTITY(1,1) Primary Key,
	FullName nvarchar(50) NOT NULL,
	HireDate Date NOT NULL,
--bundan sonrası yeni eklendi
	Email varchar(100) NOT NULL UNIQUE,
	PASSWORD_ VARCHAR(16) NOT NULL,
	PERSONTYPE VARCHAR(20) NOT NULL,

)
--manager silindi
--CREATE TABLE MANAGER (
--	ManagerID int PRIMARY KEY,
--	ProductID int,
--
--	Foreign Key (ManagerID) references Person(PersonID)
--)

CREATE TABLE EMPLOYEE (
	EmployeeID int PRIMARY KEY,
	ManagerID int,

	Foreign Key (ManagerID) references Employee(EmployeeID),
	Foreign Key (EmployeeID) references Person(PersonID)
)


-- bu customer'di bunu companyye çevirdik
CREATE TABLE COMPANY(
	CompanyID int IDENTITY(1000,3) PRIMARY KEY,
	CompanyName nvarchar(50) NOT NULL,
	City nvarchar(20) NOT NULL,
	Street nvarchar(50) NOT NULL,
	ZIPCODE varchar(5) NOT NULL,
	PhoneNumber varchar(19) UNIQUE NOT NULL,
)

--BUNU YENİ EKLEDİK COMPANI İLE İLİŞKİLİ İNSAN
CREATE TABLE CUSTOMER(
	CustomerID int NOT NULL,
	CompanyID int NOT NULL,

	Foreign Key (CustomerID) references Person(PersonID),
	Foreign Key (CompanyID ) references COMPANY(CompanyID),
)

CREATE TABLE PRODUCT_ (
	ProductID int IDENTITY(1,1) PRIMARY KEY,
	PName nvarchar(50) NOT NULL UNIQUE,
	PDescription nvarchar(500) NOT NULL,
	ReleaseDate DateTime Default GETDATE(),
	--VersionID decimal(5,2) NOT NULL,
	--Foreign Key (VersionID) references PRODUCT_VERSION(VersionID)
)

CREATE TABLE PRODUCT_VERSION(
	VersionID decimal(5,2), 
	ProductID int NOT NULL,
	PVDate datetime DEFAULT GETDATE(),
	PVDescription nvarchar(500) DEFAULT('New Version'),
	
	Primary Key(VersionID, ProductID),
	Foreign Key(ProductID) references PRODUCT_(ProductID)
)



CREATE TABLE EMPLOYEE_PRODUCT(
	EmployeeID int,
	ProductID int,

	Primary Key(EmployeeID,ProductID),
	Foreign Key (EmployeeID) references EMPLOYEE(EmployeeID),
	Foreign Key (ProductID) references PRODUCT_(ProductID)
)

CREATE TABLE FEEDBACK(
	FeedbackID int IDENTITY(1,1) PRIMARY KEY,
	FeedbackDate date DEFAULT GETDATE(),
	Message_ nvarchar(500) NOT NULL, --ISMI DEGISTI eskiden commentti
	--RATING KALKTI
	ProductID int NOT NULL,
	CompanyID int NOT NULL,
	Feedback_Type varchar(20) NOT NULL, --YENI EKLENDI
	
	Foreign Key (CompanyID) references Company(CompanyID),
	Foreign Key (ProductID) references PRODUCT_(ProductID)
)


--FEATUREREQUEST BUGREPORT TABLOLARI YENI EKLENDI
CREATE TABLE FEATUREREQUEST(
	FeedbackID int PRIMARY KEY,
	Rating int CHECK(Rating in (1,2,3,4,5)) NOT NULL, --neden int degil

	Foreign Key (FeedbackID) references FEEDBACK(FeedbackID)
)

CREATE TABLE BUGREPORT (
    FeedbackID int PRIMARY KEY,
    VersionID decimal(5,2),
    ProductID int, -- olusturuurken feedbackle aynı product id olmasi lazim
	-- bunun icin trigger yazilmali
    Foreign Key (VersionID, ProductID) references PRODUCT_VERSION(VersionID, ProductID),
	Foreign Key (FeedbackID) references FEEDBACK(FeedbackID)
);


CREATE TABLE INVOICE(
	InvoiceID int IDENTITY(6800,2),
	Fee money NOT NULL, -- tax included
	Tax as Fee * 0.05 PERSISTED,

	CONSTRAINT FeeCHK CHECK(Fee >= 0),
	PRIMARY KEY(InvoiceID)
)


CREATE TABLE LICENCE(
	LicenceID int IDENTITY(200000,13) PRIMARY KEY,
	LicenceTerm int check(LicenceTerm > 0) NOT NULL,
	StartDate datetime DEFAULT GETDATE(),
	EndDate AS DATEADD(DAY, LicenceTerm, StartDate) PERSISTED,
	--NumberOfComputer int NOT NULL,
	--price'ı sildik invoiceye fee eklendi bunu yerine
	InvoiceID int NOT NULL,
	ProductID int NOT NULL,
	CompanyID int NOT NULL,


	--CONSTRAINT NumberOfComputerCHK CHECK(NumberOfComputer > 0),

	Foreign Key (CompanyID) references Company(CompanyID),
	Foreign Key (ProductID) references PRODUCT_(ProductID),
	Foreign Key (InvoiceID) references INVOICE(InvoiceID),
)


CREATE INDEX COMPANYINDEX ON COMPANY (CompanyName)
CREATE INDEX LICENCEINDEX ON LICENCE (CompanyID)













