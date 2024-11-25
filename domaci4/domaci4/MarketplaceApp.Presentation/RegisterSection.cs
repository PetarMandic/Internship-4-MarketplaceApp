namespace MarketplaceApp_Register
{

    public class RegisterSection
    {
        public static void Register()
        {
            Console.WriteLine("Odaberite status novog korisnika");
            Console.WriteLine("1 -> Kupac");
            Console.WriteLine("2 -> Prodavač");

            Console.Write("Unesite broj(1 ili 2): ");
            var action = 0;
            var check = false;
            while (!check)
            {
                check = int.TryParse(Console.ReadLine(), out action);
                if (check && (action < 1 || action > 3))
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
                    Console.Clear();
                    CustomerInfo();
                    break;
                case 2:
                    Console.Clear();
                    SalesmanInfo();
                    break;
            }

            static void CustomerInfo()
            {
                Console.Write("Unesite ime kupca: ");
                var name = "";
                while (string.IsNullOrEmpty(name))
                {
                    name = Console.ReadLine();
                    if (string.IsNullOrEmpty(name))
                    {
                        Console.Write("Niste unijeli ime kupca, unesite ponovno:");
                    }
                }

                Console.Write("Ime e-mail kupca:  ");
                var email = "";
                var emailExist = true;
                while (emailExist || string.IsNullOrEmpty(email))
                {
                    email = Console.ReadLine();
                    emailExist = MarketplaceApp_Domain.Domain.Check_If_Email_Of_Customer_Exists(email);
                    if (string.IsNullOrEmpty(email))
                    {
                        Console.Write("Niste unijeli email, unesite ponovno: ");
                    }
                }

                Console.Write("Unesite početni balans (npr. 100.00): ");
                var balance = 0.00;
                var check = false;
                while (!check)
                {
                    check = double.TryParse(Console.ReadLine(), out balance);
                    if (!check)
                    {
                        Console.Write("Niste unijeli iznos, unesite ponovno: ");
                    }
                }

                MarketplaceApp_Domain.Domain.CreateCustomer(name, email, balance);
                Console.Clear();
                MarketplaceApp.Program.Main();
            }

            static void SalesmanInfo()
            {
                Console.Write("Unesite ime kupca: ");
                var name = "";
                while (string.IsNullOrEmpty(name))
                {
                    name = Console.ReadLine();
                    if (string.IsNullOrEmpty(name))
                    {
                        Console.Write("Niste unijeli ime kupca, unesite ponovno:");
                    }
                }

                Console.Write("Ime e-mail kupca:  ");
                var email = "";
                var emailExist = true;
                while (emailExist || string.IsNullOrEmpty(email))
                {
                    email = Console.ReadLine();
                    emailExist = MarketplaceApp_Domain.Domain.Check_If_Email_Of_Customer_Exists(email);
                    if (string.IsNullOrEmpty(email))
                    {
                        Console.Write("Niste unijeli email, unesite ponovno: ");
                    }
                }

                MarketplaceApp_Domain.Domain.CreateSalesman(name, email);
                Console.Clear();
                MarketplaceApp.Program.Main();
            }

        }
    }
}