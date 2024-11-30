using Data;
using MarketplaceApp_Domain_Objects;

namespace domaci4.MarketplaceApp.Domain
{
    public class InitialData
    {
        public static void InitialDataFunction()
        {
            Customer kupac1 = new Customer("Ana", "ana@email.com", 100.50);
            Customer kupac2 = new Customer("Igor", "igor@email.com", 200.75);
            CustomerData.listOfCustomers.Add(kupac1);
            CustomerData.listOfCustomers.Add(kupac2);
            
            Salesman prodavac1 = new Salesman("Robert Novak", "robert@email.com");
            Salesman prodavac2 = new Salesman("Petar Kovač", "petar@email.com");
            SalesmanData.listOfSalesman.Add(prodavac1);
            SalesmanData.listOfSalesman.Add(prodavac2);
            
            Product proizvod1 = new Product("Laptop", "Brzi laptop", 1200.00, "Elektronika", "na prodaju");
            Product proizvod2 = new Product("Pametni telefon", "Novi telefon", 800.00, "Elektronika", "na prodaju");
            Product proizvod3 = new Product("Televizor", "Veliki ekran", 1500.00, "Kućanski aparati", "prodano");
            Product proizvod4 = new Product("Usisavač", "Dobar usisavač", 300.00, "Kućanski aparati", "prodano");
            
            ProductData.ListOfProducts.Add(proizvod1);
            ProductData.ListOfProducts.Add(proizvod2);
            ProductData.ListOfProducts.Add(proizvod3);
            ProductData.ListOfProducts.Add(proizvod4);
            
            ProductData.ListOfProductsCategorys.Add("Elektronika");
            ProductData.ListOfProductsCategorys.Add("Kućanski aparati");
            
            Transaction transakcija1 = new Transaction(kupac1, prodavac1, DateTime.Now);
            Transaction transakcija2 = new Transaction(kupac2, prodavac2, DateTime.Now.AddDays(-1));
            TransactionData.CustomerTransactions[kupac1] = new List<Transaction> { transakcija1 };
            TransactionData.CustomerTransactions[kupac2] = new List<Transaction> { transakcija2 };
            TransactionData.SalesmanTransactions[prodavac1] = new List<Transaction> { transakcija1 };
            TransactionData.SalesmanTransactions[prodavac2] = new List<Transaction> { transakcija2 };
            
            PromoCode promoKod1 = new PromoCode("POPUST10", kupac1, 10.0, "Elektronika", DateTime.Now);
            PromoCode promoKod2 = new PromoCode("SAVET20", kupac2, 20.0, "Kućanski aparati", DateTime.Now.AddDays(-2));
            CustomerData.ListOfPromoCodes[kupac1] = new List<PromoCode> { promoKod1 };
            CustomerData.ListOfPromoCodes[kupac2] = new List<PromoCode> { promoKod2 };
            
            CustomerData.ListOfFavouriteProducts[kupac1] = new List<Product> { proizvod1 };
            CustomerData.ListOfFavouriteProducts[kupac2] = new List<Product> { proizvod2 };
            
            SalesmanEarnings zarada1 = new SalesmanEarnings(300.00, DateTime.Now, proizvod1);
            SalesmanEarnings zarada2 = new SalesmanEarnings(500.00, DateTime.Now.AddDays(-1), proizvod2);
            SalesmanData.ListOfSalesmanEarnings[prodavac1] = new List<SalesmanEarnings> { zarada1 };
            SalesmanData.ListOfSalesmanEarnings[prodavac2] = new List<SalesmanEarnings> { zarada2 };
            
            ProductData.PurchasedProducts[kupac1] = new List<Product> { proizvod1 };
            ProductData.PurchasedProducts[kupac2] = new List<Product> { proizvod2 };
            
            ProductData.SalesmanInventory[prodavac1] = new List<Product> { proizvod1 };
            ProductData.SalesmanInventory[prodavac2] = new List<Product> { proizvod2 };
            
            Transaction transakcijaProizvoda1 = new Transaction(kupac1, prodavac1, DateTime.Now);
            Transaction transakcijaProizvoda2 = new Transaction(kupac2, prodavac2, DateTime.Now.AddDays(-1));
            TransactionData.ProductTransactions[proizvod1] = transakcijaProizvoda1;
            TransactionData.ProductTransactions[proizvod2] = transakcijaProizvoda2;
        }
    }
}
