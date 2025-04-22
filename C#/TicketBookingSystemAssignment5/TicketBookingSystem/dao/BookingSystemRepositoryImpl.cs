using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.dao;
using TicketBookingSystem.entity;
using TicketBookingSystem.Exception;

namespace TicketBookingSystem.DAO
{
    public class BookingSystemRepositoryImpl : IBookingSystemRepository
    {
        //private string connStr = DBPropertyUtil.GetConnectionString("TicketDB");
        private string connStr = "Server=JAYASRI;Database=TicketBookingSystem;TrustServerCertificate=True;Integrated Security=True;";

        public Event CreateEvent(string eventName, string date, string time, int totalSeats, decimal ticketPrice, string eventType, Venue venue)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"INSERT INTO Event (event_name, event_date, event_time, venue_id, total_seats, available_seats, ticket_price, event_type) 
                                 VALUES (@event_name, @event_date, @event_time, @venue_id, @total_seats, @available_seats, @ticket_price, @event_type)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@event_name", eventName);
                cmd.Parameters.AddWithValue("@event_date", DateTime.Parse(date));
                cmd.Parameters.AddWithValue("@event_time", TimeSpan.Parse(time));
                cmd.Parameters.AddWithValue("@venue_id", 1); // Assuming venue already exists with ID 1
                cmd.Parameters.AddWithValue("@total_seats", totalSeats);
                cmd.Parameters.AddWithValue("@available_seats", totalSeats);
                cmd.Parameters.AddWithValue("@ticket_price", ticketPrice);
                cmd.Parameters.AddWithValue("@event_type", eventType);
                cmd.ExecuteNonQuery();

                return new Movie(eventName, DateTime.Parse(date), TimeSpan.Parse(time), venue, totalSeats, ticketPrice, "Genre", "Actor", "Actress");
            }
        }

        public List<Event> GetEventDetails()
        {
            List<Event> events = new List<Event>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM Event";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Process each event
                        string name = reader["event_name"].ToString();
                        DateTime date = Convert.ToDateTime(reader["event_date"]);
                        TimeSpan time = TimeSpan.Parse(reader["event_time"].ToString());
                        int totalSeats = Convert.ToInt32(reader["total_seats"]);
                        int availableSeats = Convert.ToInt32(reader["available_seats"]);
                        decimal price = Convert.ToDecimal(reader["ticket_price"]);
                        string type = reader["event_type"].ToString();

                        Venue dummyVenue = new Venue("VenueFromDB", "AddressFromDB");

                        Event value = null;

                        switch (type)
                        {
                            case "Movie":
                                value = new Movie(name, date, time, dummyVenue, totalSeats, price, "Genre", "Actor", "Actress");
                                break;
                            case "Concert":
                                value = new Concert(name, date, time, dummyVenue, totalSeats, price, "Artist", "Type");
                                break;
                            case "Sports":
                                value = new Sports(name, date, time, dummyVenue, totalSeats, price, "Sport", "TeamA vs TeamB");
                                break;
                        }

                        if (value != null)
                        {
                            value.AvailableSeats = availableSeats;
                            events.Add(value);
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                // Optional: handle exception or log
            }

            return events; // ✅ Always return a value
        }

        public int GetAvailableNoOfTickets()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT SUM(available_seats) FROM Event";
                SqlCommand cmd = new SqlCommand(query, conn);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public Booking BookTickets(string eventName, int numTickets, List<Customer> customers)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Get event info
                string getEventQuery = "SELECT * FROM Event WHERE event_name = @name";
                SqlCommand getEventCmd = new SqlCommand(getEventQuery, conn);
                getEventCmd.Parameters.AddWithValue("@name", eventName);
                SqlDataReader reader = getEventCmd.ExecuteReader();

                if (!reader.Read())
                    throw new EventNotFoundException("Event not found.");

                int eventId = (int)reader["event_id"];
                int availableSeats = (int)reader["available_seats"];
                decimal ticketPrice = (decimal)reader["ticket_price"];
                string eventType = reader["event_type"].ToString();
                reader.Close();

                if (availableSeats < numTickets)
                    throw new System.Exception("Not enough tickets available.");

                // Insert Booking
                string insertBookingQuery = @"INSERT INTO Booking (customer_id, event_id, num_tickets, total_cost, booking_date)
                                              OUTPUT INSERTED.booking_id
                                              VALUES (@customerId, @eventId, @tickets, @totalCost, @date)";

                // Insert Customer (assumes 1 customer for simplicity)
                var customer = customers[0];
                string insertCustomerQuery = @"INSERT INTO Customer (customer_name, email, phone_number)
                                               OUTPUT INSERTED.customer_id
                                               VALUES (@name, @email, @phone)";

                SqlCommand insertCustomerCmd = new SqlCommand(insertCustomerQuery, conn);
                insertCustomerCmd.Parameters.AddWithValue("@name", customer.CustomerName);
                insertCustomerCmd.Parameters.AddWithValue("@email", customer.Email);
                insertCustomerCmd.Parameters.AddWithValue("@phone", customer.PhoneNumber);
                int customerId = (int)insertCustomerCmd.ExecuteScalar();

                SqlCommand insertBookingCmd = new SqlCommand(insertBookingQuery, conn);
                insertBookingCmd.Parameters.AddWithValue("@customerId", customerId);
                insertBookingCmd.Parameters.AddWithValue("@eventId", eventId);
                insertBookingCmd.Parameters.AddWithValue("@tickets", numTickets);
                insertBookingCmd.Parameters.AddWithValue("@totalCost", numTickets * ticketPrice);
                insertBookingCmd.Parameters.AddWithValue("@date", DateTime.Now);
                int bookingId = (int)insertBookingCmd.ExecuteScalar();

                // Update available seats
                string updateEventQuery = "UPDATE Event SET available_seats = available_seats - @tickets WHERE event_id = @eventId";
                SqlCommand updateCmd = new SqlCommand(updateEventQuery, conn);
                updateCmd.Parameters.AddWithValue("@tickets", numTickets);
                updateCmd.Parameters.AddWithValue("@eventId", eventId);
                updateCmd.ExecuteNonQuery();

                return new Booking(customer, new Movie(eventName, DateTime.Now, TimeSpan.Zero, new Venue(), 0, ticketPrice, "", "", ""), numTickets)
                {
                    BookingId = bookingId
                };
            }
        }

        public void CancelBooking(int bookingId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Get booking info
                string selectQuery = "SELECT * FROM Booking WHERE booking_id = @id";
                SqlCommand selectCmd = new SqlCommand(selectQuery, conn);
                selectCmd.Parameters.AddWithValue("@id", bookingId);
                SqlDataReader reader = selectCmd.ExecuteReader();

                if (!reader.Read())
                    throw new InvalidBookingIDException("Booking ID does not exist.");

                int eventId = (int)reader["event_id"];
                int tickets = (int)reader["num_tickets"];
                reader.Close();

                // Delete booking
                string deleteBooking = "DELETE FROM Booking WHERE booking_id = @id";
                SqlCommand deleteCmd = new SqlCommand(deleteBooking, conn);
                deleteCmd.Parameters.AddWithValue("@id", bookingId);
                deleteCmd.ExecuteNonQuery();

                // Restore available seats
                string updateEvent = "UPDATE Event SET available_seats = available_seats + @tickets WHERE event_id = @eventId";
                SqlCommand updateCmd = new SqlCommand(updateEvent, conn);
                updateCmd.Parameters.AddWithValue("@tickets", tickets);
                updateCmd.Parameters.AddWithValue("@eventId", eventId);
                updateCmd.ExecuteNonQuery();
            }
        }

        public Booking GetBookingDetails(int bookingId)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"SELECT b.booking_id, c.customer_name, c.email, c.phone_number, e.event_name, b.num_tickets, b.total_cost, b.booking_date 
                                 FROM Booking b
                                 JOIN Customer c ON b.customer_id = c.customer_id
                                 JOIN Event e ON b.event_id = e.event_id
                                 WHERE b.booking_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", bookingId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.Read())
                    throw new InvalidBookingIDException("Booking not found.");

                var customer = new Customer(reader["customer_name"].ToString(), reader["email"].ToString(), reader["phone_number"].ToString());
                var eventObj = new Movie(reader["event_name"].ToString(), DateTime.Now, TimeSpan.Zero, new Venue(), 0, 0, "", "", "");
                var booking = new Booking(customer, eventObj, Convert.ToInt32(reader["num_tickets"]))
                {
                    BookingId = Convert.ToInt32(reader["booking_id"]),
                    TotalCost = Convert.ToDecimal(reader["total_cost"]),
                    BookingDate = Convert.ToDateTime(reader["booking_date"])
                };

                return booking;
            }
        }
    }
}
