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

        public Product(string name, string description, double price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}