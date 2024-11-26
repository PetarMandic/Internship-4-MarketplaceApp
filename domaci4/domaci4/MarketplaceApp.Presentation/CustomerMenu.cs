using domaci4.MarketplaceApp.Domain;
using MarketplaceApp_Domain_Objects;
namespace MarketplaceApp_CustomerMenu;

public class CustomerMenuStructure
{
    public static void CustomerMenu(string email)
    {
        Console.WriteLine("OPCIJE ZA KUPCA");
        Console.WriteLine("1 - Pregled svih proizvoda dostupnih za prodaju s detaljima");
        Console.WriteLine("2 - Kupnja proizvoda unosom ID-a proizvoda");
        Console.WriteLine("3 - Povratak kupljenog proizvoda");
        Console.WriteLine("4 - Dodavanje proizvoda u listu omiljenih");
        Console.WriteLine("5 - Pregled povijesti kupljenih proizvoda");
        Console.WriteLine("6 - Pregled liste omiljenih proizvoda");
        Console.WriteLine("7 - Povratak");
        
        Console.WriteLine("");
        Console.Write("Odaberite željenu akciju: ");
        var action = 0;
        var check = false;
        while (!check)
        {
            check = int.TryParse(Console.ReadLine(), out action);
            if (check && (action < 1 || action > 7))
            {
                Console.Write("Uneseni broj ne sadrži akciju, unesite ponovno: ");
                check = false;
            }
            else if (!check)
            {
                Console.Write("Niste unijeli broj, unesite ponovno: ");
            }
        }
        
        var currentCustomer = CustomerOptions.FindCustomer(email);
        
        switch (action)
        {
            case 1:
                Console.Clear();
                Overview_Of_All_Available_Products_Structure(email);
                break;
            case 2:
                Console.Clear();
                Product_Purchase_Structure(email,currentCustomer);
                break;
            case 3:
                Console.Clear();
                Product_Return_Structure(email, currentCustomer);
                break;
            case 4:
                Console.Clear();
                Add_Product_To_List_Of_Favourites_Structure(email, currentCustomer);
                break;
            case 5:
                Console.Clear();
                Overview_Of_Purchase_History_Structure(email, currentCustomer);
                break;
            case 6:
                Console.Clear();
                Overview_Of_Favourites_List_Structure(email, currentCustomer);
                break;
            case 7:
                Console.Clear();
                MarketplaceApp.Program.Main();
                break;
        }

        static void Overview_Of_All_Available_Products_Structure(string email)
        {
            CustomerOptions.Overview_Of_All_Available_Products();
            Back();
            Console.Clear();
            CustomerMenu(email);
        }

        static void Product_Purchase_Structure(string email, Customer currentCustomer)
        {
            Console.Write("Unesite id proizvoda koji želite kupiti: ");
            var id = 0;
            var idExist = false;
            var currentProduct = new Product("", "", 0.00, "", "");
            var check = false;
            while (!check)
            {
                check = int.TryParse(Console.ReadLine(), out id);
                var output = CustomerOptions.FindProduct(id);
                idExist = output.Item1;
                currentProduct = output.Item2;
                
                if (!check)
                {
                    Console.Write("Niste unijeli broj, unesite ponovno: ");
                }
                else if (!idExist)
                {
                    Console.Write("Uneseni id ne pripada niti jednom dostupnom proizvodu, unesite ponovno: ");
                    check = false;
                }
            }
            
            var purchasePossible = CustomerOptions.IsPurchasePossible(currentProduct, currentCustomer);
            
            if (purchasePossible)
            {
                CustomerOptions.Product_Purchase(currentProduct, currentCustomer);
            }
            else
            {
                Console.WriteLine("Nažalost nemate dovoljno sredstava da bi obavili kupnju ovog proizvoda.");
            }
            
            Back();
            Console.Clear();
            CustomerMenu(email);
        }

        static void Product_Return_Structure(string email, Customer currentCustomer)
        {
            Back();
            Console.Clear();
            CustomerMenu(email);
        }

        static void Add_Product_To_List_Of_Favourites_Structure(string email, Customer currentCustomer)
        {
            Back();
            Console.Clear();
            CustomerMenu(email);
        }

        static void Overview_Of_Purchase_History_Structure(string email, Customer currentCustomer)
        {
            Back();
            Console.Clear();
            CustomerMenu(email);
        }

        static void Overview_Of_Favourites_List_Structure(string email, Customer currentCustomer)
        {
            Back();
            Console.Clear();
            CustomerMenu(email);
        }

        static void Back()
        {
            Console.Write("Unesite broj 1 za povratak: ");
            var back = 0;
            var check = false;
            while (!check)
            {
                check = int.TryParse(Console.ReadLine(), out back);
                if (back != 1 && check == true)
                {
                    Console.Write("Unesen je krivi broj, upišite ponovno: ");
                    check = false;
                }
                else if (!check)
                {
                    Console.Write("Niste unijeli broj, upišite ponovno: ");
                }
            }
        }
    }
}