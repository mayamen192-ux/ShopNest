using ShopNest;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace ShopNest
{


    internal class Program
    {
        //global storage
        static Store store = new Store("ShopNest Store");


        //helping functions
        static public void displayMenue()
        {
            Console.WriteLine("========== ShopNest E-Commerce System ==========");
            Console.WriteLine("1. Add Physical Product");
            Console.WriteLine("2. Add Digital Product");
            Console.WriteLine("3. Display All Products");
            Console.WriteLine("4. Register Customer");
            Console.WriteLine("5. Place Order");
            Console.WriteLine("6. Cancel Order");
            Console.WriteLine("7. View Customer Order History");
            Console.WriteLine("8. Show Store Statistics");
            Console.WriteLine("0. Exit");
      
        }
        // Seed Data Method
        static public void SeedData()
        {
            // ===== Add Products =====

            // Physical Products
            store.AddPhysicalProduct("Laptop", 450.5, 2.5, 3);
            store.AddPhysicalProduct("Smartphone", 300, 0.5, 2);
            store.AddPhysicalProduct("Gaming Chair", 150, 12, 1.5);

            // Digital Products
            store.AddDigitalProduct("C# Programming Course", 50, 1200, "www.shopnest.com/csharp-course");
            store.AddDigitalProduct("Antivirus Software", 35, 500, "www.shopnest.com/antivirus");
            store.AddDigitalProduct("E-Book Bundle", 20, 250, "www.shopnest.com/ebooks");

            // ===== Register Customers =====
            store.RegisterCustomer("Ali Ahmed", "ali@gmail.com");
            store.RegisterCustomer("Sara Mohammed", "sara@gmail.com");
            store.RegisterCustomer("Ahmed Naser", "ahmed@gmail.com");

            // ===== Place Orders =====

            // Product IDs start from 100
            store.PlaceOrder("ali@gmail.com", 100);
            store.PlaceOrder("ali@gmail.com", 103);

            store.PlaceOrder("sara@gmail.com", 101);

            store.PlaceOrder("ahmed@gmail.com", 102);
            store.PlaceOrder("ahmed@gmail.com", 104);
        }
        static public void AddPhysicalProductMenu()
        {
            Console.WriteLine("=== Add Physical Product ===");

            // Name
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            // Price
            Console.Write("Enter price: ");
            if (!double.TryParse(Console.ReadLine(), out double price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            // Weight
            Console.Write("Enter weight (kg): ");
            if (!double.TryParse(Console.ReadLine(), out double weight))
            {
                Console.WriteLine("Invalid weight.");
                return;
            }

            // Shipping cost
            Console.Write("Enter shipping cost per kg: ");
            if (!double.TryParse(Console.ReadLine(), out double shipping))
            {
                Console.WriteLine("Invalid shipping cost.");
                return;
            }

            // Call Store method
            store.AddPhysicalProduct(name, price, weight, shipping);
        }
        static public void AddDigitalProductMenu()
        {
            Console.WriteLine("=== Add Digital Product ===");

            // Name
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            // Price
            Console.Write("Enter price: ");
            if (!double.TryParse(Console.ReadLine(), out double price))
            {
                Console.WriteLine("Invalid price.");
                return;
            }

            // File size
            Console.Write("Enter file size (MB): ");
            if (!double.TryParse(Console.ReadLine(), out double fileSize))
            {
                Console.WriteLine("Invalid file size.");
                return;
            }

            // Download link
            Console.Write("Enter download link: ");
            string link = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(link))
            {
                Console.WriteLine("Download link cannot be empty.");
                return;
            }

            // Call Store method
            store.AddDigitalProduct(name, price, fileSize, link);
        }
        static public void RegisterCustomerMenu()
        {
            Console.WriteLine("=== Register Customer ===");

            // Full Name
            Console.Write("Enter full name: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return;
            }

            // Email
            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty.");
                return;
            }

            // Call Store method (handles duplicate check)
            store.RegisterCustomer(name, email);
        }
        static public void PlaceOrderMenu()
        {
            Console.WriteLine("=== Place Order ===");

            // Email
            Console.Write("Enter customer email: ");
            string email = Console.ReadLine();

            //IsNullOrWhiteSpace() is a built-in C# method used to check whether a string is empty, null, or only spaces.
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty.");
                return;
            }

            // Product ID
            Console.Write("Enter product ID: ");
            if (!int.TryParse(Console.ReadLine(), out int productID))
            {
                Console.WriteLine("Invalid product ID.");
                return;
            }

            // Call Store method
            store.PlaceOrder(email, productID);
        }
        static public void DisplayAllProductsMenu()
        {
            store.DisplayAllProducts();
        }
        static public void DisplayCustomerOrderHistoryMenu()
        {
            Console.WriteLine("=== Customer Order History ===");

            // Ask for email
            Console.Write("Enter customer email: ");
            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty.");
                return;
            }

            // Call Store method
            store.DisplayCustomerOrders(email);
        }

        static public void CancelOrderMenu()
        {
            Console.WriteLine("=== Cancel Order ===");

            // Ask for Order ID
            Console.Write("Enter order ID: ");

            if (!int.TryParse(Console.ReadLine(), out int orderID))
            {
                Console.WriteLine("Invalid order ID.");
                return;
            }

            // Call Store method
            store.CancelOrder(orderID);
        }


        public void DisplayStatistics()
        {
            store.DisplayStatistics();
        }
        static public bool ConfirmExit()
        {
            Console.WriteLine("Are you sure you want to exit? (yes/no)");
            string confirm = Console.ReadLine()?.Trim().ToLower();

            if (confirm == "yes")
            {
                Console.WriteLine("Exiting system...");
                Console.WriteLine("Thank you for using the Healthcare Management System!");
                Console.WriteLine("----------------------------------------");
                // user confirmed exit
                return true;
            }
            else
            {
                Console.WriteLine("Exit cancelled. Returning to menu...");
                // user canceled exit
                return false;
            }

        }
        static void Main(string[] args)
        {
            SeedData();
            Console.Clear();

            bool exit = false;

            while (!exit)
            {
                displayMenue();

                int option;

                Console.Write("Choose option: ");

                // safe input handling
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid input. Please enter a number from 0 to 8.");
                    continue;
                }

                //oprtaional functions
                switch (option)
                {
                    case 1://Add Physical Product Menu operation
                        AddPhysicalProductMenu();
                        break;
                    case 2:// Add Digital Product Menu operation
                        AddDigitalProductMenu();
                        break;
                    case 3://Display All Products Menu operation
                        DisplayAllProductsMenu();
                        break;
                        case 4://Register Customer Menu operation
                        RegisterCustomerMenu();               
                        break;
                    case 5://Place Order Menu operation
                        PlaceOrderMenu();
                        break;
                    case 6://Cancel Order Menu operation
                        CancelOrderMenu();
                        break;
                    case 7://Display Customer Order History Menu operation
                        DisplayCustomerOrderHistoryMenu();  
                        break;
                    case 8://Display Statistics operation
                        store.DisplayStatistics();
                        break;
                    case 0://Exit operation
                        if (ConfirmExit())
                        {
                            exit = true;
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please choose between 0 and 8.");
                        break;

                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();


            }
        }
    }
}






/////base Classes///////////////////////

//Product class
abstract class Product
    {
        // Static Fields (shared by all products)
        private static int nextProductID = 100;
        private static int totalProductsCreated = 0;

        // Protected Fields (accessible in child classes)
        protected string name;
        protected double price;

        // Instance Field (each object gets its own ID)
        private int productID;

        // Properties
        public int ProductID
        {
            get { return productID; }
        }

        public string Name
        {
            get { return name; }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value > 0)
                {
                    price = value;
                }
                else
                {
                    Console.WriteLine("Error: Price must be greater than zero.");
                }
            }
        }

        // Constructor
        public Product(string name, double price)
        {
            this.name = name;

            // uses validation logic
            Price = price;

            // assign unique ID correctly
            productID = nextProductID++;

            // track total products created
            totalProductsCreated++;
        }


        public static int GetTotalProducts()
        {
            return totalProductsCreated;
        }
        public abstract void DisplayInfo();
        public static int GetTotalProductsCreated()
        {
            return totalProductsCreated;
        }
        public virtual double CalculateTotalCost()
        {
            return price;
        }
    }
    //User Class
    abstract class User //abstraction:Hide Complexity
    {
        private static int totalUsersCreated;
        protected string fullName;
        protected string email;

        //Properties:
        public string FullName { get { return fullName; } }
        public string Email
        {
            get { return email; }
        }

        // Constructor
        public User(string fullName, string email)
        {
            this.fullName = fullName;
            this.email = email;

            totalUsersCreated++;
        }
        public static int GetTotalUsersCreated()
        {
            return totalUsersCreated;
        }

        // Abstract Method (must be overridden)
        public abstract void DisplayInfo();
    }

//////////////Derived Classes///////////////////////////

//PhysicalProduct
class PhysicalProduct : Product
{
    private double weightKg;
    private double shippingCostPerKg;

    // Properties
    public double WeightKg { get { return weightKg; } }

    // Constructor
    public PhysicalProduct(string name, double price, double weightKg, double shippingCostPerKg)
        : base(name, price)
    {
        this.weightKg = weightKg;
        this.shippingCostPerKg = shippingCostPerKg;
    }

    // Override CalculateTotalCost
    public override double CalculateTotalCost()
    {
        return price + (weightKg * shippingCostPerKg);
    }

    // Override DisplayInfo
    public override void DisplayInfo()
    {
        Console.WriteLine("[Physical Product]");
        Console.WriteLine("Product ID: " + ProductID);
        Console.WriteLine("Name:       " + Name);
        Console.WriteLine("Price:      " + Price);
        Console.WriteLine("Weight:     " + weightKg + " kg");
        Console.WriteLine("Shipping Cost per kg: " + shippingCostPerKg);
        Console.WriteLine("Total Cost: " + CalculateTotalCost());
    }
}

//DigitalProduct class
class DigitalProduct : Product
{
    // Private Fields
    private double fileSizeMB;
    private string downloadLink;

    // Constructor
    public DigitalProduct(string name, double price, double fileSizeMB, string downloadLink) : base(name, price)
    {
        this.fileSizeMB = fileSizeMB;
        this.downloadLink = downloadLink;
    }

    // Overridden Method
    public override void DisplayInfo()
    {
        Console.WriteLine("[Digital Product]");
        Console.WriteLine("Product ID  :"+ ProductID);
        Console.WriteLine("Name        :"+ Name);
        Console.WriteLine("Price       :"+ Price);
        Console.WriteLine("File Size   : "+fileSizeMB+" MB");
        Console.WriteLine("Download Link:"+ downloadLink);
    }

    
}

// Customer class
class Customer : User //inhertiance:Reuse code
{
    // Private Field
    private List<Order> orders;

    // Constructor
    public Customer(string fullName, string email)
        : base(fullName, email)
    {
        orders = new List<Order>();
    }

    // Override DisplayInfo()
    public override void DisplayInfo()
    {
        Console.WriteLine("---Customer---");
        Console.WriteLine("Name:         "+FullName);
        Console.WriteLine("Email:        "+Email);
        Console.WriteLine("Total Orders: "+orders.Count);
    }

    // Add Order
    public void AddOrder(Order order)
    {
        orders.Add(order);
    }

    // Remove Order by ID
    public void RemoveOrder(int orderID)
    {
        orders.RemoveAll(o => o.OrderID == orderID);
    }

    // Display Order History
    public void DisplayOrderHistory()
    {
        Console.WriteLine("=== Order History ===");

        if (orders.Count == 0)
        {
            Console.WriteLine("No orders yet.");
            return;
        }

        foreach (Order order in orders)
        {
            order.DisplayInfo();
            Console.WriteLine("----------------------");
        }
    }
}

//Admin class
class Admin : User
{
    // Private Field
    private string role;

    // Constructor
    public Admin(string fullName, string email, string role)
        : base(fullName, email)
    {
        this.role = role;
    }

    // Sealed Override Method
    //override:The User class contains an abstract method,Because it is abstract, every derived class must provide its own version,So inside Admin, you override it.
    //sealed:
    public sealed override void DisplayInfo()
    {
        Console.WriteLine("---Admin---");
        Console.WriteLine("Name:   "+ FullName);
        Console.WriteLine("Email:  "+ Email);
        Console.WriteLine("Role:   "+ role);
    }
}

//Order class
class Order
{
    // Static Field
    private static int nextOrderID = 5000;

    // Private Fields
    private int orderID; //Encapsulation : Hide data
    private Customer customer;
    private Product product;
    private double totalCost;

    // Properties (Read-only)
    public int OrderID
    {
        get { return orderID; }
    }

    public Customer Customer
    {
        get { return customer; }
    }

    public double TotalCost
    {
        get { return totalCost; }
    }

    // Constructor
    public Order(Customer customer, Product product)
    {
        this.orderID = nextOrderID++;
        this.customer = customer;
        this.product = product;

        // Calculate cost at purchase time
        this.totalCost = product.CalculateTotalCost();
    }

    // Display Info
    public void DisplayInfo()
    {
        Console.WriteLine("----- Order Info -----");
        Console.WriteLine("Order ID:   "+ orderID);
        Console.WriteLine("Customer:   "+ customer.FullName);
        Console.WriteLine("Product:    "+ product.Name);
        Console.WriteLine("Total Cost: "+ totalCost);
    }
}


//Store class
class Store
{
    // Auto-Property
    public string storeName;
    
    //proptery
    public string StoreName
    {
        get { return storeName; }
        private set;
    }

    // Private Collections
    private List<Product> products;
    private List<Customer> customers;
    private List<Order> orders;

    // Constructor
    public Store(string name)
    {
        storeName = name;
        products = new List<Product>();
        customers = new List<Customer>();
        orders = new List<Order>();
    }

    // ================= Product Methods =================

    public void AddPhysicalProduct(string name, double price, double weight, double shippingPerKg)
    {
        PhysicalProduct p = new PhysicalProduct(name, price, weight, shippingPerKg);
        products.Add(p);

        Console.WriteLine("Physical product added. ID: "+p.ProductID);
    }

    public void AddDigitalProduct(string name, double price, double fileSizeMB, string link)
    {
        DigitalProduct p = new DigitalProduct(name, price, fileSizeMB, link);
        products.Add(p);

        Console.WriteLine("Digital product added. ID: "+ p.ProductID);
    }

    public void DisplayAllProducts()
    {
        Console.WriteLine("=== All Products ===");

        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        foreach (Product p in products)
        {
            // polymorphism:1.public override void DisplayInfo() { Console.WriteLine("Physical Product");},2.public override void DisplayInfo()  {Console.WriteLine("Digital Product"); }
            //polmorephism:Many Forms or Many Behaviours
            p.DisplayInfo(); 

            Console.WriteLine("----------------------");
        }
    }

    // ================= Custmer Methods =================

    public void RegisterCustomer(string fullName, string email)
    {
        // check duplicate
        Customer existing = customers.Find(c => c.Email == email);

        if (existing != null)
        {
            Console.WriteLine("Error: Email already registered.");
            return;
        }

        Customer c = new Customer(fullName, email);
        customers.Add(c);

        Console.WriteLine("Customer registered successfully.");
    }

    public Customer FindCustomer(string email)
    {
        return customers.Find(c => c.Email == email);
    }

    // ================= Order Mehtods =================

    public void PlaceOrder(string email, int productID)
    {
        Customer customer = FindCustomer(email);
        Product product = products.Find(p => p.ProductID == productID);

        if (customer == null)
        {
            Console.WriteLine("Error: Customer not found.");
            return;
        }

        if (product == null)
        {
            Console.WriteLine("Error: Product not found.");
            return;
        }

        Order order = new Order(customer, product);

        orders.Add(order);
        customer.AddOrder(order);

        Console.WriteLine("Order placed successfully. Order ID: " + order.OrderID+ ", Total: "+order.TotalCost);
    }

    public void CancelOrder(int orderID)
    {
        //Check each order in the list.If its OrderID matches the required orderID,return that order
        Order order = orders.Find(o => o.OrderID == orderID);

        if (order == null)
        {
            Console.WriteLine("Error: Order not found.");
            return;
        }

        // remove from customer
        order.Customer.RemoveOrder(orderID);

        // remove from store list
        orders.RemoveAll(o => o.OrderID == orderID);

        Console.WriteLine("Order cancelled successfully.");
    }

    public void DisplayCustomerOrders(string email)
    {
        Customer customer = FindCustomer(email);

        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }

        customer.DisplayInfo();
        customer.DisplayOrderHistory();
    }

    // ================= Statistics =================

    public void DisplayStatistics()
    {
        Console.WriteLine("=== Store Statistics ===");
        Console.WriteLine("Store Name: "+ storeName);

        int physicalCount = 0;
        int digitalCount = 0;

        foreach (Product p in products)
        {
            if (p is PhysicalProduct)
                physicalCount++;
            else if (p is DigitalProduct)
                digitalCount++;
        }

        double totalRevenue = 0;
        foreach (Order o in orders)
        {
            totalRevenue += o.TotalCost;
        }

        Console.WriteLine("Total Products:       "+ products.Count);
        Console.WriteLine("Physical Products:    "+ physicalCount);
        Console.WriteLine("Digital Products:     "+ digitalCount);
        Console.WriteLine("Registered Customers: "+ customers.Count);
        Console.WriteLine("Total Orders:         "+ orders.Count);
        Console.WriteLine("Total Revenue:        "+totalRevenue);
        Console.WriteLine("Total Users Created:  "+User.GetTotalUsersCreated());
    }
}