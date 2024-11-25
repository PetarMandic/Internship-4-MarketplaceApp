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
            action = MarketplaceApp_Domain.Domain.FindEmail(email);
        }

        switch (action)
        {
            case 1:
                MarketplaceApp_CustomerMenu.CustomerMenuStructure.CustomerMenu();
                break;
            case 2:
                MarketplaceApp_SalesmanMenu.SalesmanMenuStructure.SalesmanMenu();
                break;
        }
    }
}