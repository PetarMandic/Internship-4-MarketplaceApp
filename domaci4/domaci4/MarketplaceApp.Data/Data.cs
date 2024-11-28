using System;
using System.Collections.Generic;
using MarketplaceApp_Domain_Objects;

namespace Data
{
    public class CustomerData
    {
        public static List<Customer> listOfCustomers = new List<Customer>();
        public static Dictionary<Customer, List<Product>> ListOfFavouriteProducts = new Dictionary<Customer, List<Product>>();
        public static Dictionary<Customer, List<PromoCode>> ListOfPromoCodes = new Dictionary<Customer, List<PromoCode>>();
    }
    
    public class SalesmanData
    {
        public static List<Salesman> listOfSalesman = new List<Salesman>();
        public static Dictionary<Salesman, List<SalesmanEarnings>> ListOfSalesmanEarnings = new Dictionary<Salesman, List<SalesmanEarnings>>();
    }

    public class ProductData
    {
        public static Dictionary<Customer, List<Product>> PurchasedProducts = new Dictionary<Customer, List<Product>>();
        public static Dictionary<Salesman, List<Product>> SalesmanInventory = new Dictionary<Salesman, List<Product>>();
        public static List<Product> ListOfProducts = new List<Product>();
        public static List<string> ListOfProductsCategorys = new List<string>();
    }

    public class TransactionData
    {
        public static Dictionary<Salesman, List<Transaction>> SalesmanTransactions = new Dictionary<Salesman, List<Transaction>>();
        public static Dictionary<Customer, List<Transaction>> CustomerTransactions = new Dictionary<Customer, List<Transaction>>();
        public static Dictionary<Product, Transaction> ProductTransactions = new Dictionary<Product, Transaction>();
    }
}
