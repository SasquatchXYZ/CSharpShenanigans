CREATE PROCEDURE sp_GetUserAndOrders @UserId INT
AS
BEGIN
SELECT Id, FirstName, LastName
FROM Users
WHERE Id = @UserId;

SELECT Id, UserId, OrderDate, Amount
FROM Orders
WHERE UserId = @UserId;
END;
