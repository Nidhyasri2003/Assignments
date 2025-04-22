CREATE DATABASE TechShop;
USE TechShop;

CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100)  NOT NULL,
    Phone NVARCHAR(15)  NOT NULL,
    Address NVARCHAR(255) NULL
);

CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(10,2) NOT NULL
);
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE OrderDetails (
    OrderDetailID INT IDENTITY(1,1) PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Inventory (
    InventoryID INT IDENTITY(1,1) PRIMARY KEY,
    ProductID INT NOT NULL,
    QuantityInStock INT NOT NULL CHECK (QuantityInStock >= 0),
    LastStockUpdate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID) ON DELETE CASCADE ON UPDATE CASCADE
);

INSERT INTO Customers  VALUES
(1 ,'Nidhyasri', 'S R', 'nidhyasrisr@email.com', '9563290765', 'Pune')
INSERT INTO Customers  VALUES
(2, 'Keerthika', 'C', 'keerthi@email.com', '8123456789', 'Mumbai')
INSERT INTO Customers  VALUES
(3, 'Lakshmi', 'priya', 'priya@email.com', '7988776655', 'Mysore')
INSERT INTO Customers  VALUES
(4, 'Varun', 'Kapoor', 'varun.vk@email.com', '7900112233', 'Rajasthan')
INSERT INTO Customers  VALUES
(5, 'Mohamed', 'Shobi', 'mohdshoaib@email.com', '8556677889', 'Chennai')
INSERT INTO Customers  VALUES
(6, 'Kirthana', 'Balaji', 'Kirthana@email.com', '9654321876', 'Coimbattur')
INSERT INTO Customers  VALUES
(7, 'Mathangi', 'Subramanium', 'maths@gmail.com', '8765432109', 'Bangalore')
INSERT INTO Customers  VALUES
(8, 'Nithyashri', 'R', 'Nit.r@email.com', '9345678901', 'Kochi')
INSERT INTO Customers  VALUES
(9, 'Swathi', 'Vijay', 'Swatzz@email.com', '9001122334', 'Hyderabad')
INSERT INTO Customers  VALUES
(10, 'Jayasri', 'S R', 'jayasri@email.com', '9234567890', 'Delhi');


select * from customers
