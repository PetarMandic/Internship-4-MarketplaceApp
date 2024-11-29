namespace MarketplaceApp_Domain_Objects 
{

    public class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Double Balance { get; set; }

        public Customer(string name, string email, Double balance)
        {
            Name = name;
            Email = email;
            Balance = balance;
        }
    }

    public class Salesman
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Salesman(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }

    public class SalesmanEarnings
    {
        public double Earnings { get; set; }
        public DateTime Date { get; set; }
        public Product Product { get; set; }

        public SalesmanEarnings(double earnings, DateTime date, Product product)
        {
            Earnings = earnings;
            Date = date;
            Product = product;
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Double Price { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public int Id { get; private set; }
        
        private static int idCounter = 1;

        public Product(string name, string description, double price, string category, string status)
        {
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            Status = status;
            Id = idCounter++;
        }
    }

    public class Transaction
    {
        private static int idCounter = 1;
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Salesman Salesman { get; set; }
        public DateTime Date { get; set; }

        public Transaction(Customer customer, Salesman salesman, DateTime date)
        {
            Id = idCounter++;
            Customer = customer;
            Salesman = salesman;
            Date = date;
        }
    }

    public class PromoCode
    {
        public string Code { get; set; }
        public Customer Customer { get; set; }
        public double Discount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }

        public PromoCode(string code, Customer customer, double discount, string category, DateTime date)
        {
            Code = code;
            Customer = customer;
            Discount = discount;
            Category = category;
            Date = date;
        }
        
    }
}