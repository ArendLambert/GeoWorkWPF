create table AccessLevel(
ID_AccessLevel INT IDENTITY(1,1) PRIMARY KEY,
[Name] nvarchar(50),
[Description] nvarchar(255),
);

create table Position(
ID_Position INT IDENTITY(1,1) PRIMARY KEY,
[Name] nvarchar(50),
Salary INT,
ID_AccessLevel INT FOREIGN KEY REFERENCES AccessLevel(ID_AccessLevel) ON DELETE CASCADE ON UPDATE CASCADE
);

create table Employee(
ID_Employee INT IDENTITY(1,1) PRIMARY KEY,
Passport nvarchar(100),
ID_Position INT FOREIGN KEY REFERENCES [Position](ID_Position) ON DELETE CASCADE ON UPDATE CASCADE,
);

create table CustomerType(
ID_CustomerType INT IDENTITY(1,1) PRIMARY KEY,
[Description] nvarchar(255),
);

create table Customer(
ID_Customer INT IDENTITY(1,1) PRIMARY KEY,
ID_Type INT FOREIGN KEY REFERENCES CustomerType(ID_CustomerType) ON DELETE CASCADE ON UPDATE CASCADE,
);

create table Equipment(
ID_Equipment INT IDENTITY(1,1) PRIMARY KEY,
[Name] nvarchar(50),
SerialNumber nvarchar(50)
);

create table Project(
ID_Project INT IDENTITY(1,1) PRIMARY KEY,
[Name] nvarchar(50),
ID_Customer INT FOREIGN KEY REFERENCES Customer(ID_Customer) ON DELETE CASCADE ON UPDATE CASCADE,
ID_Employee INT FOREIGN KEY REFERENCES Employee(ID_Employee) ON DELETE CASCADE ON UPDATE CASCADE,
);

create table [Square](
ID_Square INT IDENTITY(1,1) PRIMARY KEY,
[Name] nvarchar(50),
Alitude INT, 
ID_Project INT FOREIGN KEY REFERENCES Project(ID_Project) ON DELETE CASCADE ON UPDATE CASCADE,
);

create table [Profile](
ID_profile INT IDENTITY(1,1) PRIMARY KEY,
[Name] nvarchar(50),
ID_Square INT FOREIGN KEY REFERENCES [Square](ID_Square) ON DELETE CASCADE ON UPDATE CASCADE,
);

create table Picket(
ID_Picket INT IDENTITY(1,1) PRIMARY KEY,
[Name] nvarchar(50),
ID_profile INT FOREIGN KEY REFERENCES [Profile](ID_profile) ON DELETE CASCADE ON UPDATE CASCADE
);

create table [Point](
ID_Point INT IDENTITY(1,1) PRIMARY KEY,
X FLOAT,
Y FLOAT,
TransitioAmplitude FLOAT,
SignalAnomaly FLOAT,
Amendments FLOAT,
[Datetime] Datetime,
ID_Operator INT FOREIGN KEY REFERENCES Employee(ID_Employee) ON DELETE CASCADE ON UPDATE CASCADE,
ID_Equipment INT FOREIGN KEY REFERENCES Equipment(ID_Equipment) ON DELETE CASCADE ON UPDATE CASCADE,
ID_Picket INT FOREIGN KEY REFERENCES Picket(ID_Picket),
);

create table AreaCoordinates(
ID_Record INT IDENTITY(1,1) PRIMARY KEY,
ID_Square INT FOREIGN KEY REFERENCES [Square](ID_Square) ON DELETE CASCADE ON UPDATE CASCADE,
X FLOAT,
Y FLOAT,
);

create table ProfileCoordinates(
ID_Record INT IDENTITY(1,1) PRIMARY KEY,
ID_profile INT FOREIGN KEY REFERENCES [Profile](ID_profile) ON DELETE CASCADE ON UPDATE CASCADE,
X FLOAT,
Y FLOAT,
);

create table PicketCoordinates(
ID_Record INT IDENTITY(1,1) PRIMARY KEY,
ID_Picket INT FOREIGN KEY REFERENCES Picket(ID_Picket) ON DELETE CASCADE ON UPDATE CASCADE,
X FLOAT,
Y FLOAT,
);

create table Report(
ID_Report INT IDENTITY(1,1) PRIMARY KEY,
ID_Employee INT FOREIGN KEY REFERENCES Employee(ID_Employee) ON DELETE CASCADE ON UPDATE CASCADE,
ID_Project INT FOREIGN KEY REFERENCES Project(ID_Project),
ReportFile nvarchar(MAX)
);
-- Изменение столбцов на NOT NULL
ALTER TABLE AccessLevel ALTER COLUMN [Name] NVARCHAR(50) NOT NULL;
ALTER TABLE AccessLevel ALTER COLUMN [Description] NVARCHAR(255) NOT NULL;

ALTER TABLE Position ALTER COLUMN [Name] NVARCHAR(50) NOT NULL;
ALTER TABLE Position ALTER COLUMN Salary INT NOT NULL;

ALTER TABLE Employee ALTER COLUMN Passport NVARCHAR(100) NOT NULL;

ALTER TABLE CustomerType ALTER COLUMN [Description] NVARCHAR(255) NOT NULL;

ALTER TABLE Equipment ALTER COLUMN [Name] NVARCHAR(50) NOT NULL;
ALTER TABLE Equipment ALTER COLUMN SerialNumber NVARCHAR(50) NOT NULL;

ALTER TABLE Project ALTER COLUMN [Name] NVARCHAR(50) NOT NULL;

ALTER TABLE [Square] ALTER COLUMN [Name] NVARCHAR(50) NOT NULL;
ALTER TABLE [Square] ALTER COLUMN Alitude INT NOT NULL;

ALTER TABLE [Profile] ALTER COLUMN [Name] NVARCHAR(50) NOT NULL;

ALTER TABLE Picket ALTER COLUMN [Name] NVARCHAR(50) NOT NULL;

ALTER TABLE [Point] ALTER COLUMN X FLOAT NOT NULL;
ALTER TABLE [Point] ALTER COLUMN Y FLOAT NOT NULL;
ALTER TABLE [Point] ALTER COLUMN TransitioAmplitude FLOAT NOT NULL;
ALTER TABLE [Point] ALTER COLUMN SignalAnomaly FLOAT NOT NULL;
ALTER TABLE [Point] ALTER COLUMN Amendments FLOAT NOT NULL;
ALTER TABLE [Point] ALTER COLUMN [Datetime] DATETIME NOT NULL;

ALTER TABLE AreaCoordinates ALTER COLUMN X FLOAT NOT NULL;
ALTER TABLE AreaCoordinates ALTER COLUMN Y FLOAT NOT NULL;

ALTER TABLE ProfileCoordinates ALTER COLUMN X FLOAT NOT NULL;
ALTER TABLE ProfileCoordinates ALTER COLUMN Y FLOAT NOT NULL;

ALTER TABLE PicketCoordinates ALTER COLUMN X FLOAT NOT NULL;
ALTER TABLE PicketCoordinates ALTER COLUMN Y FLOAT NOT NULL;

ALTER TABLE Report ALTER COLUMN ReportFile NVARCHAR(MAX) NOT NULL;

-- Добавление уникальных ограничений
ALTER TABLE Employee ADD CONSTRAINT UQ_Employee_Passport UNIQUE (Passport);
ALTER TABLE Equipment ADD CONSTRAINT UQ_Equipment_SerialNumber UNIQUE (SerialNumber);

-- Добавление внешних ключей с ON DELETE NO ACTION
ALTER TABLE Picket 
ADD CONSTRAINT FK_Picket_Profile 
FOREIGN KEY (ID_profile) 
REFERENCES [Profile](ID_profile) 
ON DELETE NO ACTION;

ALTER TABLE [Point] 
ADD CONSTRAINT FK_Point_Picket 
FOREIGN KEY (ID_Picket) 
REFERENCES Picket(ID_Picket) 
ON DELETE NO ACTION;

ALTER TABLE Report 
ADD CONSTRAINT FK_Report_Project 
FOREIGN KEY (ID_Project) 
REFERENCES Project(ID_Project) 
ON DELETE NO ACTION;
