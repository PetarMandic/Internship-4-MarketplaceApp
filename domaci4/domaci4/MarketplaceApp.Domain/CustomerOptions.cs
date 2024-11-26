using Data;
using MarketplaceApp_Domain_Objects;

namespace domaci4.MarketplaceApp.Domain
{
    public class CustomerOptions
    {
        public static Customer FindCustomer(string email)
        {
            Customer currentCustomer = new Customer("", "", 0.00);
            foreach (var customer in CustomerData.listOfCustomers)
            {
                if (customer.Email == email)
                {
                    currentCustomer = customer;
                }
            }

            return currentCustomer;
        }

        public static void Overview_Of_All_Available_Products()
        {
            var number = 1;
            foreach (var salesman in SalesmanData.listOfSalesman)
            {
                var list = ProductData.SalesmanInventory[salesman];
                foreach (var product in list)
                {
                    if (product.Status == "na prodaju")
                    {
                        Console.WriteLine(number+". Naziv: "+product.Name+"  Opis: "+product.Description+"  Cijena: "+product.Price+"  Kategorija: "+product.Category+"  Status: "+product.Status+"  Id: "+product.Id);
                        number++;
                    }
                }
            }
        }

        public static (bool, Product) FindProduct(int id)
        {
            var idExist = false;
            var currentProduct = new Product("", "", 0.00, "", "");
            foreach (var product in ProductData.ListOfProducts)
            {
                if (product.Id == id)
                {
                    idExist = true;
                    currentProduct = product;
                }
            }
            return (idExist, currentProduct);
        }

        public static bool IsPurchasePossible(Product currentProduct, Customer currentCustomer)
        {
            var purchasePossible = false;
            if (currentProduct.Price <= currentCustomer.Balance)
            {
                purchasePossible = true;
            }
            return purchasePossible;
        }
        
        public static void Product_Purchase(Product currentProduct, Customer currentCustomer)
        {
            Console.WriteLine("Kupnja proizvoda je uspiješno izvržena.");
            var newBalance = currentCustomer.Balance - currentProduct.Price;
            Customer customerClone = new Customer("", "", 0.00);
            Salesman salesmanClone = new Salesman("", "");
            Product productClone = new Product("", "", 0.00, "", "");
            
            //Promjena vrijednosti Customer.Balance unutar listOfCustomer
            foreach (var customer in CustomerData.listOfCustomers)
            {
                if (currentCustomer == customer)
                {
                    customer.Balance = newBalance;
                    customerClone = customer;
                    break;
                }
            }
            //Pronalazk kojem prodavaču pripada proizvod
            foreach (var salesman in SalesmanData.listOfSalesman)
            {
                foreach (var product in ProductData.SalesmanInventory[salesman])
                {
                    if (product == currentProduct)
                    {
                        salesmanClone = salesman;
                        break;
                    } 
                }
            }
            //Promjena statusa proizvoda unutar ListOfProducts
            foreach (var product in ProductData.ListOfProducts)
            {
                if (product == currentProduct)
                {
                    product.Status = "prodano";
                    productClone = product;
                    break;
                }
            }
            //Promjena statusa proizvoda unutar liste koju sadržava dictionary SalesmanInventory
            foreach (var salesman in SalesmanData.listOfSalesman)
            {
                if (salesmanClone == salesman)
                {
                    var list = ProductData.SalesmanInventory[salesman];
                    foreach (var product in list)
                    {
                        if (product == currentProduct)
                        {
                            product.Status = "prodano";
                        }
                    }
                }
            }
            
            Transaction transaction = new Transaction(customerClone, salesmanClone, DateTime.Now);

            if (!TransactionData.SalesmanTransactions.ContainsKey(salesmanClone))
            {
                List<Transaction> transactionList = new List<Transaction>();
                TransactionData.SalesmanTransactions.Add(salesmanClone, transactionList);
            }
            
            if (!TransactionData.CustomerTransactions.ContainsKey(currentCustomer))
            {
                List<Transaction> transactionList = new List<Transaction>();
                TransactionData.CustomerTransactions.Add(customerClone, transactionList);
            }
            
            var listTransactionSalesman = TransactionData.SalesmanTransactions[salesmanClone];
            var listTransactionCustomer = TransactionData.CustomerTransactions[currentCustomer];
            
            listTransactionSalesman.Add(transaction);
            listTransactionCustomer.Add(transaction);
            
            TransactionData.CustomerTransactions.Remove(currentCustomer);
            TransactionData.SalesmanTransactions.Remove(salesmanClone);
            
            TransactionData.SalesmanTransactions.Add(salesmanClone, listTransactionSalesman);
            TransactionData.CustomerTransactions.Add(customerClone, listTransactionCustomer);
            
            TransactionData.ProductTransactions.Add(transaction, productClone);
        }
    }
}
