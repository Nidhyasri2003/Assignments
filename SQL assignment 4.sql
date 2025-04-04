create database Couriermanagement;

CREATE TABLE [User] (
    UserID INT PRIMARY KEY,
    Name VARCHAR(255),
    Email VARCHAR(255) UNIQUE,
    Password VARCHAR(255),
    ContactNumber VARCHAR(20),
    Address TEXT
);

-- Courier Table
CREATE TABLE Courier (
    CourierID INT PRIMARY KEY,
    SenderName VARCHAR(255),
    SenderAddress TEXT,
    ReceiverName VARCHAR(255),
    ReceiverAddress TEXT,
    Weight DECIMAL(5, 2),
    Status VARCHAR(50),
    TrackingNumber VARCHAR(20) UNIQUE,
    DeliveryDate DATE
);

-- Courier Services Table
CREATE TABLE CourierServices (
    ServiceID INT PRIMARY KEY,
    ServiceName VARCHAR(100),
    Cost DECIMAL(8, 2)
);

-- Employee Table
CREATE TABLE Employee (
    EmployeeID INT PRIMARY KEY,
    Name VARCHAR(255),
    Email VARCHAR(255) UNIQUE,
    ContactNumber VARCHAR(20),
    Role VARCHAR(50),
    Salary DECIMAL(10, 2)
);

-- Location Table
CREATE TABLE Location (
    LocationID INT PRIMARY KEY,
    LocationName VARCHAR(100),
    Address TEXT
);

-- Payment Table
CREATE TABLE Payment (
    PaymentID INT PRIMARY KEY,
    CourierID INT,
    LocationID INT,
    Amount DECIMAL(10, 2),
    PaymentDate DATE,
    FOREIGN KEY (CourierID) REFERENCES Courier(CourierID),
    FOREIGN KEY (LocationID) REFERENCES Location(LocationID)
);

INSERT INTO [User] VALUES 
(1, 'Nidhya Sri', 'nidhya@example.com', 'pass123', '9876543210', 'Chennai, TN'),
(2, 'keerthi', 'arun@example.com', 'arun456', '9123456780', 'Coimbatore, TN');

-- Insert Courier Services
INSERT INTO CourierServices VALUES 
(1, 'Standard Delivery', 100.00),
(2, 'Express Delivery', 200.00);

-- Insert Couriers
INSERT INTO Courier VALUES 
(101, 'Nidhya Sri', 'Chennai, TN', 'keerthi', 'Coimbatore, TN', 2.5, 'In Transit', 'TRK123456', '2025-04-10'),
(102, 'keerthi', 'Coimbatore, TN', 'Nidhya Sri', 'Chennai, TN', 1.0, 'Delivered', 'TRK789012', '2025-04-01');

-- Insert Employees
INSERT INTO Employee VALUES 
(1, 'Priya ', 'priya@courier.com', '8888888888', 'Manager', 50000.00),
(2, 'rose', 'rose@courier', '9999999999', 'Delivery Agent', 25000.00);

-- Insert Locations
INSERT INTO Location VALUES 
(1, 'Chennai Office', '123 Mount Road, Chennai'),
(2, 'Coimbatore Office', '456 Avinashi Road, Coimbatore');

-- Insert Payments
INSERT INTO Payment VALUES 
(1, 101, 1, 100.00, '2025-04-05'),
(2, 102, 2, 200.00, '2025-04-02');

--task 2 
-- 1. List all customers:
SELECT * FROM [User];

-- 2. List all orders for a specific customer 
SELECT * FROM Courier
WHERE SenderName = 'Nidhya Sri';

-- 3. List all couriers:
SELECT * FROM Courier;

-- 4. List all packages for a specific order 
SELECT * FROM Courier
WHERE CourierID = 101;

-- 5. List all deliveries for a specific courier 
SELECT * FROM Courier
WHERE TrackingNumber = 'TRK123456';

-- 6. List all undelivered packages 
SELECT * FROM Courier
WHERE Status != 'Delivered';

-- 7. List all packages that are scheduled for delivery today:
SELECT * FROM Courier
WHERE DeliveryDate = CAST(GETDATE() AS DATE);

-- 8. List all packages with a specific status 
SELECT * FROM Courier
WHERE Status = 'In Transit';

-- 9. Calculate the total number of packages for each courier 
SELECT SenderName, COUNT(*) AS TotalPackages
FROM Courier
GROUP BY SenderName;

-- 11. List all packages with a specific weight range (e.g., between 1.0kg and 3.0kg):
SELECT * FROM Courier
WHERE Weight BETWEEN 1.0 AND 3.0;

-- 12. Retrieve employees whose names contain 'John':
SELECT * FROM Employee
WHERE Name LIKE '%John%';

-- 13. Retrieve all courier records with payments greater than a specific amount (e.g., 150.00):
SELECT c.*
FROM Courier c
JOIN Payment p ON c.CourierID = p.CourierID
WHERE p.Amount > 150.00;


ALTER TABLE Courier
ADD EmployeeID INT;

ALTER TABLE Courier
ADD CONSTRAINT FK_Courier_Employee
FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID);


UPDATE Courier
SET EmployeeID = 1;

SELECT EmployeeID, COUNT(*) AS TotalCouriersHandled
FROM Courier
GROUP BY EmployeeID;

-- 15. Calculate the total revenue generated by each location
SELECT LocationID, SUM(Amount) AS TotalRevenue
FROM Payment
GROUP BY LocationID;


ALTER TABLE Courier
ADD ReceiverLocationID INT;

-- Add foreign key constraint 
ALTER TABLE Courier
ADD CONSTRAINT FK_Courier_ReceiverLocation
FOREIGN KEY (ReceiverLocationID) REFERENCES Location(LocationID);

UPDATE Courier
SET ReceiverLocationID = 1;

SELECT ReceiverLocationID, COUNT(*) AS TotalDelivered
FROM Courier
WHERE Status = 'Delivered'
GROUP BY ReceiverLocationID;

SELECT LocationID, SUM(Amount) AS TotalPayments
FROM Payment
GROUP BY LocationID
HAVING SUM(Amount) < 2000;

-- 19. Calculate Total Payments per Location
SELECT LocationID, SUM(Amount) AS TotalPayments
FROM Payment
GROUP BY LocationID;

SELECT CourierID, SUM(Amount) AS TotalPayment
FROM Payment
WHERE LocationID = 1
GROUP BY CourierID
HAVING SUM(Amount) > 1000;

-- 21. Retrieve couriers who have received payments totaling more than $1000 after a certain date (e.g., '2025-01-01')
SELECT CourierID, SUM(Amount) AS TotalPayment
FROM Payment
WHERE PaymentDate > '2025-01-01'
GROUP BY CourierID
HAVING SUM(Amount) > 1000;

-- 22. Retrieve locations where the total amount received is more than $5000 before a certain date (e.g., '2025-03-01')
SELECT LocationID, SUM(Amount) AS TotalAmount
FROM Payment
WHERE PaymentDate < '2025-03-01'
GROUP BY LocationID
HAVING SUM(Amount) > 5000;

--task 3
SELECT P.*, C.*
FROM Payment P
INNER JOIN Courier C ON P.CourierID = C.CourierID;

-- 24. Retrieve Payments with Location Information
SELECT P.*, L.*
FROM Payment P
INNER JOIN Location L ON P.LocationID = L.LocationID;

-- 25. Retrieve Payments with Courier and Location Information
SELECT P.*, C.*, L.*
FROM Payment P
INNER JOIN Courier C ON P.CourierID = C.CourierID
INNER JOIN Location L ON P.LocationID = L.LocationID;

-- 26. List all payments with courier details
SELECT P.PaymentID, P.Amount, P.PaymentDate, C.SenderName, C.ReceiverName
FROM Payment P
JOIN Courier C ON P.CourierID = C.CourierID;

-- 27. Total payments received for each courier
SELECT CourierID, SUM(Amount) AS TotalPayment
FROM Payment
GROUP BY CourierID;

-- 28. List payments made on a specific date 
SELECT *
FROM Payment
WHERE PaymentDate = '2025-04-01';

-- 29. Get Courier Information for Each Payment
SELECT P.PaymentID, C.*
FROM Payment P
JOIN Courier C ON P.CourierID = C.CourierID;

-- 30. Get Payment Details with Location
SELECT P.PaymentID, P.Amount, P.PaymentDate, L.LocationName
FROM Payment P
JOIN Location L ON P.LocationID = L.LocationID;

SELECT C.CourierID, SUM(P.Amount) AS TotalPayment
FROM Courier C
JOIN Payment P ON C.CourierID = P.CourierID
GROUP BY C.CourierID;

-- 32. List Payments Within a Date Range
SELECT *
FROM Payment
WHERE PaymentDate BETWEEN '2025-04-01' AND '2025-04-04';

-- 33. Retrieve all users and their corresponding courier records, including no matches
SELECT U.*, C.*
FROM [User] U
FULL OUTER JOIN Courier C ON U.Name = C.SenderName;

SELECT E.*, P.*
FROM Employee E
LEFT JOIN Courier C ON E.EmployeeID = C.EmployeeID
LEFT JOIN Payment P ON C.CourierID = P.CourierID;

-- 36. List all users and all courier services (Cross Join)
SELECT U.Name, S.ServiceName
FROM [User] U
CROSS JOIN CourierServices S;

-- 37. List all employees and all locations (Cross Join)
SELECT E.Name AS EmployeeName, L.LocationName
FROM Employee E
CROSS JOIN Location L;

-- 38. Retrieve a list of couriers and their corresponding sender info
SELECT C.CourierID, U.*
FROM Courier C
LEFT JOIN [User] U ON C.SenderName = U.Name;

-- 39. Retrieve a list of couriers and their corresponding receiver info
SELECT C.CourierID, C.ReceiverName, U.*
FROM Courier C
LEFT JOIN [User] U ON C.ReceiverName = U.Name;

SELECT C.*, S.*
FROM Courier C
LEFT JOIN CourierServices S ON C.CourierID = S.ServiceID;

-- 41. Retrieve a list of employees and number of couriers assigned
SELECT E.EmployeeID, E.Name, COUNT(C.CourierID) AS TotalCouriers
FROM Employee E
LEFT JOIN Courier C ON E.EmployeeID = C.EmployeeID
GROUP BY E.EmployeeID, E.Name;

-- 42. Retrieve locations and total payment amount received
SELECT L.LocationID, L.LocationName, SUM(P.Amount) AS TotalReceived
FROM Location L
JOIN Payment P ON L.LocationID = P.LocationID
GROUP BY L.LocationID, L.LocationName;
-- 43. Retrieve all couriers sent by the same sender
SELECT SenderName, COUNT(*) AS TotalSent
FROM Courier
GROUP BY SenderName
HAVING COUNT(*) > 1;

-- 44. List all employees who share the same role
SELECT Role, COUNT(*) AS TotalEmployees
FROM Employee
GROUP BY Role
HAVING COUNT(*) > 1;

ALTER TABLE Courier
ALTER COLUMN SenderAddress VARCHAR(500); 

-- 45. Retrieve all payments for couriers sent from the same location
SELECT C.SenderAddress, SUM(P.Amount) AS TotalPayment
FROM Courier C
JOIN Payment P ON C.CourierID = P.CourierID
GROUP BY C.SenderAddress;

-- 46. Retrieve all couriers sent from the same location
SELECT SenderAddress, COUNT(*) AS TotalCouriers
FROM Courier
GROUP BY SenderAddress
HAVING COUNT(*) > 1;

-- 47. List employees and number of couriers they delivered
SELECT E.EmployeeID, E.Name, COUNT(C.CourierID) AS DeliveredCount
FROM Employee E
LEFT JOIN Courier C ON E.EmployeeID = C.EmployeeID
WHERE C.Status = 'Delivered'
GROUP BY E.EmployeeID, E.Name;

SELECT C.CourierID, SUM(P.Amount) AS TotalPaid, S.Cost
FROM Courier C
JOIN Payment P ON C.CourierID = P.CourierID
JOIN CourierServices S ON C.CourierID = S.ServiceID
GROUP BY C.CourierID, S.Cost
HAVING SUM(P.Amount) > S.Cost;

-- 49. Find couriers that have a weight greater than the average weight of all couriers
SELECT *
FROM Courier
WHERE Weight > (SELECT AVG(Weight) FROM Courier);

-- 50. Find the names of all employees who have a salary greater than the average salary
SELECT Name, Salary
FROM Employee
WHERE Salary > (SELECT AVG(Salary) FROM Employee);

-- 51. Find the total cost of all courier services where the cost is less than the maximum cost
SELECT SUM(Cost) AS TotalCost
FROM CourierServices
WHERE Cost < (SELECT MAX(Cost) FROM CourierServices);

-- 52. Find all couriers that have been paid for
SELECT *
FROM Courier
WHERE CourierID IN (SELECT DISTINCT CourierID FROM Payment);

-- 53. Find the locations where the maximum payment amount was made
SELECT L.LocationID, L.LocationName
FROM Location L
JOIN Payment P ON L.LocationID = P.LocationID
WHERE P.Amount = (SELECT MAX(Amount) FROM Payment);

-- 54. Find all couriers whose weight is greater than the weight of all couriers sent by a specific sender (e.g., 'John Doe')
SELECT *
FROM Courier
WHERE Weight > ALL (
    SELECT Weight
    FROM Courier
    WHERE SenderName = 'John Doe'
);





