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
        
        public static void Product_Purchase(Product currentProduct, Customer currentCustomer, double price)
        {
            var newBalance = 0.00;
            var action = 0;
            if (price == 0)
            {
                newBalance = currentCustomer.Balance - currentProduct.Price;
            }
            else
            {
                newBalance = currentCustomer.Balance - price;
                action = 1;
            }
            
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

            if (action == 1)
            {
                SalesmanEarnings salesmanEarnings = new SalesmanEarnings(price, DateTime.Now);
                if (!SalesmanData.ListOfSalesmanEarnings.ContainsKey(salesmanClone))
                {
                    List<SalesmanEarnings> salesmanEarningsList = new List<SalesmanEarnings>();
                    SalesmanData.ListOfSalesmanEarnings.Add(salesmanClone, salesmanEarningsList);
                }
                
                var list = SalesmanData.ListOfSalesmanEarnings[salesmanClone];
                list.Add(salesmanEarnings);
                SalesmanData.ListOfSalesmanEarnings[salesmanClone] = list;
            }
            else
            {                                                                                                                                                                                                       
                SalesmanEarnings salesmanEarnings = new SalesmanEarnings(currentProduct.Price, DateTime.Now);
                if (!SalesmanData.ListOfSalesmanEarnings.ContainsKey(salesmanClone))
                {
                    List<SalesmanEarnings> salesmanEarningsList = new List<SalesmanEarnings>();
                    SalesmanData.ListOfSalesmanEarnings.Add(salesmanClone, salesmanEarningsList);
                }
                
                var list = SalesmanData.ListOfSalesmanEarnings[salesmanClone];
                list.Add(salesmanEarnings);
                SalesmanData.ListOfSalesmanEarnings[salesmanClone] = list;
            }
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
            if (!ProductData.PurchasedProducts.ContainsKey(currentCustomer))
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
            if (!CustomerData.ListOfFavouriteProducts.ContainsKey(currentCustomer))
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

        public static bool Check_If_Customer_Get_Promo_Code(Customer currentCustomer, Product lastProduct)
        {
            var numberOfProducts = ProductData.PurchasedProducts[currentCustomer].Count;
            if (numberOfProducts % 3 == 0 && numberOfProducts >= 3)
            {
                Console.WriteLine("Čestitam, osvojili ste promotivni kod za vašu treću kupovinu!!!");
                return true;
            }
            
            else if (lastProduct.Price >= 200.00)
            {
                Console.WriteLine("Čestitam, osvojili ste promotivni kod kupovinom proizvoda iznad 200.00 dolara");
                return true;
            }
            
            return false;
        }

        public static PromoCode GeneratePromoCode(Customer currentCustomer)
        {
            var Code = Guid.NewGuid();
            var code = Code.ToString();
            
            Random random = new Random();
            var randomNumber = 0.01 + (random.NextDouble() * (1 - 0.01));
            var randomNumberOfDays = random.Next(1, 365);
            DateTime date = DateTime.Now;
            date.AddDays(randomNumberOfDays);

            var category = "";
            if (ProductData.ListOfProductsCategorys.Count == 0)
            {
                category = "All";
            }
            else
            {
                random = new Random(); 
                var RandomNumber = random.Next(0, ProductData.ListOfProductsCategorys.Count);
                category = ProductData.ListOfProductsCategorys[RandomNumber];
            }
            
            PromoCode promoCode = new PromoCode(code, currentCustomer, randomNumber, category, date);
            return promoCode;
        }
        
        public static void GetPromoCode(Customer currentCustomer, PromoCode promoCode)
        {
            if (!CustomerData.ListOfPromoCodes.ContainsKey(currentCustomer))
            {
                List<PromoCode> promoCodes = new List<PromoCode>();
                CustomerData.ListOfPromoCodes.Add(currentCustomer, promoCodes);
            }
                
            var list = CustomerData.ListOfPromoCodes[currentCustomer];
            list.Add(promoCode);
            CustomerData.ListOfPromoCodes[currentCustomer] = list;
        }

        public static (bool, PromoCode) OptionToUsePromoCode(Customer currentCustomer, Product currentProduct)
        {
            foreach (var promoCode in CustomerData.ListOfPromoCodes[currentCustomer])
            {
                if (promoCode.Category == currentProduct.Category || promoCode.Category == "All")
                {
                    return (true, promoCode);
                }
            }
            
            return (false, null);
        }

        public static double UsePromoCode(Customer currentCustomer, PromoCode promoCode, Product currentProduct)
        {
            var newPrice = currentProduct.Price - currentProduct.Price * promoCode.Discount;
            CustomerData.ListOfPromoCodes[currentCustomer].Remove(promoCode);
            return newPrice;
        }
    }
}
