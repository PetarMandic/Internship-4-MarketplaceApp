using MarketplaceApp_Domain_Objects;
namespace MarketplaceApp_Login;

public class LoginSection
{
    public static void Login()
    {
        Console.Write("Unesite email od korisnika na koga se želite prijaviti: ");
        var email = "";
        var action = 0;
        while (action == 0 || string.IsNullOrEmpty(email))
        {
            email = Console.ReadLine();
            action = MarketplaceApp_Domain.Registration_Login.FindEmail(email);
            if (action == 0)
            {
                Console.WriteLine("Uneseni email ne postoji.");
                Console.WriteLine("Želite li se vratiti natrag ili pokušati ponovno?");
                Console.WriteLine("1 - Pokušaj ponovno");
                Console.WriteLine("2 - Povratak");
                
                Console.Write("Unsite broj: ");
                var newAction = 0;
                var check = false;
                while (!check)
                {
                    check = int.TryParse(Console.ReadLine(), out newAction);
                }

                switch (newAction)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Unesite email od korisnika na koga se želite prijaviti: ");
                        break;
                    case 2:
                        Console.Clear();
                        MarketplaceApp.Program.Main();
                        break;
                }
            }
        }
        
        switch (action)
        {
            case 1:
                MarketplaceApp_CustomerMenu.CustomerMenuStructure.CustomerMenu(email);
                break;
            case 2:
                MarketplaceApp_SalesmanMenu.SalesmanMenuStructure.SalesmanMenu(email);
                break;
        }
    }
}