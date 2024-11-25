using System.Reflection.Metadata;
using Data;
using MarketplaceApp_Domain_Objects;
using MarketplaceApp_SalesmanMenu;

namespace domaci4.MarketplaceApp.Domain
{
    public class SalesmanOptions
    {
        public static Salesman FindSalesman(string email)
        {
            Salesman currentSalesman = new Salesman("", "");
            foreach (var salesman in SalesmanData.listOfSalesman)
            {
                if (salesman.Email == email)
                {
                    currentSalesman = salesman;
                }
            }

            return currentSalesman;
        }
        
        public static void Add_Product(string name, string description, double price, Salesman currentSalesman)
        {
            Product product = new Product(name,description,price);
            var list = ProductData.SalesmanInventory[currentSalesman];
            list.Add(product);
            ProductData.SalesmanInventory[currentSalesman] = list;
        }

        public static void Overview_Of_Salesman_Products(Salesman currentSalesman)
        {
            var list = ProductData.SalesmanInventory[currentSalesman];
            int number = 1;
            
            foreach (var product in list)
            {
                Console.WriteLine(number+". Naziv: "+product.Name+"  Opis: "+product.Description+"  Cijena: "+product.Price);
                number++;
            }
        }
    }
}