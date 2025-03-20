-- Заполнение таблицы AccessLevel
INSERT INTO AccessLevel ([Name], [Description])
VALUES
('Admin', 'Администратор системы'),
('User', 'Обычный пользователь'),
('Manager', 'Менеджер');

-- Заполнение таблицы Position
INSERT INTO Position ([Name], Salary, ID_AccessLevel)
VALUES
('Developer', 60000, 2),  -- User
('Designer', 50000, 2),   -- User
('Team Lead', 80000, 1),  -- Admin
('Manager', 70000, 3);    -- Manager

-- Заполнение таблицы Employee
INSERT INTO Employee (Passport, ID_Position)
VALUES
('1234567890', 1),   -- Developer
('9876543210', 2),   -- Designer
('4567890123', 3),   -- Team Lead
('6543210987', 4);   -- Manager

-- Заполнение таблицы CustomerType
INSERT INTO CustomerType ([Description])
VALUES
('VIP'),
('Regular'),
('New');

-- Заполнение таблицы Customer
INSERT INTO Customer (ID_Type)
VALUES
(1),  -- VIP
(2),  -- Regular
(3);  -- New

-- Заполнение таблицы Equipment
INSERT INTO Equipment ([Name], SerialNumber)
VALUES
('Laptop', 'SN12345'),
('Monitor', 'SN67890'),
('Keyboard', 'SN54321');

-- Заполнение таблицы Project
INSERT INTO Project ([Name], ID_Customer, ID_Employee)
VALUES
('Project A', 1, 1),   -- VIP, Developer
('Project B', 2, 3),   -- Regular, Team Lead
('Project C', 3, 2);   -- New, Designer

-- Заполнение таблицы Square
INSERT INTO [Square] ([Name], Alitude, ID_Project)
VALUES
('Square 1', 100, 1),  -- Project A
('Square 2', 150, 2),  -- Project B
('Square 3', 120, 3);  -- Project C

-- Заполнение таблицы Profile
INSERT INTO [Profile] ([Name], ID_Square)
VALUES
('Profile 1', 1),  -- Square 1
('Profile 2', 2),  -- Square 2
('Profile 3', 3);  -- Square 3

-- Заполнение таблицы Picket
INSERT INTO Picket ([Name], ID_profile)
VALUES
('Picket 1', 1),  -- Profile 1
('Picket 2', 2),  -- Profile 2
('Picket 3', 3);  -- Profile 3

-- Заполнение таблицы Point
INSERT INTO [Point] (X, Y, TransitioAmplitude, SignalAnomaly, Amendments, [Datetime], ID_Operator, ID_Equipment, ID_Picket)
VALUES
(10.5, 20.5, 1.0, 0.1, 0.01, CONVERT(DATETIME, '2025-03-20 10:00:00', 120), 1, 1, 1),  -- Operator 1, Equipment 1, Picket 1
(15.5, 25.5, 1.5, 0.2, 0.02, CONVERT(DATETIME, '2025-03-20 11:00:00', 120), 2, 2, 2),  -- Operator 2, Equipment 2, Picket 2
(20.5, 30.5, 2.0, 0.3, 0.03, CONVERT(DATETIME, '2025-03-20 12:00:00', 120), 3, 3, 3);  -- Operator 3, Equipment 3, Picket 3

-- Заполнение таблицы AreaCoordinates
INSERT INTO AreaCoordinates (ID_Square, X, Y)
VALUES
(1, 10.0, 20.0),  -- Square 1
(2, 15.0, 25.0),  -- Square 2
(3, 20.0, 30.0);  -- Square 3

-- Заполнение таблицы ProfileCoordinates
INSERT INTO ProfileCoordinates (ID_profile, X, Y)
VALUES
(1, 10.5, 20.5),  -- Profile 1
(2, 15.5, 25.5),  -- Profile 2
(3, 20.5, 30.5);  -- Profile 3

-- Заполнение таблицы PicketCoordinates
INSERT INTO PicketCoordinates (ID_Picket, X, Y)
VALUES
(1, 10.0, 20.0),  -- Picket 1
(2, 15.0, 25.0),  -- Picket 2
(3, 20.0, 30.0);  -- Picket 3

-- Заполнение таблицы Report
INSERT INTO Report (ID_Employee, ID_Project, ReportFile)
VALUES
(1, 1, 'Report for Project A.pdf'),
(2, 2, 'Report for Project B.pdf');  -- убрана лишняя запятая
