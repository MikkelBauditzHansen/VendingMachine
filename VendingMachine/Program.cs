using System;
using System.Collections.Generic;
using VendingMachine.Repository;  // IVendingMachineRepo, VendingMachineCollectionRepo
using VendingMachine.Services;   // PaymentService, IPaymentRepo, PaymentCollectionRepo
using VendingMachine.Models;     // Product

namespace VendingMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPaymentRepo paymentRepo = new PaymentCollectionRepo();
            IVendingMachineRepo repo = new VendingMachineCollectionRepo(); // fællesnjj hylde
            PaymentService paymentService = new PaymentService(paymentRepo);
            VendingMachineService vmService = new VendingMachineService(repo, paymentService);

            Console.WriteLine("Skriv rolle: (Admin, Kunde)");
            string menuInput = Console.ReadLine();

            bool running = false;
            bool adminRunning = false;
            int saldo = 0;

            if (menuInput != null && menuInput.ToLower() == "kunde") { running = true; }
            if (menuInput != null && menuInput.ToLower() == "admin") { adminRunning = true; }
            if (!running && !adminRunning) { Console.WriteLine("ugyldig svar"); return; }

            // ===== ADMIN LOOP =====
            while (adminRunning)
            {
                Console.Clear();
                Console.WriteLine("=== SIMPEL VENDING MACHINE ===");
                Console.WriteLine("Bank saldo: " + saldo + " kr.");
                Console.WriteLine();
                PrintStock(repo);
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
                    int belob = Convert.ToInt32(Console.ReadLine());
                    if (belob > 0) { saldo += belob; Console.WriteLine("Du indsatte " + belob + " kr."); }
                    else { Console.WriteLine("Ugyldigt beløb."); }
                }
                else if (adminInput == "2")
                {
                    Console.Write("Indtast beløb: ");
                    int belob = Convert.ToInt32(Console.ReadLine());
                    if (belob > 0) { saldo -= belob; Console.WriteLine("Du hævede " + belob + " kr."); }
                }
                else if (adminInput == "3")
                {
                    AddProduct(repo);   // tilføj N enheder til hylden
                }
                else if (adminInput == "4")
                {
                    RemoveOneById(repo); // fjern én enhed
                }
                else if (adminInput == "0")
                {
                    adminRunning = false;
                }
                else
                {
                    Console.WriteLine("Ugyldigt valg.");
                }

                if (adminRunning)
                {
                    Console.WriteLine();
                    Console.WriteLine("Tryk en tast for at fortsætte...");
                    Console.ReadKey();
                }
            }

            // ===== KUNDE LOOP =====
            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== SIMPEL VENDING MACHINE ===");
                Console.WriteLine("Saldo: " + saldo + " kr.");
                Console.WriteLine();
                PrintStock(repo);
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
                    int belob = Convert.ToInt32(Console.ReadLine());
                    if (belob > 0) { saldo += belob; Console.WriteLine("Du indsatte " + belob + " kr."); }
                    else { Console.WriteLine("Ugyldigt beløb."); }
                }
                else if (input == "2")
                {
                    Console.Write("Indtast produkt-ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    Product chosen = repo.FindById(id); // find én fysisk vare
                    if (chosen == null)
                    {
                        Console.WriteLine("Udsolgt eller ukendt ID!");
                    }
                    else if (saldo < chosen.Price)
                    {
                        Console.WriteLine("For lav saldo.");
                    }
                    else
                    {
                        saldo = saldo - chosen.Price;
                        repo.Delete(chosen); // fjern præcis én enhed
                        Console.WriteLine("Du købte: " + chosen.Name);
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
                    if (saldo > 0) { Console.WriteLine("Returpenge: " + saldo + " kr."); }
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

        // ===== Hjælpemetoder (skal ligge UDEN for Main) =====

        // Tæl antal pr. ID/Name ved at kigge på repoens liste
        private static void PrintStock(IVendingMachineRepo repo)
        {
            List<Product> items = repo.GetAll();

            List<int> ids = new List<int>();
            List<string> names = new List<string>();
            List<int> counts = new List<int>();

            int i = 0;
            while (i < items.Count)
            {
                Product p = items[i];
                int idx = IndexOfId(ids, p.ID);
                if (idx == -1)
                {
                    ids.Add(p.ID);
                    names.Add(p.Name);
                    counts.Add(1);
                }
                else
                {
                    counts[idx] = counts[idx] + 1;
                }
                i = i + 1;
            }

            Console.WriteLine("ID   Navn              Antal");
            Console.WriteLine("-----------------------------");
            int j = 0;
            while (j < ids.Count)
            {
                string line = ids[j].ToString().PadRight(4)
                             + names[j].PadRight(17)
                             + counts[j].ToString();
                Console.WriteLine(line);
                j = j + 1;
            }

            if (ids.Count == 0)
            {
                Console.WriteLine("(Tomt lager)");
            }
        }

        private static int IndexOfId(List<int> ids, int id)
        {
            int k = 0;
            while (k < ids.Count)
            {
                if (ids[k] == id) { return k; }
                k = k + 1;
            }
            return -1;
        }

        // ADMIN: Tilføj N fysiske enheder (individuelle objekter)
        private static void AddProduct(IVendingMachineRepo repo)
        {
            Console.WriteLine("===Admin: Tilføj nyt produkt====");

            Console.Write("ID: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Navn: ");
            string nameText = Console.ReadLine();

            Console.Write("Pris: ");
            int price = Convert.ToInt32(Console.ReadLine());

            Console.Write("Størrelse: ");
            string sizeText = Console.ReadLine();

            Console.Write("Antal (enheder): ");
            int amount = Convert.ToInt32(Console.ReadLine());

            int n = 0;
            while (n < amount)
            {
                Product p = new Product(id, nameText, price, sizeText);
                repo.Add(p);
                n = n + 1;
            }

            Console.WriteLine(amount + " stk " + nameText + " tilføjet.");
        }

        // ADMIN: Fjern én enhed med valgt ID
        private static void RemoveOneById(IVendingMachineRepo repo)
        {
            Console.Write("Angiv ID der skal fjernes (én vare): ");
            int id = Convert.ToInt32(Console.ReadLine());

            Product p = repo.FindById(id);
            if (p == null)
            {
                Console.WriteLine("Ingen vare med det ID på lager.");
            }
            else
            {
                repo.Delete(p);
                Console.WriteLine("Fjernede: " + p.Name);
            }
        }
    }
}
