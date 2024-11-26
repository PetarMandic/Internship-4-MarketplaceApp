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
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Salesman Salesman { get; set; }
        public DateTime Date { get; set; }

        public Transaction(int id, Customer customer, Salesman salesman, DateTime date)
        {
            Id = id;
            Customer = customer;
            Salesman = salesman;
            Date = date;
        }
    }
}