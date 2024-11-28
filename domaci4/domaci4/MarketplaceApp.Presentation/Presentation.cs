using System;
using MarketplaceApp_Domain;

namespace MarketplaceApp
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("1 - Registracija korisnika");
            Console.WriteLine("2 - Prijava korisnika");
            Console.WriteLine("3 - Izlaz iz aplikacije");
            Console.WriteLine("");
            Console.Write("Odaberite željenu akciju: ");
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
                    MarketplaceApp_Register.RegisterSection.Register();
                    break;
                case 2:
                    Console.Clear();
                    MarketplaceApp_Login.LoginSection.Login();
                    break;
                case 3:
                    break;
            }
            
        }
    }
}