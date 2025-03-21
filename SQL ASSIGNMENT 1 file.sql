--Create the database named "TechShop"
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

--Insert at least 10 sample records into each of the following tables. 
INSERT INTO Customers (customerID, FirstName, LastName, Email, Phone, Address) VALUES
(1, 'Nidhya', 'S R', 'Nid@email.com', '9876543210', 'Pune'),
(2, 'Keerthika', 'C', 'Keer@email.com', '9988776655', 'Mumbai'),
(3, 'Priya', 'Narayanan', 'Priya@email.com', '9871234567', 'Mysore'),
(4, 'Varun', 'Kapoor', 'Varun.VK@email.com', '9876541230', 'Rajasthan'),
(5, 'Mohmed', 'Shoaib', 'shobi.mohd@email.com', '9812345678', 'Chennai'),
(6, 'Kirthana', 'B', 'Kirr@email.com', '9856743120', 'Noida'),
(7, 'Mathangi', 'Subramaniam', 'Maths@gmail.com', '9865432109', 'Hyderabad'),
(8, 'Nithyashree', 'R', 'Nithya@email.com', '9876123456', 'Kochi'),
(9, 'Swathi', 'Vijay', 'swathi.v@email.com', '9874321098', 'Bangalore'),
(10, 'Anuradha', 'Krishnan', 'anu.k@email.com', '9897654321', 'Ahmedabad');

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
--SQL query to retrieve the names and emails of all customers. 
SELECT FirstName, LastName, Email 
FROM Customers;


--SQL query to retrieve the names and emails of all customers. 
SELECT O.OrderID, O.OrderDate, C.FirstName, C.LastName 
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID;

-- SQL query to insert a new customer record into the "Customers" table. 
INSERT INTO Customers (FirstName, LastName, Email, Phone, Address) 
VALUES ('David', 'Miller', 'david.miller@email.com', '9876543211', 'Nagpur');

--update the prices of all electronic gadgets in the "Products" table 
UPDATE Products
SET Price = Price * 1.10;


 
 --SQL query to delete a specific order and its associated order details
DELETE FROM OrderDetails WHERE OrderID = 1;
DELETE FROM Orders WHERE OrderID = 1;

-- SQL query to insert a new order into the "Orders" table
INSERT INTO Orders (CustomerID, OrderDate, TotalAmount)
VALUES (1, GETDATE(), 500.00);


-- SQL query to update the contact information 
UPDATE Customers
SET Email = 'nidh@gmail.com' , Address = 'Mumbai'
WHERE CustomerID = 1;

--Write an SQL query to recalculate and update the total cost of each order
UPDATE Orders
SET TotalAmount = (
    SELECT SUM(OD.Quantity * P.Price)
    FROM OrderDetails OD
    JOIN Products P ON OD.ProductID = P.ProductID
    WHERE OD.OrderID = Orders.OrderID
);

--SQL query to delete all orders and their associated order details for a specific customer from the "Orders" and "OrderDetails" tables. 
DELETE FROM OrderDetails 
WHERE OrderID IN (SELECT OrderID FROM Orders WHERE CustomerID = 1);

DELETE FROM Orders 
WHERE CustomerID = 1;

--SQL query to insert a new electronic gadget product 
INSERT INTO Products (ProductName, Description, Price)
VALUES ('Wireless Earbuds', 'High-quality Bluetooth earbuds with noise cancellation', 2999.00);


--SQL query to insert a new electronic gadget product into the "Products" table
INSERT INTO Products (ProductName, Category, Description, Price, StockQuantity) 
VALUES ('Smart TV 55 Inch', 'Electronics', '4K Ultra HD Smart TV with HDR', 65000.00, 15);

--SQL query to update the status of a specific order in the "Orders" table
UPDATE Orders
SET Status = 'Shipped'
WHERE OrderID = 10;

--SQL query to calculate and update the number of orders placed by each customer  in the "Customers" table based on the data in the "Orders" table. UPDATE Customers

SET OrderCount = (
    SELECT COUNT(*) 
    FROM Orders 
    WHERE Orders.CustomerID = Customers.CustomerID
);


--Task 3

--Write an SQL query to retrieve a list of all orders along with customer information 
SELECT O.OrderID, O.OrderDate, C.FirstName, C.LastName, C.Email
FROM Orders O
JOIN Customers C ON O.CustomerID = C.CustomerID
ORDER BY O.OrderDate DESC;

-- SQL query to find the total revenue generated by each electronic gadget product. 
SELECT P.ProductName, SUM(OD.Quantity * P.Price) AS TotalRevenue
FROM OrderDetails OD
JOIN Products P ON OD.ProductID = P.ProductID
GROUP BY P.ProductName
ORDER BY TotalRevenue DESC;


-- SQL query to list all customers who have made at least one purchase.
SELECT DISTINCT C.CustomerID, C.FirstName, C.LastName, C.Email, C.Phone, C.Address
FROM Customers C
JOIN Orders O ON C.CustomerID = O.CustomerID;

-- SQL query to find the most popular electronic gadget, which is the one with the highest 
total quantity ordered. 
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


--SQL query to calculate the total revenue generated by all orders placed within a specific time period.
SELECT SUM(O.TotalAmount) AS TotalRevenue
FROM Orders O
WHERE O.OrderDate BETWEEN '2024-01-01' AND'2024-12-31';


--Task 4
-- SQL query to find out which customers have not placed any orders.
SELECT CustomerID, FirstName, LastName, Email, Phone
FROM Customers
WHERE CustomerID NOT IN (SELECT DISTINCT CustomerID FROM Orders);

--SQL query to find the total number of products available for sale.
SELECT COUNT(*) AS TotalProducts
FROM Products
WHERE ProductID IN (SELECT ProductID FROM Inventory WHERE QuantityInStock > 0);

--SQL query to calculate the total revenue generated by TechShop.  
SELECT SUM(TotalAmount) AS TotalRevenue
FROM Orders
WHERE OrderID IN (SELECT OrderID FROM OrderDetails);




SELECT SUM(TotalAmount) AS CustomerRevenue
FROM Orders
WHERE CustomerID = 10;

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


