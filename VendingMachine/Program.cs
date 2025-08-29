using Microsoft.VisualBasic.FileIO;
using VendingMachine.Repository;
using VendingMachine.Services;

namespace VendingMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPaymentRepo paymentRepo = new PaymentCollectionRepo();
            IVendingMachineRepo productRepo = new VendingMachineCollectionRepo();
            PaymentService paymentService = new PaymentService(paymentRepo);
            VendingMachineService vmService = new VendingMachineService(productRepo, paymentService);
            IVendingMachineRepo repo = new VendingMachineCollectionRepo();
            List<Product> products = repo.GetAll();
            

            Console.WriteLine("Skriv rolle: (Admin, Kunde)");
            string menuInput = Console.ReadLine();
            bool running = false;
            bool adminRunning = false;
            int saldo = 0;
            if (menuInput.ToLower() == "kunde")
            {
                running = true;
            }

            if (menuInput.ToLower() == "admin")
            {
                adminRunning = true;
            }
            else
            {
                Console.WriteLine("ugyldig svar");
            }
            while (adminRunning)
            {
                Console.Clear();
                Console.WriteLine("=== SIMPEL VENDING MACHINE ===");
                Console.WriteLine("Bank saldo: " + saldo + " kr.");
                Console.WriteLine();
                PrintProducts(products);
                Console.WriteLine();
                Console.WriteLine("1) Indsæt penge");
                Console.WriteLine("2) Hæv penge");
                Console.WriteLine("3) Tilføj produkt");
                Console.WriteLine("4) Fjern produkt");
                Console.WriteLine("0) Afslut");
                Console.Write("Vælg: ");

                string adminInput = Console.ReadLine();

                if (adminInput == "1")
                {
                    Console.Write("Indtast beløb: ");
                    string belobTxt = Console.ReadLine();
                    int belob;
                    if (int.TryParse(belobTxt, out belob) && belob > 0)
                    {
                        saldo += belob;
                        Console.WriteLine("Du indsatte " + belob + " kr.");
                    }
                    else
                    {
                        Console.WriteLine("Ugyldigt beløb.");
                    }
                }
                else if (adminInput == "2")
                {
                    Console.Write("Indtast beløb: ");
                    string belobTxt = Console.ReadLine();
                    int belob;
                    if (int.TryParse(belobTxt, out belob) && belob > 0)
                    {
                        saldo -= belob;
                        Console.WriteLine("Du hævede " + belob + " kr.");
                    }
                    
                }
                else if (adminInput == "3")
                {
                    Console.Write("");
                }
            }


                while (running)
                {
                    Console.Clear();
                    Console.WriteLine("=== SIMPEL VENDING MACHINE ===");
                    Console.WriteLine("Saldo: " + saldo + " kr.");
                    Console.WriteLine();
                    PrintProducts(products);
                    Console.WriteLine();
                    Console.WriteLine("1) Indsæt penge");
                    Console.WriteLine("2) Køb vare (skriv ID)");
                    Console.WriteLine("3) Få returpenge");
                    Console.WriteLine("0) Afslut");
                    Console.Write("Vælg: ");

                    string input = Console.ReadLine();
                    Console.WriteLine();

                    if (input == "1")
                    {
                        Console.Write("Indtast beløb: ");
                        string belobTxt = Console.ReadLine();
                        int belob;
                        if (int.TryParse(belobTxt, out belob) && belob > 0)
                        {
                            saldo += belob;
                            Console.WriteLine("Du indsatte " + belob + " kr.");
                        }
                        else
                        {
                            Console.WriteLine("Ugyldigt beløb.");
                        }
                    }
                    else if (input == "2")
                    {
                        Console.Write("Indtast produkt-ID: ");
                        string idTxt = Console.ReadLine();
                        int id;
                    if (int.TryParse(idTxt, out id))
                    {
                        Product chosen = FindById(products, id);
                    }

                    else
                    {

                        repo.Delete(chosen);
                        Console.WriteLine("Du fik: " + chosen.Name);
                    }
                        }
                    
                    else if (input == "3")
                    {
                        Console.WriteLine("Du fik " + saldo + " kr. retur.");
                        saldo = 0;
                    }
                    else if (input == "0")
                    {
                        Console.WriteLine("Tak for besøget!");
                        if (saldo > 0) Console.WriteLine("Returpenge: " + saldo + " kr.");
                        running = false;
                    }
                    else
                    {
                        Console.WriteLine("Ugyldigt valg.");
                    }

                    if (running)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Tryk en tast for at fortsætte...");
                        Console.ReadKey();
                    }
                }
        }

        static void PrintProducts(List<Product> products) //husk at fjern
        {
            Console.WriteLine("ID  Navn        Pris   Str.   Lager");
            Console.WriteLine("-------------------------------------");
            foreach (Product p in products)
            {
                Console.WriteLine(p.ID + "   " + p.Name.PadRight(10) + p.Price + "kr   " + p.Size.PadRight(5) + "   ");
            }
        }

        static Product FindById(List<Product> products, int id) //husk at fjern
        {
            foreach (Product p in products)
            {
                if (p.ID == id) return p;
            }
            return null;
        }
        private static void AddProduct(IVendingMachineRepo repo)
        {
            Console.WriteLine("===Admin: Tilføj nyt produkt====");
            Console.WriteLine("ID: ");
            string idText = Console.ReadLine();
            int id = Convert.ToInt32(idText);

            Console.WriteLine("Navn: ");
            string nameText = Console.ReadLine();

            Console.WriteLine("Pris: ");
            string priceText = Console.ReadLine();
            int price = Convert.ToInt32(idText);

            Console.WriteLine("Størrelse: ");
            string sizeText = Console.ReadLine();

            Console.WriteLine("Antal: ");
            string quantityText = Console.ReadLine();
            int quantity = Convert.ToInt32(idText);

            Product newProduct = new Product(id, nameText, price, sizeText);

            repo.Add(newProduct);

            Console.WriteLine($"Produkt tilføjet: ID: {id}, {nameText}, {price}, {sizeText}");
        }
    }

  }
        
    
