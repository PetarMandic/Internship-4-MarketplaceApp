namespace domaci4;

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
