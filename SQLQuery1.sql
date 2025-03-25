create table AccessLevel(
    ID_AccessLevel INT IDENTITY(1,1) PRIMARY KEY,
    [Name] nvarchar(50),
    [Description] nvarchar(255)
);

create table Position(
    ID_Position INT IDENTITY(1,1) PRIMARY KEY,
    [Name] nvarchar(50),
    Salary INT,
    ID_AccessLevel INT FOREIGN KEY REFERENCES AccessLevel(ID_AccessLevel) ON DELETE SET NULL ON UPDATE CASCADE
);

create table Employee(
    ID_Employee INT IDENTITY(1,1) PRIMARY KEY,
    Passport nvarchar(100),
    [Password] nvarchar(255) NOT NULL,
    [Login] nvarchar(50) NOT NULL,
    ID_Position INT FOREIGN KEY REFERENCES [Position](ID_Position) ON DELETE SET NULL ON UPDATE CASCADE
);

create table CustomerType(
    ID_CustomerType INT IDENTITY(1,1) PRIMARY KEY,
    [Description] nvarchar(255)
);

create table Customer(
    ID_Customer INT IDENTITY(1,1) PRIMARY KEY,
    [Name] nvarchar(100) NOT NULL,
    [Password] nvarchar(255) NOT NULL,
    [Login] nvarchar(50) NOT NULL,
    ID_Type INT FOREIGN KEY REFERENCES CustomerType(ID_CustomerType) ON DELETE SET NULL ON UPDATE CASCADE
);

create table Equipment(
    ID_Equipment INT IDENTITY(1,1) PRIMARY KEY,
    [Name] nvarchar(50),
    SerialNumber nvarchar(50)
);

create table Project(
    ID_Project INT IDENTITY(1,1) PRIMARY KEY,
    [Name] nvarchar(50),
    ID_Customer INT FOREIGN KEY REFERENCES Customer(ID_Customer) ON DELETE SET NULL ON UPDATE CASCADE,
    ID_Employee INT FOREIGN KEY REFERENCES Employee(ID_Employee) ON DELETE SET NULL ON UPDATE CASCADE
);

create table [Square](
    ID_Square INT IDENTITY(1,1) PRIMARY KEY,
    [Name] nvarchar(50),
    Alitude INT, 
    ID_Project INT FOREIGN KEY REFERENCES Project(ID_Project) ON DELETE SET NULL ON UPDATE CASCADE
);

create table [Profile](
    ID_profile INT IDENTITY(1,1) PRIMARY KEY,
    [Name] nvarchar(50),
    ID_Square INT FOREIGN KEY REFERENCES [Square](ID_Square) ON DELETE SET NULL ON UPDATE CASCADE
);

create table Picket(
    ID_Picket INT IDENTITY(1,1) PRIMARY KEY,
    [Name] nvarchar(50),
    ID_profile INT FOREIGN KEY REFERENCES [Profile](ID_profile) ON DELETE NO ACTION ON UPDATE NO ACTION -- Убрано CASCADE
);

create table [Point](
    ID_Point INT IDENTITY(1,1) PRIMARY KEY,
    X FLOAT,
    Y FLOAT,
    Gravity FLOAT,
    GravityAnomaly FLOAT,
    Amendments FLOAT,
    [Datetime] Datetime,
    ID_Operator INT FOREIGN KEY REFERENCES Employee(ID_Employee) ON DELETE SET NULL ON UPDATE CASCADE,
    ID_Equipment INT FOREIGN KEY REFERENCES Equipment(ID_Equipment) ON DELETE SET NULL ON UPDATE CASCADE,
    ID_Picket INT FOREIGN KEY REFERENCES Picket(ID_Picket) ON DELETE NO ACTION ON UPDATE CASCADE
);

create table AreaCoordinates(
    ID_Record INT IDENTITY(1,1) PRIMARY KEY,
    ID_Square INT FOREIGN KEY REFERENCES [Square](ID_Square) ON DELETE SET NULL ON UPDATE CASCADE,
    X FLOAT,
    Y FLOAT
);

create table ProfileCoordinates(
    ID_Record INT IDENTITY(1,1) PRIMARY KEY,
    ID_profile INT FOREIGN KEY REFERENCES [Profile](ID_profile) ON DELETE SET NULL ON UPDATE CASCADE,
    X FLOAT,
    Y FLOAT
);

create table PicketCoordinates(
    ID_Record INT IDENTITY(1,1) PRIMARY KEY,
    ID_Picket INT FOREIGN KEY REFERENCES Picket(ID_Picket) ON DELETE SET NULL ON UPDATE CASCADE,
    X FLOAT,
    Y FLOAT
);

create table Report(
    ID_Report INT IDENTITY(1,1) PRIMARY KEY,
    ID_Employee INT FOREIGN KEY REFERENCES Employee(ID_Employee) ON DELETE SET NULL ON UPDATE CASCADE,
    ID_Project INT FOREIGN KEY REFERENCES Project(ID_Project) ON DELETE NO ACTION ON UPDATE NO ACTION,
    ReportFile nvarchar(MAX)
);

-- Добавление ограничений NOT NULL
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
ALTER TABLE [Point] ALTER COLUMN Gravity FLOAT NOT NULL;
ALTER TABLE [Point] ALTER COLUMN GravityAnomaly FLOAT NOT NULL;
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

GO
-- Триггер для каскадного удаления
CREATE TRIGGER TR_Picket_Delete
ON Picket
INSTEAD OF DELETE
AS
BEGIN
    -- Удаляем связанные записи в Point
    DELETE FROM [Point]
    WHERE ID_Picket IN (SELECT ID_Picket FROM deleted);

    -- Удаляем сам пикет
    DELETE FROM Picket
    WHERE ID_Picket IN (SELECT ID_Picket FROM deleted);
END;
GO