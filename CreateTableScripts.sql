CREATE TABLE User (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);

CREATE TABLE SalesPerson (
    SalesPersonId INT IDENTITY(1,1) PRIMARY KEY,
    SalesPersonName NVARCHAR(50) NOT NULL,
    SalesPersonEmail NVARCHAR(100) NOT NULL
);

CREATE TABLE Appointment (
AppointmentId INT IDENTITY (1, 1) PRIMARY KEY,
SalesPersonId INT NOT NULL,
UserId INT NULL,
AppointmentDate DATETIME NOT NULL,
AvailabilityStatus VARCHAR(50) NOT NULL,
AppointmentType VARCHAR(50) NOT NULL,
CONSTRAINT FK_Appointment_SalesPerson FOREIGN KEY (SalesPersonId) REFERENCES SalesPerson(SalesPersonId),
CONSTRAINT FK_Appointment_User FOREIGN KEY (UserId) REFERENCES User
);

CREATE TABLE [Car] (
CarId INT IDENTITY (1, 1) NOT NULL,
ModelName VARCHAR(50) NOT NULL,
ModelNumber INT NOT NULL,
AvailabilityStatus VARCHAR(50) NOT NULL,
ModelYear INT NOT NULL,
CONSTRAINT PK_Car PRIMARY KEY (CarId)
);

CREATE TABLE [TestDrive] (
TestDriveId INT IDENTITY (1, 1) NOT NULL,
TestDriveDate DATETIME NOT NULL,
CarId INT NOT NULL,
CONSTRAINT PK_TestDrive PRIMARY KEY (TestDriveId),
CONSTRAINT FK_TestDrive_Car FOREIGN KEY (CarId) REFERENCES Car(CarId)
);

CREATE TABLE [Order] (
OrderId INT IDENTITY (1, 1) NOT NULL,
CarId INT NOT NULL,
SalesPersonId INT NOT NULL,
UserId INT NOT NULL,
DeliveryDate DATETIME NOT NULL,
OrderStatus VARCHAR(50) NOT NULL,
CONSTRAINT PK_Order PRIMARY KEY (OrderId),
CONSTRAINT FK_Order_Car FOREIGN KEY (CarId) REFERENCES Car(CarId),
CONSTRAINT FK_Order_SalesPerson FOREIGN KEY (SalesPersonId) REFERENCES SalesPerson(SalesPersonId),
CONSTRAINT FK_Order_User FOREIGN KEY (UserId) REFERENCES User
);






