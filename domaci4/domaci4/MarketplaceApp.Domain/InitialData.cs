using Data;
using MarketplaceApp_Domain_Objects;

namespace domaci4.MarketplaceApp.Domain
{
    public class InitialData
    {
        public static Customer customer1 = new Customer("Petar", "petar@gmail.com", 100.00);
        public static Customer customer2 = new Customer("Igor", "igor@gmail.com", 6000.00);
        
        public static Salesman salesman1 = new Salesman("Dario", "dario@gmail.com");
        public static Salesman salesman2 = new Salesman("Ana", "ana@gmail.com");

        public static Product Product1 = new Product("Pizza", "Mješana", 12.5, "hrana", "na prodaji");
        public static Product Product2 = new Product("Suzuki sx4", "benzinac", 250, "automobil", "na prodaji");
        public static Product Product3 = new Product("Ploča za pikado", "nikad korištena", 45, "igra", "prodano");
        public static Product Product4 = new Product("Pizza", "Margarita", 10, "hrana", "prodano");
        
        public static SalesmanEarnings SalesmanEarnings1 = new SalesmanEarnings(42.75,DateTime.Now, Product3);
        public static SalesmanEarnings SalesmanEarnings2 = new SalesmanEarnings(9.5,DateTime.Now, Product4);
        
        public static Dictionary<Salesman, List<SalesmanEarnings>> ListOfSalesmanEarnings = new Dictionary<Salesman, List<SalesmanEarnings>>();
    }
}