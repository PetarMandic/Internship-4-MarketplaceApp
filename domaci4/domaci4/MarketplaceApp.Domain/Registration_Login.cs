using Data;
using domaci4.MarketplaceApp.Domain;
using MarketplaceApp_Domain_Objects;

namespace MarketplaceApp_Domain
{
    public class Registration_Login
    {
        public static bool Check_If_Email_Of_Customer_Exists(string email)
        {
            var emailExist = false;
            
            if (CustomerData.listOfCustomers.Count != 0)
            {
                foreach (var customer in CustomerData.listOfCustomers)
                {
                    if (customer.Email == email)
                    {
                        Console.Write("Korisnik već postoji s unesenim Email-om, unesite ponovno: ");
                        emailExist = true;
                    }
                }
            }
            
            if(SalesmanData.listOfSalesman.Count != 0)
            {
                
                foreach (var salesman in SalesmanData.listOfSalesman)
                {
                    if (salesman.Email == email)
                    {
                        Console.Write("Korisnik već postoji s unesenim Email-om, unesite ponovno: ");
                        emailExist = true;
                    }
                }
            }

            return emailExist;
        }

        public static void CreateCustomer(string name, string email, double balance)
        {
            Customer customer = new Customer(name, email, balance);
            CustomerData.listOfCustomers.Add(customer);
            var promoCode = CustomerOptions.GeneratePromoCode(customer);
            CustomerOptions.GetPromoCode(customer, promoCode);
        }

        public static void CreateSalesman(string name, string email)
        {
            Salesman salesman = new Salesman(name, email);
            SalesmanData.listOfSalesman.Add(salesman);
            List<Product> ListOfProducts = new List<Product>();
            ProductData.SalesmanInventory.Add(salesman, ListOfProducts);
        }

        public static int FindEmail(string email)
        {
            var emailExist = 0;
            
            foreach (var customer in CustomerData.listOfCustomers)
            {
                if (customer.Email == email)
                {
                    emailExist = 1;
                }
            }
            
            foreach (var salesman in SalesmanData.listOfSalesman)
            {
                if (salesman.Email == email)
                {
                    emailExist = 2;
                }
            }
            
            return emailExist;
        }
        
    }
}