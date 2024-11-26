using System.Reflection.Metadata;
using Data;
using MarketplaceApp_Domain_Objects;
using MarketplaceApp_SalesmanMenu;
using Transaction = MarketplaceApp_Domain_Objects.Transaction;

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
        
        public static void Add_Product(string name, string description, double price, Salesman currentSalesman, string category, string status)
        {
            Product product = new Product(name, description, price, category, status);
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
                Console.WriteLine(number+". Naziv: "+product.Name+"  Opis: "+product.Description+"  Cijena: "+product.Price+"  Kategorija: "+product.Category+"  Status: "+product.Status+"  Id: "+product.Id);
                number++;
            }
        }

        public static void Overview_Of_Earnings(Salesman currentSalesman)
        {
            var list = ProductData.SalesmanInventory[currentSalesman];
            var earnings = 0.00; 
            foreach (var product in list)
            {
                if (product.Status == "prodano")
                {
                    earnings += product.Price - product.Price * 0.05;
                }
            }
            Console.WriteLine("Ukupna trenutna zarada iznosi: "+earnings);
        }

        public static void Overview_Of_Sold_Products_Under_Category(Salesman currentSalesman, string category)
        {
            var list = ProductData.SalesmanInventory[currentSalesman];
            var earnings = 0.00;
            foreach (var product in list)
            {
                if (product.Category == category && product.Status == "prodano")
                {
                    earnings += product.Price - product.Price * 0.05;
                }
            }
            Console.WriteLine("Ukupna trenutna zarada pod određenom kategorijom iznosi: "+earnings);
        }

        public static void Overview_Of_Earnings_In_Certain_Time(Salesman currentSalesman, DateTime firstDate, DateTime secondDate)
        {
            var earnings = 0.00;
            foreach (var transaction in TransactionData.SalesmanTransactions[currentSalesman])
            {
                if (transaction.Date >= firstDate && transaction.Date <= secondDate)
                {
                    var product = TransactionData.ProductTransactions[transaction];
                    earnings += product.Price - product.Price * 0.05;
                }
            }
            Console.WriteLine("Ukupna trenutna zarada pod u određenom vremenu iznosi: "+earnings);
        }
    }
}