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

CREATE TABLE Orders
(
    Id        INT PRIMARY KEY IDENTITY(1,1),
    UserId    INT            NOT NULL,
    OrderDate DATETIME       NOT NULL,
    Amount    DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users (Id)
);

INSERT INTO Orders (UserId, OrderDate, Amount)
VALUES (3, GETDATE(), 100.50);
INSERT INTO Orders (UserId, OrderDate, Amount)
VALUES (3, GETDATE(), 250.75);
INSERT INTO Orders (UserId, OrderDate, Amount)
VALUES (2, GETDATE(), 50.25);
