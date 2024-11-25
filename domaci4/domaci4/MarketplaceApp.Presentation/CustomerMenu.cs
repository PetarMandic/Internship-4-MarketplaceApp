namespace MarketplaceApp_CustomerMenu;

public class CustomerMenuStructure
{
    public static void CustomerMenu()
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
        
        switch (action)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
        }
    }
}