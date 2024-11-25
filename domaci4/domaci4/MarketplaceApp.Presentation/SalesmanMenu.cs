namespace MarketplaceApp_SalesmanMenu;

public class SalesmanMenuStructure
{
    public static void SalesmanMenu()
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
        }
    }
}