CREATE DATABASE TechShop

CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Phone NVARCHAR(15) UNIQUE NOT NULL,
    Address NVARCHAR(255) NOT NULL
);

CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255),
    Price DECIMAL(10,2) NOT NULL
);
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) ON DELETE CASCADE
);
CREATE TABLE OrderDetails (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);

CREATE TABLE Inventory (
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL,
    QuantityInStock INT NOT NULL CHECK (QuantityInStock >= 0),
    LastStockUpdate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE
);

INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) VALUES
('Nidhya', 'S R', 'Nid@email.com', '9876543210', 'Pune'),
('Keerthika', 'C', 'Keer@email.com', '9988776655', 'Mumbai'),
('Priya', 'Narayanan', 'Priya@email.com', '9871234567', 'Mysore'),
('Varun', 'Kapoor', 'Varun.VK@email.com', '9876541230', 'Rajasthan'),
('Mohmed', 'Shoaib', 'shobi.mohd@email.com', '9812345678', 'Chennai'),
('Kirthana', 'B', 'Kirr@email.com', '9856743120', 'Noida'),
('Mathangi', 'Subramaniam', 'Maths@gmail.com', '9865432109', 'Hyderabad'),
('Nithyashree', 'R', 'Nithya@email.com', '9876123456', 'Kochi'),
('Swathi', 'Vijay', 'swathi.v@email.com', '9874321098', 'Bangalore'),
('Anuradha', 'Krishnan', 'anu.k@email.com', '9897654321', 'Ahmedabad');

INSERT INTO Products (ProductName, Description, Price) VALUES
('Smartphone', 'Latest model with 128GB storage', 599.99),
('Laptop', '15-inch screen with 8GB RAM', 899.99),
('Smartwatch', 'Waterproof with heart-rate monitor', 199.99),
('Wireless Earbuds', 'Noise-canceling with 20-hour battery life', 129.99),
('Tablet', '10-inch display, 64GB storage', 399.99),
('Gaming Console', 'Next-gen console with 1TB SSD', 499.99),
('Bluetooth Speaker', 'Portable with deep bass', 79.99),
('Monitor', '27-inch 4K display', 349.99),
('Mechanical Keyboard', 'RGB backlight with fast response', 99.99),
('External Hard Drive', '2TB storage USB 3.0', 149.99);


INSERT INTO Orders (CustomerID, TotalAmount) VALUES
(1, 799.99), 
(2, 199.99),
(3, 129.99),
(4, 599.99),
(5, 499.99),
(6, 399.99),
(7, 899.99),
(8, 149.99),
(9, 349.99),
(10, 79.99);

select* from customers

INSERT INTO OrderDetails (OrderID, ProductID, Quantity) VALUES
(1, 1, 1),
(2, 3, 2),
(3, 4, 1),
(4, 1, 2),
(5, 6, 1),
(6, 5, 1),
(7, 2, 1),
(8, 10, 1),
(9, 8, 1),
(10, 7, 3);

INSERT INTO Inventory (ProductID, QuantityInStock) VALUES
(1, 50),
(2, 30),
(3, 100),
(4, 75),
(5, 40),
(6, 25),
(7, 60),
(8, 20),
(9, 35),
(10, 55);

SELECT * FROM Inventory;
SELECT * FROM Orders;

--Task 2
SELECT FirstName, LastName, Email 
FROM Customers;

SELECT O.OrderID, O.OrderDate, C.FirstName, C.LastName 
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID;

INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) 
VALUES ('David', 'Miller', 'david.miller@email.com', '9876543211', 'Nagpur');

UPDATE Products
SET Price = Price * 1.10;


DECLARE @OrderID INT = 1; 

DELETE FROM OrderDetails WHERE OrderID = @OrderID;
DELETE FROM Orders WHERE OrderID = @OrderID;

INSERT INTO Orders (CustomerID, OrderDate, TotalAmount)
VALUES (1, GETDATE(), 500.00);

DECLARE @CustomerID INT = 1; -- Change as needed
DECLARE @NewEmail VARCHAR(255) = 'new.email@email.com';
DECLARE @NewAddress VARCHAR(255) = '123 New Street, NY';

UPDATE Customers
SET Email = @NewEmail, Address = @NewAddress
WHERE CustomerID = @CustomerID;

UPDATE Orders
SET TotalAmount = (
    SELECT SUM(OD.Quantity * P.Price)
    FROM OrderDetails OD
    JOIN Products P ON OD.ProductID = P.ProductID
    WHERE OD.OrderID = Orders.OrderID
);

DECLARE @CustomerID INT = 1; 

DELETE FROM OrderDetails 
WHERE OrderID IN (SELECT OrderID FROM Orders WHERE CustomerID = @CustomerID);

DELETE FROM Orders 
WHERE CustomerID = @CustomerID;


INSERT INTO Products (ProductName, Description, Price)
VALUES ('Wireless Earbuds', 'High-quality Bluetooth earbuds with noise cancellation', 2999.00);

DECLARE @OrderID INT = 1; 
DECLARE @NewStatus VARCHAR(50) = 'Shipped';

ALTER TABLE Customers ADD OrderCount INT DEFAULT 0;

UPDATE Customers
SET OrderCount = (
    SELECT COUNT(*) 
    FROM Orders 
    WHERE Orders.CustomerID = Customers.CustomerID
);


--Task 3
SELECT O.OrderID, O.OrderDate, C.FirstName, C.LastName, C.Email
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
ORDER BY O.OrderDate DESC;

SELECT P.ProductName, SUM(OD.Quantity * P.Price) AS TotalRevenue
FROM OrderDetails OD
JOIN Products P ON OD.ProductID = P.ProductID
GROUP BY P.ProductName
ORDER BY TotalRevenue DESC;

SELECT DISTINCT C.CustomerID, C.FirstName, C.LastName, C.Email, C.Phone, C.Address
FROM Customers C
JOIN Orders O ON C.CustomerID = O.CustomerID;

SELECT TOP 1 P.ProductName, SUM(OD.Quantity) AS TotalQuantityOrdered
FROM OrderDetails OD
JOIN Products P ON OD.ProductID = P.ProductID
GROUP BY P.ProductName
ORDER BY TotalQuantityOrdered DESC;


SELECT C.FirstName, C.LastName, AVG(O.TotalAmount) AS AvgOrderValue
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
GROUP BY C.FirstName, C.LastName
ORDER BY AvgOrderValue DESC;

SELECT TOP 1 O.OrderID, C.FirstName, C.LastName, O.TotalAmount
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
ORDER BY O.TotalAmount DESC;

SELECT P.ProductName, COUNT(OD.OrderID) AS TimesOrdered
FROM OrderDetails OD
JOIN Products P ON OD.ProductID = P.ProductID
GROUP BY P.ProductName
ORDER BY TimesOrdered DESC;

DECLARE @ProductName VARCHAR(255) = 'Wireless Earbuds'; -- Change as needed

DECLARE @StartDate DATE = '2024-01-01';  -- Change as needed
DECLARE @EndDate DATE = '2024-12-31';    -- Change as needed

SELECT SUM(O.TotalAmount) AS TotalRevenue
FROM Orders O
WHERE O.OrderDate BETWEEN @StartDate AND @EndDate;


--Task 4
SELECT CustomerID, FirstName, LastName, Email, Phone
FROM Customers
WHERE CustomerID NOT IN (SELECT DISTINCT CustomerID FROM Orders);

SELECT COUNT(*) AS TotalProducts
FROM Products
WHERE ProductID IN (SELECT ProductID FROM Inventory WHERE QuantityInStock > 0);

SELECT SUM(TotalAmount) AS TotalRevenue
FROM Orders
WHERE OrderID IN (SELECT OrderID FROM OrderDetails);


DECLARE @CustomerID INT = 101;  

SELECT SUM(TotalAmount) AS CustomerRevenue
FROM Orders
WHERE CustomerID = @CustomerID;

SELECT CustomerID, FirstName, LastName, OrderCount
FROM (
    SELECT C.CustomerID, C.FirstName, C.LastName, COUNT(O.OrderID) AS OrderCount
    FROM Customers C
    JOIN Orders O ON C.CustomerID = O.CustomerID
    GROUP BY C.CustomerID, C.FirstName, C.LastName
) AS OrderSummary
WHERE OrderCount = (SELECT MAX(OrderCount) FROM (
    SELECT COUNT(OrderID) AS OrderCount FROM Orders GROUP BY CustomerID
) AS MaxOrders);


SELECT TOP 1 C.CustomerID, C.FirstName, C.LastName, SUM(O.TotalAmount) AS TotalSpent
FROM Customers C
JOIN Orders O ON C.CustomerID = O.CustomerID
GROUP BY C.CustomerID, C.FirstName, C.LastName
ORDER BY TotalSpent DESC;

SELECT AVG(TotalAmount) AS AvgOrderValue
FROM Orders;

SELECT C.CustomerID, C.FirstName, C.LastName, COUNT(O.OrderID) AS OrderCount
FROM Customers C
LEFT JOIN Orders O ON C.CustomerID = O.CustomerID
GROUP BY C.CustomerID, C.FirstName, C.LastName
ORDER BY OrderCount DESC;


