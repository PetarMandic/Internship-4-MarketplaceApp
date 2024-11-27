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
            if (currentProduct.Price <= currentCustomer.Balance && currentProduct.Status == "na prodaju")
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
                List<Product> emptyProductList = new List<Product>();
                TransactionData.CustomerTransactions.Add(customerClone, transactionList);
                ProductData.PurchasedProducts.Add(customerClone, emptyProductList);
            }
            
            var listTransactionSalesman = TransactionData.SalesmanTransactions[salesmanClone];
            var listTransactionCustomer = TransactionData.CustomerTransactions[currentCustomer];
            
            listTransactionSalesman.Add(transaction);
            listTransactionCustomer.Add(transaction);
            
            TransactionData.CustomerTransactions.Remove(currentCustomer);
            TransactionData.SalesmanTransactions.Remove(salesmanClone);
            
            TransactionData.SalesmanTransactions.Add(salesmanClone, listTransactionSalesman);
            TransactionData.CustomerTransactions.Add(customerClone, listTransactionCustomer);
            
            TransactionData.ProductTransactions.Add(productClone, transaction);

            var productList = ProductData.PurchasedProducts[currentCustomer];
            productList.Add(productClone);
            ProductData.PurchasedProducts.Remove(currentCustomer);
            ProductData.PurchasedProducts.Add(customerClone, productList);
        }

        public static bool DoesProductBelongToCustomer(Customer currentCustomer, Product currentProduct)
        {
            var check = false;
            foreach (var product in ProductData.PurchasedProducts[currentCustomer])
            {
                if (product == currentProduct)
                {
                    check = true;
                }
            }
            return check;
        }
        
        public static void Product_Return(Customer currentCustomer, Product currentProduct)
        {
            var newBalance = currentCustomer.Balance + currentProduct.Price;
            Customer customerClone = new Customer("", "", 0.00);
            Salesman salesmanClone = new Salesman("", "");
            Product productClone = new Product("", "", 0.00, "", "");
            
            foreach (var customer in CustomerData.listOfCustomers)
            {
                if (currentCustomer == customer)
                {
                    customer.Balance = newBalance;
                    customerClone = customer;
                    break;
                }
            }
            
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
            
            foreach (var product in ProductData.ListOfProducts)
            {
                if (product == currentProduct)
                {
                    product.Status = "na prodaju";
                    productClone = product;
                    break;
                }
            }
            
            foreach (var salesman in SalesmanData.listOfSalesman)
            {
                if (salesmanClone == salesman)
                {
                    var list = ProductData.SalesmanInventory[salesman];
                    foreach (var product in list)
                    {
                        if (product == currentProduct)
                        {
                            product.Status = "na prodaju";
                        }
                    }
                }
            }
            
            var currentTransaction = TransactionData.ProductTransactions[currentProduct];
            TransactionData.ProductTransactions.Remove(currentProduct);
            
            var salesmanTransactionList = TransactionData.SalesmanTransactions[salesmanClone];
            foreach (var transaction in salesmanTransactionList)
            {
                if (transaction == currentTransaction)
                {
                    TransactionData.SalesmanTransactions[salesmanClone].Remove(transaction);
                    break;
                }
            }
         
            var customerTransactionList = TransactionData.CustomerTransactions[currentCustomer];
            foreach (var transaction in customerTransactionList)
            {
                if (transaction == currentTransaction)
                {
                    TransactionData.CustomerTransactions[currentCustomer].Remove(transaction);
                    break;
                }
            }
            
            var listOfProducts = ProductData.PurchasedProducts[currentCustomer];
            listOfProducts.Remove(currentProduct);
            ProductData.PurchasedProducts.Remove(currentCustomer);
            ProductData.PurchasedProducts.Add(customerClone, listOfProducts);
            
        }

        public static bool StatusOfProduct(Product currentProduct)
        {
            var check = false;
            if (currentProduct.Status == "na prodaju")
            {
                check = true;
            }
            return check;
        }
        
        public static void Add_Product_To_List_Of_Favourites(Customer currentCustomer, Product currentProduct)
        {
            if (!CustomerData.ListOfFavouriteProducts.ContainsKey(currentCustomer))
            {
                List<Product> productList = new List<Product>();
                CustomerData.ListOfFavouriteProducts.Add(currentCustomer, productList);
            }
            CustomerData.ListOfFavouriteProducts[currentCustomer].Add(currentProduct);
        }

        public static void Overview_Of_Purchase_History(Customer currentCustomer)
        {
            var number = 1;
            if (ProductData.PurchasedProducts[currentCustomer].Count == 0)
            {
                Console.WriteLine("Niste kupili još niti jedan proizvod");
                return;
            }
            
            foreach (var product in ProductData.PurchasedProducts[currentCustomer])
            {
                Console.WriteLine(number+". Naziv: "+product.Name+"  Opis: "+product.Description+"  Cijena: "+product.Price+"  Kategorija: "+product.Category+"  Status: "+product.Status+"  Id: "+product.Id);
                number++;
            }
        }

        public static void Overview_Of_Favourites_List(Customer currentCustomer)
        {
            var number = 1;
            if (CustomerData.ListOfFavouriteProducts[currentCustomer].Count == 0)
            {
                Console.WriteLine("Lista omiljenih je prazna");
                return;
            }
            
            foreach (var product in CustomerData.ListOfFavouriteProducts[currentCustomer])
            {
                Console.WriteLine(number+". Naziv: "+product.Name+"  Opis: "+product.Description+"  Cijena: "+product.Price+"  Kategorija: "+product.Category+"  Status: "+product.Status+"  Id: "+product.Id);
                number++;
            }
        }
    }
}
