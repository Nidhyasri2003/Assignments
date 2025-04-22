CREATE DATABASE HMBank;
USE HMBank;

CREATE TABLE Customers (
    customer_id INT IDENTITY(1,1) PRIMARY KEY,
    first_name NVARCHAR(50) NOT NULL,
    last_name NVARCHAR(50) NOT NULL,
    DOB DATE NOT NULL,
    email NVARCHAR(100) UNIQUE NOT NULL,
    phone_number NVARCHAR(15) UNIQUE NOT NULL,
    address NVARCHAR(255) NOT NULL
);


CREATE TABLE Accounts (
    account_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    account_type VARCHAR(20) CHECK (account_type IN ('savings', 'current', 'zero_balance')) NOT NULL,
    balance DECIMAL(15,2) NOT NULL DEFAULT 0.00,
    CONSTRAINT FK_Customer_Account FOREIGN KEY (customer_id) REFERENCES Customers(customer_id) ON DELETE CASCADE
);


CREATE TABLE Transactions (
    transaction_id INT IDENTITY(1,1) PRIMARY KEY,
    account_id INT NOT NULL,
    transaction_type VARCHAR(20) CHECK (transaction_type IN ('deposit', 'withdrawal', 'transfer')) NOT NULL,
    amount DECIMAL(15,2) NOT NULL CHECK (amount > 0),
    transaction_date DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Account_Transaction FOREIGN KEY (account_id) REFERENCES Accounts(account_id) ON DELETE CASCADE
);


INSERT INTO Customers ( first_name, last_name, DOB,  email, phone_number, address) VALUES
('Nidhya', 'S R','2003-10-29', 'Nid@email.com', '9876543210', 'Bangalore'),
('Keerthika', 'C','2003-05-22', 'Keer@email.com', '9988776655', 'Pune'),
('Priya', 'Narayanan','2003-10-31', 'Priya@email.com', '9871234567', 'Mysore'),
('Varun', 'Kapoor', '2004-03-26','Varun.VK@email.com', '9876541230', 'Ujjain'),
('Mohmed', 'Shoaib', '2004-02-05','shobi.mohd@email.com', '9812345678' ,'chennai'),
('Kirthana', 'B', '2004-01-06','Kirr@email.com', '9856743120', 'coimbattur'),
('Mathangi', 'Subramaniam', '2003-09-04','Maths@gmail.com', '9865432109','kochi'),
('Nithyashree', 'R', '2003-08-07','Nithya@email.com', '9876123456', 'surat'),
('Swathi', 'Vijay', '2004-01-28','swathi.v@email.com', '9874321098', 'hyderabad'),
('Anuradha', 'Krishnan', '2004-12-03','anu.k@email.com', '9897654321', 'Gujarat');

INSERT INTO Accounts (customer_id, account_type, balance) VALUES
(1, 'savings', 5000.00),
(2, 'current', 12000.50),
(3, 'savings', 3000.75),
(4, 'zero_balance', 0.00),
(5, 'current', 8000.25),
(6, 'savings', 15000.00),
(7, 'zero_balance', 0.00),
(8, 'savings', 2000.00),
(9, 'current', 9500.00),
(10, 'savings', 7000.80);

INSERT INTO Transactions (account_id, transaction_type, amount, transaction_date) VALUES
(1, 'deposit', 2000.00, '2024-03-01 10:15:00'),
(2, 'withdrawal', 500.00, '2024-03-02 12:30:00'),
(3, 'deposit', 1000.00, '2024-03-03 14:45:00'),
(4, 'deposit', 500.00, '2024-03-04 09:00:00'),
(5, 'transfer', 2000.00, '2024-03-05 16:20:00'),
(6, 'withdrawal', 1500.00, '2024-03-06 18:05:00'),
(7, 'deposit', 3000.00, '2024-03-07 08:30:00'),
(8, 'transfer', 1200.00, '2024-03-08 14:10:00'),
(9, 'deposit', 2500.00, '2024-03-09 11:55:00'),
(10, 'withdrawal', 800.00, '2024-03-10 17:40:00');

--Write a SQL query to retrieve the name, account type and email of all customers.   

SELECT 
    c.first_name, 
    c.last_name, 
    a.account_type, 
    c.email 
FROM Customers c
JOIN Accounts a ON c.customer_id = a.customer_id;

--Write a SQL query to list all transaction corresponding customer. 
SELECT 
    c.first_name, 
    c.last_name, 
    a.account_id, 
    t.transaction_id, 
    t.transaction_type, 
    t.amount, 
    t.transaction_date 
FROM Customers c
JOIN Accounts a ON c.customer_id = a.customer_id
JOIN Transactions t ON a.account_id = t.account_id
ORDER BY c.customer_id, t.transaction_date;

--Write a SQL query to increase the balance of a specific account by a certain amount. 
UPDATE Accounts
SET balance = balance + 1000 
WHERE account_id = 3; 

--Write a SQL query to Combine first and last names of customers as a full_name.
SELECT 
    customer_id, 
    CONCAT(first_name, ' ', last_name) AS full_name 
FROM Customers;

-- SQL query to remove accounts with a balance of zero where the account type is savings.

DELETE FROM Accounts
WHERE balance = 0 AND account_type = 'savings';

--Write a SQL query to Find customers living in a specific city. 
SELECT * 
FROM Customers 
WHERE address LIKE '%pune%'; 


-- Write a SQL query to Get the account balance for a specific account. 
SELECT account_id, balance 
FROM Accounts 
WHERE account_id = 3; 



--Write a SQL query to List all current accounts with a balance greater than $1,000.
SELECT * 
FROM Accounts 
WHERE account_type = 'current' 
AND balance > 1000;

--Write a SQL query to Retrieve all transactions for a specific account.
SELECT transaction_id, account_id, transaction_type, amount, transaction_date  
FROM Transactions  
WHERE account_id = 3  
ORDER BY transaction_date DESC;


 --Write a SQL query to Calculate the interest accrued on savings accounts based on a given interest rate. 
SELECT account_id, balance, 
       (balance * 0.05) AS interest_accrued  
FROM Accounts
WHERE account_type = 'savings';

-- SQL query to Identify accounts where the balance is less than a specified overdraft limit.
SELECT account_id, customer_id, balance
FROM Accounts
WHERE balance < -100



--Write a SQL query to Find customers not living in a specific city.
SELECT customer_id, first_name, last_name, address
FROM Customers  
WHERE address NOT LIKE '%' + 'chennai' + '%';

--task 3
-- 1. Find the average account balance for all customers
SELECT AVG(balance) AS average_balance
FROM Accounts;

-- 2. Retrieve the top 10 highest account balances
SELECT TOP 10 account_id, customer_id, balance
FROM Accounts
ORDER BY balance DESC;

-- 3. Calculate Total Deposits for All Customers on a specific date
SELECT SUM(amount) AS total_deposits
FROM Transactions
WHERE transaction_type = 'deposit'
  AND CAST(transaction_date AS DATE) = '2025-04-01';

-- 4. Find the Oldest and Newest Customers Oldest customer
SELECT TOP 1 * 
FROM Customers 
ORDER BY DOB;
-- Newest customer
SELECT TOP 1 * 
FROM Customers 
ORDER BY DOB DESC;

-- 5. Retrieve transaction details along with the account type
SELECT t.transaction_id, t.transaction_type, t.amount, t.transaction_date, a.account_type
FROM Transactions t
JOIN Accounts a ON t.account_id = a.account_id;

-- 6. Get a list of customers along with their account details
SELECT c.customer_id, c.first_name, c.last_name, a.account_id, a.account_type, a.balance
FROM Customers c
JOIN Accounts a ON c.customer_id = a.customer_id;

-- 7. Retrieve transaction details along with customer information for a specific account
SELECT t.*, c.first_name, c.last_name, c.email
FROM Transactions t
JOIN Accounts a ON t.account_id = a.account_id
JOIN Customers c ON a.customer_id = c.customer_id
WHERE t.account_id = 1;  

-- 8. Identify customers who have more than one account
SELECT customer_id, COUNT(*) AS account_count
FROM Accounts
GROUP BY customer_id
HAVING COUNT(*) > 1;

-- 9. Calculate the difference in transaction amounts between deposits and withdrawals
SELECT
    SUM(CASE WHEN transaction_type = 'deposit' THEN amount ELSE 0 END) -
    SUM(CASE WHEN transaction_type = 'withdrawal' THEN amount ELSE 0 END) AS balance_difference
FROM Transactions;

--10
SELECT account_id,
       AVG(balance) AS avg_daily_balance
FROM (
    SELECT account_id, CAST(transaction_date AS DATE) AS day,
           SUM(amount) OVER (PARTITION BY account_id, CAST(transaction_date AS DATE)) AS balance
    FROM Transactions
    WHERE transaction_date BETWEEN '2025-03-01' AND '2025-03-31'
) AS DailyBalance
GROUP BY account_id;


-- 11. Calculate the total balance for each account type
SELECT account_type, SUM(balance) AS total_balance
FROM Accounts
GROUP BY account_type;

-- 12. Identify accounts with the highest number of transactions (order by descending)
SELECT account_id, COUNT(*) AS transaction_count
FROM Transactions
GROUP BY account_id
ORDER BY transaction_count DESC;

-- 13. List customers with high aggregate account balances, along with their account types
SELECT c.customer_id, c.first_name, c.last_name, a.account_type, SUM(a.balance) AS total_balance
FROM Customers c
JOIN Accounts a ON c.customer_id = a.customer_id
GROUP BY c.customer_id, c.first_name, c.last_name, a.account_type
HAVING SUM(a.balance) > 1000;

-- 14. Identify and list duplicate transactions based on transaction amount, date, and account
SELECT account_id, amount, transaction_date, COUNT(*) AS duplicate_count
FROM Transactions
GROUP BY account_id, amount, transaction_date
HAVING COUNT(*) > 0;

--task 4
-- 1. Retrieve the customer(s) with the highest account balance
SELECT c.*
FROM Customers c
JOIN Accounts a ON c.customer_id = a.customer_id
WHERE a.balance = (SELECT MAX(balance) FROM Accounts);

-- 2. Calculate the average account balance for customers who have more than one account
SELECT AVG(balance) AS avg_balance
FROM Accounts
WHERE customer_id IN (
    SELECT customer_id
    FROM Accounts
    GROUP BY customer_id
    HAVING COUNT(account_id) > 1
);

-- 3. Retrieve accounts with transactions whose amounts exceed the average transaction amount
SELECT DISTINCT a.account_id, a.account_type, t.amount
FROM Accounts a
JOIN Transactions t ON a.account_id = t.account_id
WHERE t.amount > (SELECT AVG(amount) FROM Transactions);

-- 4. Identify customers who have no recorded transactions
SELECT * 
FROM Customers c
WHERE NOT EXISTS (
    SELECT 1
    FROM Accounts a
    JOIN Transactions t ON a.account_id = t.account_id
    WHERE a.customer_id = c.customer_id
);

-- 5. Calculate the total balance of accounts with no recorded transactions
SELECT SUM(balance) AS total_balance_without_transactions
FROM Accounts a
WHERE NOT EXISTS (
    SELECT 1
    FROM Transactions t
    WHERE t.account_id = a.account_id
);

-- 6. Retrieve transactions for accounts with the lowest balance
SELECT *
FROM Transactions
WHERE account_id IN (
    SELECT account_id
    FROM Accounts
    WHERE balance = (SELECT MIN(balance) FROM Accounts)
);

-- 7. Identify customers who have accounts of multiple types
SELECT customer_id
FROM Accounts
GROUP BY customer_id
HAVING COUNT(DISTINCT account_type) > 1;

-- 8. Calculate the percentage of each account type out of the total number of accounts
SELECT account_type,
       COUNT(*) * 100.0 / (SELECT COUNT(*) FROM Accounts) AS percentage_of_total
FROM Accounts
GROUP BY account_type;


-- 9. Retrieve all transactions for a customer with a given customer_id
SELECT t.*
FROM Transactions t
WHERE t.account_id IN (
    SELECT account_id
    FROM Accounts
    WHERE customer_id = 101  -- Replace with desired customer_id
);

-- 10. Calculate the total balance for each account type, including a subquery within the SELECT clause
SELECT account_type,
       (SELECT SUM(balance) FROM Accounts a2 WHERE a2.account_type = a1.account_type) AS total_balance
FROM Accounts a1
GROUP BY account_type;
