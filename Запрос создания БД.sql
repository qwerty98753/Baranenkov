USE "Materials"

CREATE TABLE Gender
(
	Id INT PRIMARY KEY IDENTITY,
	Name VARCHAR(1),
);

INSERT INTO Gender VALUES ('М'), ('Ж');

CREATE TABLE Clients
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(200),
	Surname NVARCHAR(200),
	Patronymic NVARCHAR(200),
	Gender INT FOREIGN KEY REFERENCES Gender(Id)
);

INSERT INTO Clients VALUES
    ('Илья', 'Полушкин', 'Сергеевич', '1');

CREATE TABLE Users
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(200),
	Password NVARCHAR(500)
);

INSERT INTO Users VALUES
    ('Админ', '123');

CREATE TABLE Materials
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(200),
	Count INT,
	Price INT
);

INSERT INTO Materials VALUES
    ('Шёлк', '100', '699');
	('Полиэстер', '100', '399');
	('Трикотаж', '97', '415');

CREATE TABLE Supplier
(
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(200),
	Date DATE,
	Material INT FOREIGN KEY REFERENCES Materials(Id)
);

INSERT INTO Supplier VALUES
	('Shelgh.ru', '09.11.2022', '1');
