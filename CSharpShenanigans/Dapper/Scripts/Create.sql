CREATE
DATABASE DapperDemo;

USE
DapperDemo;

CREATE TABLE Users
(
    Id        INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName  NVARCHAR(50) NOT NULL
);

-- Let's insert some sample data as well for our queries to work
INSERT INTO Users (FirstName, LastName)
VALUES ('Cick', 'Nhapsas');
INSERT INTO Users (FirstName, LastName)
VALUES ('Dot', 'Net');
