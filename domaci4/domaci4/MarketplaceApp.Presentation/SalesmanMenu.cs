using MarketplaceApp_Domain_Objects;
using domaci4.MarketplaceApp.Domain;

namespace MarketplaceApp_SalesmanMenu;

public class SalesmanMenuStructure
{
    public static void SalesmanMenu(string email)
    {
        Console.WriteLine("OPCIJE ZA PRODAVAČA");
        Console.WriteLine("1 - Dodavanje proizvoda unosom naziva, opisa i cijene");
        Console.WriteLine("2 - Pregled svih proizvoda u vlasništvu prodavača");
        Console.WriteLine("3 - Pregled ukupne zarade od prodaje");
        Console.WriteLine("4 - Pregled prodanih proizvoda po kategoriji");
        Console.WriteLine("5 - Pregled zarade u određenom vremenskom razdoblju");
        Console.WriteLine("6 - Povratak");

        Console.WriteLine("");
        Console.Write("Odaberite željenu akciju: ");
        var action = 0;
        var check = false;
        while (!check)
        {
            check = int.TryParse(Console.ReadLine(), out action);
            if (check && (action < 1 || action > 6))
            {
                Console.Write("Uneseni broj ne sadrži akciju, unesite ponovno: ");
                check = false;
            }
            else if (!check)
            {
                Console.Write("Niste unijeli broj, unesite ponovno: ");
            }
        }
        
        var currentSalesman = SalesmanOptions.FindSalesman(email);
        
        
        switch (action)
        {
            case 1:
                Console.Clear();
                Add_Product_Structure(email, currentSalesman);
                break;
            case 2:
                Console.Clear();
                Overview_Of_Salesman_Products_Structure(email, currentSalesman);
                break;
            case 3:
                Console.Clear();
                Overview_Of_Earnings_Structure(email);
                break;
            case 4:
                Console.Clear();
                Overview_Of_Sold_Products_Under_Category_Structure(email);
                break;
            case 5:
                Console.Clear();
                Overview_Of_Earnings_In_Certain_Time_Structure(email);
                break;
            case 6:
                Console.Clear();
                MarketplaceApp.Program.Main();
                break;
        }

        static void Add_Product_Structure(string email, Salesman currentSalesman)
        {
            Console.Write("Unesite naziv proizvoda: ");
            var name = "";
            while (string.IsNullOrWhiteSpace(name))
            {
                name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    Console.Write("Niste unijeli naziv, unesite ponovno: ");
                }
            }
            
            Console.Write("Unesite opis proizvoda: ");
            var description = "";
            while (string.IsNullOrWhiteSpace(description))
            {
                description = Console.ReadLine();
                if (string.IsNullOrEmpty(description))
                {
                    Console.Write("Niste unijeli opis proizvoda, unesite ponovno: ");
                }
            }
            
            Console.Write("Unesite cijenu proizvoda(npr. 10.00): ");
            var price = 0.00;
            var check = false;
            while (!check)
            {
                check = double.TryParse(Console.ReadLine(), out price);
                if (!check)
                {
                    Console.Write("Niste unijeli pravilno vrijednost, unesite ponovno: ");
                }
            }
            
            SalesmanOptions.Add_Product(name, email, price, currentSalesman);
            
            Back();
            Console.Clear();
            SalesmanMenu(email);
        }

        static void Overview_Of_Salesman_Products_Structure(string email, Salesman currentSalesman)
        {
            SalesmanOptions.Overview_Of_Salesman_Products(currentSalesman);
            Back();
            Console.Clear();
            SalesmanMenu(email);
        }

        static void Overview_Of_Earnings_Structure(string email)
        {
            Back();
            Console.Clear();
            SalesmanMenu(email);
        }

        static void Overview_Of_Sold_Products_Under_Category_Structure(string email)
        {
            Back();
            Console.Clear();
            SalesmanMenu(email);
        }

        static void Overview_Of_Earnings_In_Certain_Time_Structure(string email)
        {
            Back();
            Console.Clear();
            SalesmanMenu(email);
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