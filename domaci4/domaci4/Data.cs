namespace domaci4;


public class CustomerData
{
    private List<Customer> listOfCustomers;

    public CustomerData()
    {
        listOfCustomers = new List<Customer>();
    }

    public void Save(Customer customer)
    {
        listOfCustomers.Add(customer);
    }
}

public class SalesmanData
{
    private List<Salesman> listOfSalesman;

    public SalesmanData()
    {
        listOfSalesman = new List<Salesman>();
    }

    public void Save(Salesman salesman)
    {
        listOfSalesman.Add(salesman);
    }
}

