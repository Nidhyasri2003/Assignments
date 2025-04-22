using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.dao;
using TicketBookingSystem.entity;
using TicketBookingSystem.Exception;

namespace TicketBookingSystem
{
    public class MainModule
    {
        static BookingSystemServiceProviderImpl bookingSystem = new BookingSystemServiceProviderImpl();

        public static void Main(string[] args)
        {
            bool running = true;
            Console.WriteLine("Welcome to the Ticket Booking System");

            while (running)
            {
                Console.WriteLine("\n------------ MENU ------------");
                Console.WriteLine("1. Create Event");
                Console.WriteLine("2. View All Events");
                Console.WriteLine("3. Book Tickets");
                Console.WriteLine("4. Cancel Booking");
                Console.WriteLine("5. View Booking Details");
                Console.WriteLine("6. Available Tickets Summary");
                Console.WriteLine("7. Exit");
                Console.WriteLine("-----------------------------");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            CreateEvent();
                            break;
                        case "2":
                            ViewAllEvents();
                            break;
                        case "3":
                            BookTickets();
                            break;
                        case "4":
                            CancelBooking();
                            break;
                        case "5":
                            ViewBooking();
                            break;
                        case "6":
                            Console.WriteLine("Total Available Tickets: " + bookingSystem.GetAvailableNoOfTickets());
                            break;
                        case "7":
                            running = false;
                            Console.WriteLine("Exiting... Thank you!");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                catch (EventNotFoundException ex)
                {
                    Console.WriteLine("[Event Error] " + ex.Message);
                }
                catch (InvalidBookingIDException ex)
                {
                    Console.WriteLine("[Booking Error] " + ex.Message);
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine("[Null Pointer] " + ex.Message);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("[General Error] " + ex.Message);
                }
            }
        }

        static void CreateEvent()
        {
            Console.Write("Enter Event Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Event Type (Movie/Sports/Concert): ");
            string type = Console.ReadLine();
            Console.Write("Enter Date (yyyy-MM-dd): ");
            string date = Console.ReadLine();
            Console.Write("Enter Time (HH:mm): ");
            string time = Console.ReadLine();
            Console.Write("Enter Total Seats: ");
            int seats = int.Parse(Console.ReadLine());
            Console.Write("Enter Ticket Price: ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Venue Name: ");
            string venueName = Console.ReadLine();
            Console.Write("Enter Venue Address: ");
            string address = Console.ReadLine();

            Venue venue = new Venue(venueName, address);
            bookingSystem.CreateEvent(name, date, time, seats, price, type, venue);
        }

        static void ViewAllEvents()
        {
            List<Event> events = bookingSystem.GetEventDetails();
            if (events.Count == 0)
            {
                Console.WriteLine("No events created yet.");
                return;
            }

            foreach (var ev in events)
            {
                ev.DisplayEventDetails(); // Polymorphic call
            }
        }

        static void BookTickets()
        {
            Console.Write("Enter Event Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Number of Tickets: ");
            int numTickets = int.Parse(Console.ReadLine());

            List<Customer> customers = new List<Customer>();
            for (int i = 1; i <= numTickets; i++)
            {
                Console.WriteLine($"Enter details for Ticket {i}:");
                Console.Write("Name: ");
                string cname = Console.ReadLine();
                Console.Write("Email: ");
                string email = Console.ReadLine();
                Console.Write("Phone: ");
                string phone = Console.ReadLine();
                customers.Add(new Customer(cname, email, phone));
            }

            Booking booking = bookingSystem.BookTickets(name, numTickets, customers);
            booking.DisplayBookingDetails();
        }

        static void CancelBooking()
        {
            Console.Write("Enter Booking ID to cancel: ");
            int bookingId = int.Parse(Console.ReadLine());
            bookingSystem.CancelBooking(bookingId);
            Console.WriteLine("Booking cancelled successfully.");
        }

        static void ViewBooking()
        {
            Console.Write("Enter Booking ID to view: ");
            int bookingId = int.Parse(Console.ReadLine());
            Booking booking = bookingSystem.GetBookingDetails(bookingId);
            booking.DisplayBookingDetails();
        }
    }
}