CREATE DATABASE TicketBookingSystem;
GO

USE TicketBookingSystem;
GO

-- 2. Create Venu table
CREATE TABLE Venu (
    venue_id INT PRIMARY KEY IDENTITY(1,1),
    venue_name VARCHAR(100) NOT NULL,
    address VARCHAR(255) NOT NULL
);

-- 3. Create Booking table
CREATE TABLE Booking (
    booking_id INT PRIMARY KEY IDENTITY(1,1),
    customer_id INT NOT NULL,
    event_id INT NOT NULL,
    num_tickets INT CHECK (num_tickets > 0),
    total_cost DECIMAL(10,2) CHECK (total_cost >= 0),
    booking_date DATE NOT NULL
);

-- 4. Create Customer table
CREATE TABLE Customer (
    customer_id INT PRIMARY KEY IDENTITY(1,1),
    customer_name VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE,
    phone_number VARCHAR(15),
    booking_id INT FOREIGN KEY REFERENCES Booking(booking_id)
);

-- 5. Create Event table
CREATE TABLE Event (
    event_id INT PRIMARY KEY IDENTITY(1,1),
    event_name VARCHAR(100) NOT NULL,
    event_date DATE NOT NULL,
    event_time TIME NOT NULL,
    venue_id INT FOREIGN KEY REFERENCES Venu(venue_id),
    total_seats INT CHECK (total_seats > 0),
    available_seats INT CHECK (available_seats >= 0),
    ticket_price DECIMAL(10,2) CHECK (ticket_price >= 0),
    event_type VARCHAR(50) CHECK (event_type IN ('Movie', 'Sports', 'Concert')),
    booking_id INT FOREIGN KEY REFERENCES Booking(booking_id)
);

INSERT INTO Venu (venue_name, address) VALUES
('Sathyam Cinemas', 'Chennai'),
('Nehru Stadium', 'Chennai'),
('Music Academy', 'Chennai');

INSERT INTO Customer (customer_name, email, phone_number) VALUES
('Nidhyasri S R', 'nidhyasri@email.com', '9876543210'),
('Varun', 'Varun@email.com', '9123456789'),
('keerthi', 'keerthi@email.com', '9988776655');

INSERT INTO Event (event_name, event_date, event_time, venue_id, total_seats, available_seats, ticket_price, event_type) VALUES
('Avengers Movie', '2025-05-01', '18:00', 1, 100, 100, 250.00, 'Movie'),
('Chennai vs Mumbai Match', '2025-05-05', '19:00', 2, 500, 500, 750.00, 'Sports'),
('A R Rahman Live', '2025-05-10', '20:00', 3, 300, 300, 1200.00, 'Concert');

INSERT INTO Booking (customer_id, event_id, num_tickets, total_cost, booking_date) VALUES
(1, 1, 2, 2000.00, '2023-11-01'),
(2, 2, 3, 4500.00, '2023-11-05'),
(3, 3, 5, 12500.00, '2023-11-10');

INSERT INTO Venu (venue_name, address) VALUES
('Chennai Grand Hall', 'Mount Road, Chennai'),
('PVR Cinemas', 'Express Avenue, Chennai'),
('Chepauk Stadium', 'Marina Beach Road, Chennai'),
('Music Academy', 'T.T.K Road, Chennai'),
('Nehru Indoor Stadium', 'Periyamet, Chennai'),
('Sathyam Cinemas', 'Royapettah, Chennai'),
('Island Grounds', 'Anna Salai, Chennai'),
('Phoenix MarketCity Arena', 'Velachery, Chennai'),
('AVM Studios', 'Vadapalani, Chennai'),
('Vani Mahal', 'T Nagar, Chennai');

INSERT INTO Event (event_name, event_date, event_time, venue_id, total_seats, available_seats, ticket_price, event_type) VALUES
('Avengers Movie', '2023-12-01', '18:00', 2, 150, 130, 1000.00, 'Movie'),
('Chennai vs Mumbai Match', '2023-12-05', '19:30', 3, 500, 490, 1500.00, 'Sports'),
('A R Rahman Live', '2023-12-10', '20:00', 4, 200, 194, 2500.00, 'Concert'),
('Dance Fest 2023', '2023-12-15', '17:00', 1, 300, 299, 1000.00, 'Concert'),
('Vijay’s New Movie', '2023-12-18', '16:00', 6, 180, 180, 1200.00, 'Movie'),
('Super Singer Live', '2023-12-20', '18:30', 5, 350, 350, 2000.00, 'Concert'),
('Stand-up Comedy Night', '2023-12-22', '19:00', 7, 100, 100, 800.00, 'Concert'),
('F1 Expo', '2023-12-24', '11:00', 8, 250, 250, 1800.00, 'Sports'),
('New Year Bash', '2023-12-31', '21:00', 9, 400, 400, 3000.00, 'Concert'),
('Theatre Play: Hamlet', '2023-12-28', '17:30', 10, 120, 120, 900.00, 'Movie');

INSERT INTO Customer (customer_name, email, phone_number) VALUES
('jayasri', 'jaya@email.com', '9876543210'),
('Rahul Kumar', 'rahul.k@email.com', '9876500000'),
('Aarthi Raj', 'aarthi@email.com', '9000001234'),
('Sneha R', 'sneha@email.com', '9876501234'),
('Karan Das', 'karan@email.com', '9123456789'),
('Swetha Menon', 'swetha@email.com', '9898989898'),
('Ravi Shankar', 'ravi@email.com', '8080808080'),
('Priya Dharshini', 'priya@email.com', '7010101010'),
('Vikram Iyer', 'vikram@email.com', '9012345678'),
('Meena K', 'meena@email.com', '8123456789');

INSERT INTO Booking (customer_id, event_id, num_tickets, total_cost, booking_date) VALUES
(1, 1, 2, 2000.00, '2023-11-01'),
(2, 2, 3, 4500.00, '2023-11-05'),
(3, 3, 5, 12500.00, '2023-11-10'),
(4, 4, 1, 1000.00, '2023-11-15'),
(5, 5, 2, 2400.00, '2023-11-16'),
(6, 6, 4, 8000.00, '2023-11-17'),
(7, 7, 3, 2400.00, '2023-11-20'),
(8, 8, 5, 9000.00, '2023-11-22'),
(9, 9, 2, 6000.00, '2023-11-25'),
(10, 10, 1, 900.00, '2023-11-28');

--TASK 2--
-- 2. List all events
SELECT * FROM Event;

-- 3. Events with available tickets
SELECT * FROM Event WHERE available_seats > 0;

-- 4. Event names containing 'cup'
SELECT * FROM Event WHERE event_name LIKE '%cup%';

-- 5. Events with price between 1000 and 2500
SELECT * FROM Event WHERE ticket_price BETWEEN 1000 AND 2500;

-- 6. Events in date range
SELECT * FROM Event WHERE event_date BETWEEN '2023-12-01' AND '2023-12-20';

-- 7. Available concerts
SELECT * FROM Event WHERE available_seats > 0 AND event_type = 'Concert';

-- 8. 5 users starting from 6th
SELECT * FROM Customer ORDER BY customer_id OFFSET 5 ROWS FETCH NEXT 5 ROWS ONLY;


-- 9. Bookings with more than 4 tickets
SELECT * FROM Booking WHERE num_tickets > 4;

-- 10. Customers with phone ending in ‘000’
SELECT * FROM Customer WHERE phone_number LIKE '%000';

-- 11. Events with seat capacity > 15000 ordered
SELECT * FROM Event WHERE total_seats > 15000 ORDER BY total_seats DESC;

-- 12. Events not starting with x, y, z
SELECT * FROM Event WHERE event_name NOT LIKE 'x%' AND event_name NOT LIKE 'y%' AND event_name NOT LIKE 'z%';

-- TASK 3 --

-- 1. Events and Their Average Ticket Prices
SELECT event_name, AVG(ticket_price) AS avg_price
FROM Event
GROUP BY event_name;

-- 2. Total Revenue Generated by Events
SELECT event_name, SUM(total_cost) AS total_revenue
FROM Booking
JOIN Event ON Booking.event_id = Event.event_id
GROUP BY event_name;

-- 3. Event with Highest Ticket Sales
SELECT TOP 1 event_name, SUM(num_tickets) AS tickets_sold
FROM Booking
JOIN Event ON Booking.event_id = Event.event_id
GROUP BY event_name
ORDER BY tickets_sold DESC;

-- 4. Total Tickets Sold for Each Event
SELECT e.event_name, SUM(b.num_tickets) AS total_tickets
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY e.event_name;

-- 5. Events with No Ticket Sales
SELECT event_name
FROM Event
WHERE event_id NOT IN (SELECT DISTINCT event_id FROM Booking);

-- 6. User Who Has Booked the Most Tickets
SELECT TOP 1 c.customer_name, SUM(b.num_tickets) AS total_booked
FROM Booking b
JOIN Customer c ON b.customer_id = c.customer_id
GROUP BY c.customer_name
ORDER BY total_booked DESC;

-- 7. Events and tickets sold per month
SELECT DATENAME(MONTH, booking_date) AS month_name, e.event_name, SUM(b.num_tickets) AS tickets
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY DATENAME(MONTH, booking_date), e.event_name;

-- 8. Average Ticket Price by Venue
SELECT v.venue_name, AVG(e.ticket_price) AS avg_price
FROM Event e
JOIN Venu v ON e.venue_id = v.venue_id
GROUP BY v.venue_name;


-- 9. Total Tickets Sold by Event Type
SELECT event_type, SUM(b.num_tickets) AS total_tickets
FROM Booking b
JOIN Event e ON b.event_id = e.event_id
GROUP BY event_type;

-- 10. Revenue by Year
SELECT YEAR(booking_date) AS year, SUM(total_cost) AS yearly_revenue
FROM Booking
GROUP BY YEAR(booking_date);


-- 11. Users who booked multiple events
SELECT c.customer_name, COUNT(DISTINCT b.event_id) AS events_booked
FROM Customer c
JOIN Booking b ON c.customer_id = b.customer_id
GROUP BY c.customer_name
HAVING COUNT(DISTINCT b.event_id) > 1;


-- 12. Revenue Generated by Events for Each User
SELECT c.customer_name, SUM(b.total_cost) AS revenue
FROM Customer c
JOIN Booking b ON c.customer_id = b.customer_id
GROUP BY c.customer_name;

-- 13. Avg Ticket Price for Each Category and Venue
SELECT event_type, v.venue_name, AVG(e.ticket_price) AS avg_price
FROM Event e
JOIN Venu v ON e.venue_id = v.venue_id
GROUP BY event_type, v.venue_name;

-- 14. Users and Tickets Bought in Last 30 Days
SELECT c.customer_name, SUM(b.num_tickets) AS recent_tickets
FROM Customer c
JOIN Booking b ON c.customer_id = b.customer_id
WHERE booking_date >= DATEADD(DAY, -30, GETDATE())
GROUP BY c.customer_name;



-- TASK 4 --

-- 1. Avg Ticket Price per Venue (subquery)
SELECT venue_name, (
    SELECT AVG(ticket_price)
    FROM Event e
    WHERE e.venue_id = v.venue_id
) AS avg_price
FROM Venu v;

-- 2. Events with >50% Tickets Sold
SELECT event_name
FROM Event
WHERE (total_seats - available_seats) > (total_seats * 0.5);

-- 2. Events with >50% Tickets Sold
SELECT event_name
FROM Event
WHERE (total_seats - available_seats) > (total_seats * 0.5);

-- 4. Users Who Didn't Book Any Tickets (NOT EXISTS)
SELECT *
FROM Customer c
WHERE NOT EXISTS (
    SELECT 1
    FROM Booking b
    WHERE b.customer_id = c.customer_id
);

-- 5. Events with No Ticket Sales (NOT IN)
SELECT *
FROM Event
WHERE event_id NOT IN (
    SELECT DISTINCT event_id FROM Booking
);

-- 6. Tickets Sold by Event Type (FROM clause subquery)
SELECT event_type, SUM(total_tickets) AS total
FROM (
    SELECT e.event_type, b.num_tickets AS total_tickets
    FROM Booking b
    JOIN Event e ON b.event_id = e.event_id
) AS temp
GROUP BY event_type;

-- 7. Events with Price > Average Price (WHERE subquery)
SELECT *
FROM Event
WHERE ticket_price > (
    SELECT AVG(ticket_price) FROM Event
);

-- 8. Revenue per User (Correlated Subquery)
SELECT c.customer_name,
    (SELECT SUM(total_cost)
     FROM Booking b
     WHERE b.customer_id = c.customer_id) AS total_revenue
FROM Customer c;

-- 9. Users Who Booked in Given Venue (WHERE subquery)
SELECT *
FROM Customer
WHERE customer_id IN (
    SELECT b.customer_id
    FROM Booking b
    JOIN Event e ON b.event_id = e.event_id
    WHERE e.venue_id = 1  -- Example venue_id
);

-- 10. Tickets Sold per Event Type (GROUP BY in subquery)
SELECT event_type, SUM(tickets) AS total
FROM (
    SELECT e.event_type, b.num_tickets AS tickets
    FROM Booking b
    JOIN Event e ON b.event_id = e.event_id
) AS temp
GROUP BY event_type;

-- 11. Users Booked in Each Month (DATE_FORMAT with subquery)
SELECT DISTINCT c.customer_name
FROM Customer c
JOIN Booking b ON c.customer_id = b.customer_id
WHERE MONTH(b.booking_date) IN (
    SELECT DISTINCT MONTH(booking_date)
    FROM Booking
);

-- 12. Average Ticket Price per Venue (same as #1, again)
SELECT venue_name, (
    SELECT AVG(ticket_price)
    FROM Event e
    WHERE e.venue_id = v.venue_id
) AS avg_price
FROM Venu v;






























