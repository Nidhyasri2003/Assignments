CREATE DATABASE SISDB;
GO

CREATE TABLE Students (
    student_id INT PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    date_of_birth DATE NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    phone_number VARCHAR(15) UNIQUE NOT NULL
);

CREATE TABLE Teacher (
    teacher_id INT PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE Courses (
    course_id INT PRIMARY KEY,
    course_name VARCHAR(100) NOT NULL,
    credits INT NOT NULL CHECK (credits > 0),
    teacher_id INT,
    FOREIGN KEY (teacher_id) REFERENCES Teacher(teacher_id) ON DELETE SET NULL
);

CREATE TABLE Enrollments (
    enrollment_id INT PRIMARY KEY,
    student_id INT,
    course_id INT,
    enrollment_date DATE NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (student_id) REFERENCES Students(student_id) ON DELETE CASCADE,
    FOREIGN KEY (course_id) REFERENCES Courses(course_id) ON DELETE CASCADE
);

CREATE TABLE Payments (
    payment_id INT PRIMARY KEY,
    student_id INT,
    amount DECIMAL(10,2) NOT NULL CHECK (amount > 0),
    payment_date DATE NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (student_id) REFERENCES Students(student_id) ON DELETE CASCADE
);

INSERT INTO Students (student_id, first_name, last_name, date_of_birth, email, phone_number) VALUES
(1, 'Nidhya', 'S R','2003-10-29', 'Nid@email.com', '9876543210'),
(2, 'Keerthika', 'C','2003-05-22', 'Keer@email.com', '9988776655'),
(3, 'Priya', 'Narayanan','2003-10-31', 'Priya@email.com', '9871234567'),
(4, 'Varun', 'Kapoor', '2004-03-26','Varun.VK@email.com', '9876541230'),
(5, 'Mohmed', 'Shoaib', '2004-02-05','shobi.mohd@email.com', '9812345678'),
(6, 'Kirthana', 'B', '2004-01-06','Kirr@email.com', '9856743120'),
(7, 'Mathangi', 'Subramaniam', '2003-09-04','Maths@gmail.com', '9865432109'),
(8, 'Nithyashree', 'R', '2003-08-07','Nithya@email.com', '9876123456'),
(9, 'Swathi', 'Vijay', '2004-01-28','swathi.v@email.com', '9874321098'),
(10, 'Anuradha', 'Krishnan', '2004-12-03','anu.k@email.com', '9897654321');

INSERT INTO Teacher (teacher_id, first_name, last_name, email) VALUES
(1, 'Amit', 'Sharma', 'amit.sharma@example.com'),
(2, 'Priya', 'Iyer', 'priya.iyer@example.com'),
(3, 'Rahul', 'Verma', 'rahul.verma@example.com'),
(4, 'Sneha', 'Reddy', 'sneha.reddy@example.com'),
(5, 'Vikram', 'Patel', 'vikram.patel@example.com'),
(6, 'Anjali', 'Nair', 'anjali.nair@example.com'),
(7, 'Suresh', 'Yadav', 'suresh.yadav@example.com'),
(8, 'Meera', 'Choudhary', 'meera.choudhary@example.com'),
(9, 'Arjun', 'Bose', 'arjun.bose@example.com'),
(10, 'Divya', 'Rajput', 'divya.rajput@example.com');
GO

INSERT INTO Courses (course_id, course_name, credits, teacher_id) VALUES
(1, 'Engineering Mathematics', 3, 1),
(2, 'Engineering Physics', 4, 2),
(3, 'Object oriented pragramming', 4, 3),
(4, 'Artificial Intelligence', 3, 4),
(5, 'DBMS', 5, 5),
(6, 'Devops', 3, 6),
(7, 'Internet of Things', 3, 7),
(8, 'Computer Networks', 3, 8),
(9, 'Psychology', 4, 9),
(10, 'Java', 3, 10);

INSERT INTO Enrollments (enrollment_id, student_id, course_id, enrollment_date) VALUES
(1, 1, 5, '2024-03-10'),
(2, 2, 3, '2024-03-12'),
(3, 3, 1, '2024-03-15'),
(4, 4, 6, '2024-03-18'),
(5, 5, 2, '2024-03-20'),
(6, 6, 4, '2024-03-22'),
(7, 7, 9, '2024-03-25'),
(8, 8, 10, '2024-03-27'),
(9, 9, 7, '2024-03-30'),
(10, 10, 8, '2024-03-31');

INSERT INTO Payments (payment_id, student_id, amount, payment_date) VALUES
(1, 1, 5000.00, '2024-03-05'),
(2, 2, 4000.00, '2024-03-07'),
(3, 3, 4500.00, '2024-03-10'),
(4, 4, 3000.00, '2024-03-12'),
(5, 5, 6000.00, '2024-03-15'),
(6, 6, 3500.00, '2024-03-18'),
(7, 7, 5000.00, '2024-03-20'),
(8, 8, 4000.00, '2024-03-22'),
(9, 9, 4500.00, '2024-03-25'),
(10, 10, 3000.00, '2024-03-27');

--Task 2
INSERT INTO Students (student_id, first_name, last_name, date_of_birth, email, phone_number)  
VALUES (11, 'John', 'Doe', '1995-08-15', 'john.doe@example.com', '1234567890');  
GO

INSERT INTO Enrollments (enrollment_id, student_id, course_id, enrollment_date)  
VALUES (11, 1, 10, '2025-03-21');  

UPDATE Teacher  
SET email = 'rahul@gmail.com'  
WHERE teacher_id = 3;  

DELETE FROM Enrollments  
WHERE student_id = 1 AND course_id = 10;  

UPDATE Courses  
SET teacher_id = 3  
WHERE course_id = 10;

DELETE FROM Enrollments  
WHERE student_id = 1;  

UPDATE Payments  
SET amount = 7500  
WHERE payment_id = 5;  

--Task 3
SELECT s.student_id, s.first_name, s.last_name, SUM(p.amount) AS total_payments
FROM Students s
JOIN Payments p ON s.student_id = p.student_id
WHERE s.student_id = 1  
GROUP BY s.student_id, s.first_name, s.last_name;

SELECT c.course_id, c.course_name, COUNT(e.student_id) AS enrolled_students
FROM Courses c
LEFT JOIN Enrollments e ON c.course_id = e.course_id
GROUP BY c.course_id, c.course_name
ORDER BY enrolled_students DESC;

SELECT s.student_id, s.first_name, s.last_name
FROM Students s
LEFT JOIN Enrollments e ON s.student_id = e.student_id
WHERE e.student_id IS NULL;


SELECT s.first_name, s.last_name, c.course_name
FROM Students s
JOIN Enrollments e ON s.student_id = e.student_id
JOIN Courses c ON e.course_id = c.course_id
ORDER BY s.last_name, s.first_name;

SELECT t.first_name, t.last_name, c.course_name
FROM Teacher t
JOIN Courses c ON t.teacher_id = c.teacher_id
ORDER BY t.last_name, t.first_name;


SELECT s.student_id, s.first_name, s.last_name, e.enrollment_date
FROM Students s
JOIN Enrollments e ON s.student_id = e.student_id
JOIN Courses c ON e.course_id = c.course_id
WHERE c.course_id = 10;  

SELECT s.student_id, s.first_name, s.last_name
FROM Students s
LEFT JOIN Payments p ON s.student_id = p.student_id
WHERE p.student_id IS NULL;

SELECT c.course_id, c.course_name
FROM Courses c
LEFT JOIN Enrollments e ON c.course_id = e.course_id
WHERE e.course_id IS NULL;


SELECT e.student_id, s.first_name, s.last_name, COUNT(e.course_id) AS course_count
FROM Enrollments e
JOIN Students s ON e.student_id = s.student_id
GROUP BY e.student_id, s.first_name, s.last_name
HAVING COUNT(e.course_id) > 1
ORDER BY course_count DESC;

SELECT t.teacher_id, t.first_name, t.last_name
FROM Teacher t
LEFT JOIN Courses c ON t.teacher_id = c.teacher_id
WHERE c.course_id IS NULL;

--Task 4

SELECT AVG(student_count) AS avg_students_per_course
FROM (
    SELECT course_id, COUNT(student_id) AS student_count
    FROM Enrollments
    GROUP BY course_id
) AS course_enrollment;


SELECT s.student_id, s.first_name, s.last_name, p.amount
FROM Students s
JOIN Payments p ON s.student_id = p.student_id
WHERE p.amount = (SELECT MAX(amount) FROM Payments);


SELECT c.course_id, c.course_name, enroll_count
FROM Courses c
JOIN (
    SELECT course_id, COUNT(student_id) AS enroll_count
    FROM Enrollments
    GROUP BY course_id
) AS course_enrollment ON c.course_id = course_enrollment.course_id
WHERE enroll_count = (
    SELECT MAX(enroll_count)
    FROM (
        SELECT COUNT(student_id) AS enroll_count
        FROM Enrollments
        GROUP BY course_id
    ) AS max_enrollments
);

SELECT t.teacher_id, t.first_name, t.last_name, 
       (SELECT SUM(p.amount)
        FROM Payments p
        JOIN Enrollments e ON p.student_id = e.student_id
        JOIN Courses c ON e.course_id = c.course_id
        WHERE c.teacher_id = t.teacher_id) AS total_payments
FROM Teacher t;


SELECT s.student_id, s.first_name, s.last_name
FROM Students s
WHERE (SELECT COUNT(course_id) FROM Enrollments WHERE student_id = s.student_id) = 
      (SELECT COUNT(course_id) FROM Courses);


SELECT first_name, last_name 
FROM Teacher 
WHERE teacher_id NOT IN (SELECT DISTINCT teacher_id FROM Courses);

SELECT AVG(DATEDIFF(YEAR, date_of_birth, GETDATE())) AS avg_age 
FROM Students;


SELECT course_name 
FROM Courses 
WHERE course_id NOT IN (SELECT DISTINCT course_id FROM Enrollments);

SELECT s.student_id, s.first_name, s.last_name, c.course_name, 
       (SELECT SUM(p.amount) 
        FROM Payments p 
        JOIN Enrollments e ON p.student_id = e.student_id
        WHERE e.student_id = s.student_id AND e.course_id = c.course_id) AS total_payment
FROM Students s
JOIN Enrollments e ON s.student_id = e.student_id
JOIN Courses c ON e.course_id = c.course_id;

SELECT student_id, first_name, last_name 
FROM Students 
WHERE student_id IN (
    SELECT student_id 
    FROM Payments 
    GROUP BY student_id 
    HAVING COUNT(payment_id) > 1
);

SELECT s.student_id, s.first_name, s.last_name, COALESCE(SUM(p.amount), 0) AS total_payment 
FROM Students s
LEFT JOIN Payments p ON s.student_id = p.student_id
GROUP BY s.student_id, s.first_name, s.last_name;


SELECT c.course_name, COUNT(e.student_id) AS enrolled_students 
FROM Courses c
LEFT JOIN Enrollments e ON c.course_id = e.course_id
GROUP BY c.course_name;


SELECT s.student_id, s.first_name, s.last_name, AVG(p.amount) AS avg_payment
FROM Students s
JOIN Payments p ON s.student_id = p.student_id
GROUP BY s.student_id, s.first_name, s.last_name;

























