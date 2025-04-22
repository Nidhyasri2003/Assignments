using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techshop.DAO;
using Techshop.Entities;
using Techshop.Exceptions;

namespace Techshop
{
    class Program
    {
        static void Main(string[] args)
        {
            OrderDAO orderDAO = new OrderDAO();
        ICustomerDAO customerDAO = new CustomerDAO();
        IProductDAO productDAO = new ProductDAO();

            // Menu for User
            while (true)
            {
                Console.WriteLine("\n=== TechShop Menu ===");
                Console.WriteLine("1. Register New Customer");
                Console.WriteLine("2. View All Customers");
                Console.WriteLine("3. Add New Product");
                Console.WriteLine("4. View All Products");
                Console.WriteLine("5. Place New Order");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Customer newCustomer = new Customer();
        Console.Write("Enter First Name: ");
                        newCustomer.FirstName = Console.ReadLine();
                        Console.Write("Enter Last Name: ");
                        newCustomer.LastName = Console.ReadLine();
                        Console.Write("Enter Email: ");
                        newCustomer.Email = Console.ReadLine();
                        Console.Write("Enter Phone: ");
                        newCustomer.Phone = Console.ReadLine();
                        Console.Write("Enter Address: ");
                        newCustomer.Address = Console.ReadLine();

                        try
                        {
                            customerDAO.AddCustomer(newCustomer);
                        }
                        catch (System.IO.InvalidDataException ex)
                        {
                            Console.WriteLine("❌ Error: " + ex.Message);
                        }
break;

                    case "2":
    var customers = customerDAO.GetAllCustomers();
    foreach (var c in customers)
    {
        c.GetCustomerDetails();
    }
    break;

case "3":
    Product newProduct = new Product();
    Console.Write("Enter Product Name: ");
    newProduct.ProductName = Console.ReadLine();
    Console.Write("Enter Description: ");
    newProduct.Description = Console.ReadLine();
    Console.Write("Enter Price: ");
    newProduct.Price = Convert.ToDecimal(Console.ReadLine());

    productDAO.AddProduct(newProduct);
    break;

case "4":
    var products = productDAO.GetAllProducts();
    foreach (var p in products)
    {
        p.GetProductDetails();
    }
    break;

case "5":
    try
    {
        // Create an order
        Order newOrder = new Order();
        Console.Write("Enter Customer ID: ");
        int custId = Convert.ToInt32(Console.ReadLine());
        newOrder.Customer = customerDAO.GetCustomerById(custId);
        newOrder.OrderDate = DateTime.Now;
        List<OrderDetail> orderDetails = new List<OrderDetail>();

        decimal total = 0;
        string more = "y";
        while (more.ToLower() == "y")
        {
            OrderDetail detail = new OrderDetail();
            Console.Write("Enter Product ID: ");
            int prodId = Convert.ToInt32(Console.ReadLine());
            detail.Product = productDAO.GetProductById(prodId);

            Console.Write("Enter Quantity: ");
            detail.Quantity = Convert.ToInt32(Console.ReadLine());

            total += detail.Product.Price * detail.Quantity;
            orderDetails.Add(detail);

            Console.Write("Add more products? (y/n): ");
            more = Console.ReadLine();
        }

        newOrder.TotalAmount = total;

        // Place the order
        orderDAO.PlaceOrder(newOrder, orderDetails);
    }
    catch (InsufficientStockException ex)
    {
        Console.WriteLine("❌ Stock error: " + ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine("❌ Something went wrong: " + ex.Message);
    }
    break;

case "0":
    Console.WriteLine("Exiting TechShop. Goodbye!");
    return;

default:
    Console.WriteLine("Invalid choice. Try again.");
    break;
}
            }
        }
    }
}