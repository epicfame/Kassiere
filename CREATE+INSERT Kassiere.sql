--CREATE DATABSE
GO
DROP DATABASE Kassiere
GO
CREATE DATABASE Kassiere
GO
USE Kassiere
GO

-- CREATE TABLE
CREATE TABLE Staff(
    StaffID CHAR(5) NOT NULL CHECK(StaffID LIKE 'ST[0-9][0-9][0-9]')PRIMARY KEY,
	StaffPassword VARCHAR(20) NOT NULL,
    StaffName VARCHAR(50) NOT NULL,
    StaffEmail VARCHAR(50) CHECK(StaffEmail LIKE '%@kassiere.com') NOT NULL,
    StaffDoB DATE CHECK(YEAR(StaffDoB) >= 1960) NOT NULL, 
    StaffGender VARCHAR(10) CHECK(StaffGender = 'Male' OR StaffGender = 'Female') NOT NULL,
    StaffAddress VARCHAR(50) NOT NULL,
	StaffPhoneNumber VARCHAR(50) NOT NULL,
    StaffSalary INT NOT NULL
)

CREATE TABLE ItemCategory(
	ItemCategoryID CHAR(5) NOT NULL CHECK(ItemCategoryID LIKE 'IC[0-9][0-9][0-9]')PRIMARY KEY,
	ItemName VARCHAR(50) NOT NULL
)

CREATE TABLE Item(
	ItemID CHAR(5) NOT NULL CHECK(ItemID LIKE 'IT[0-9][0-9][0-9]')PRIMARY KEY,
	ItemCategoryID CHAR(5) FOREIGN KEY REFERENCES ItemCategory(ItemCategoryID) NOT NULL,
	ItemName VARCHAR(50) NOT NULL,
	ItemQuantity INT NOT NULL,
	ItemPrice INT NOT NULL
)

CREATE TABLE TransactionHeader(
    TransactionID CHAR(5) NOT NULL CHECK(TransactionID LIKE 'TH[0-9][0-9][0-9]')PRIMARY KEY,
	StaffID CHAR(5) FOREIGN KEY REFERENCES Staff(StaffID) NOT NULL,
    TransactionDate DATETIME NOT NULL
)

CREATE TABLE TransactionDetail(
	TransactionID CHAR(5) FOREIGN KEY REFERENCES TransactionHeader(TransactionID) NOT NULL,
	ItemID CHAR(5) FOREIGN KEY REFERENCES Item(ItemID),
	Quantity INT
)

--INSERT DATA
INSERT INTO Staff (StaffID,StaffPassword,StaffName,StaffEmail,StaffDoB,StaffGender,StaffAddress,StaffPhoneNumber,StaffSalary)
VALUES
  ('ST116','urna,','Tanner Kirk','orci.luctus@kassiere.com','19781212','Female','Ap #347-6450 Netus Ave','1-734-830-1187',232100),
  ('ST176','fringilla','Eric West','nonummy.ipsum.non@kassiere.com','20220115','Male','644-3760 Sociis Street','336-1167',295729),
  ('ST338','sociis','Josiah Owens','eu.odio.phasellus@kassiere.com','19611126','Female','Ap #799-1869 Malesuada Street','651-7028',313314),
  ('ST708','faucibus','Jason Chen','at@kassiere.com','20150920','Male','Ap #601-2309 Aliquet, Road','572-1241',413506),
  ('ST375','Morbi','Lane Frederick','nec.enim@kassiere.com','19760607','Female','518-3407 Donec Ave','968-6492',380143),
  ('ST577','tristique','Stuart Hull','vivamus@kassiere.com','20111218','Male','Ap #279-5207 Lorem Ave','1-508-562-8242',140198),
  ('ST450','quis','Sybill Dickson','porta@kassiere.com','19771026','Female','592-4381 Sem Road','1-576-112-6716',188976),
  ('ST832','Aenean','William Barton','nec.urna.et@kassiere.com','20060613','Female','P.O. Box 708, 7057 Lorem Ave','547-7550',358231),
  ('ST237','et','Matthew Anderson','vehicula@kassiere.com','19780507','Female','671-3024 Ac Road','111-2186',109602),
  ('ST480','gravida','Brittany Edwards','lorem.eget@kassiere.com','19700907','Male','1908 Faucibus Avenue','452-2486',366208);

INSERT INTO ItemCategory (ItemCategoryID,ItemName)VALUES
  ('IC321','Detergen'),
  ('IC663','Mi Instan'),
  ('IC141','Sabun'),
  ('IC736','Alat Kecantikan'),
  ('IC183','Kopi dan Teh'),
  ('IC482','Sayuran'),
  ('IC646','Alat Rumah Tangga'),
  ('IC429','Ikan Segar'),
  ('IC474','Kesehatan'),
  ('IC634','Alat Dapur');

INSERT INTO Item (ItemID,ItemCategoryID,ItemName,ItemQuantity,ItemPrice)VALUES
  ('IT572','IC663','Indomie',15,85617),
  ('IT061','IC482','Jagung',48,82147),
  ('IT118','IC634','Talenan',72,75400),
  ('IT574','IC321','Bayclin',61,72235),
  ('IT742','IC482','Kacang Panjang',12,77614),
  ('IT946','IC736','Garniere',71,21339),
  ('IT356','IC429','Tenggiri',89,85916),
  ('IT564','IC321','Rinso',46,96463),
  ('IT849','IC482','Labu Siam',6,25910),
  ('IT047','IC474','Milanta',53,38542);


 INSERT INTO TransactionHeader (TransactionID,StaffID,TransactionDate)
VALUES
  ('TH001','ST577','2023-05-11 06:15:45'),
  ('TH002','ST116','2020-03-04 10:42:01'),
  ('TH003','ST375','2021-05-17 01:24:04'),
  ('TH004','ST480','2020-06-30 10:19:47'),
  ('TH005','ST832','2020-04-20 03:39:27'),
  ('TH006','ST480','2020-03-11 20:45:36'),
  ('TH007','ST450','2020-11-27 08:18:06'),
  ('TH008','ST176','2022-01-05 16:21:09'),
  ('TH009','ST708','2020-06-22 00:07:53'),
  ('TH010','ST237','2021-07-24 04:55:28');

INSERT INTO TransactionDetail(TransactionID,ItemID,Quantity)
VALUES
  ('TH001','IT061',26),
  ('TH002','IT564',27),
  ('TH003','IT574',11),
  ('TH004','IT118',5),
  ('TH005','IT356',7),
  ('TH006','IT849',19),
  ('TH007','IT742',10),
  ('TH008','IT047',28),
  ('TH009','IT572',29),
  ('TH010','IT946',18);