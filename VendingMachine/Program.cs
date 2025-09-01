using System;
using System.IO;
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
            IVendingMachineRepo repo = new VendingMachineJsonRepo();
            PaymentService paymentService = new PaymentService(paymentRepo);
            VendingMachineService vmService = new VendingMachineService(repo, paymentService);


            bool adminRunning = false;
            bool running = false;
            int saldo = 0;
            int bankSaldo = 1000;
            // ===== ADMIN LOOP =====

            Console.WriteLine("Velkommen til Vending Machine, skriv kunde eller admin");
            string inputChoice = Console.ReadLine();


            if (inputChoice.ToLower() == "admin")
            {
                Console.Write("Indtast admin-kode: ");
                string code = Console.ReadLine();
                if (code == "1235")
                {
                    adminRunning = true;
                }
                else
                {
                    Console.WriteLine("Forkert kode. Adgang nægtet.");
                    return;
                }
            }
            if(inputChoice.ToLower() == "kunde")
            {
                running = true;
            }

            while (adminRunning)
            {
                Console.Clear();
                Console.WriteLine("=== Admin System ===");
                Console.WriteLine("Saldo: " + bankSaldo + " kr.");
                Console.WriteLine();
                PrintStock(repo);
                Console.WriteLine();
                Console.WriteLine("1) Hæv penge");
                Console.WriteLine("2) Tilføj penge");
                Console.WriteLine("3) Tilføj varer");
                Console.WriteLine("4) Fjern varer");
                Console.WriteLine("5) Fjern alle varer med et ID");
                Console.WriteLine("6) Tøm hele hylden");
                Console.WriteLine("0) Afslut");
                Console.Write("Vælg: ");

                string adminChoice = Console.ReadLine();

                if (adminChoice == "1")
                {
                    Console.Write("Indtast beløb: ");
                    string belobTxt = Console.ReadLine();
                    int belob;
                    try
                    {
                        belob = Convert.ToInt32(belobTxt);
                    }
                    catch
                    {
                        Console.WriteLine("Ugyldigt beløb.");
                        Pause();
                        continue;
                    }

                    if (belob <= 0)
                    {
                        Console.WriteLine("Beløbet skal være positivt.");
                    }
                    else if (belob > bankSaldo)
                    {
                        Console.WriteLine("Kan ikke hæve mere end bank saldo.");
                    }
                    else
                    {
                        bankSaldo = bankSaldo - belob;
                        Console.WriteLine("Du hævede " + belob + " kr. Ny bank saldo: " + bankSaldo + " kr.");
                    }
                    Pause();
                }
                if (adminChoice == "2")
                {
                    int belob = PromptInt("Indtast beløb:");
                    if (belob <= 0) Console.WriteLine("Beløbet skal være positivt.");
                    else { bankSaldo = bankSaldo + belob; Console.WriteLine("Du indsatte " + belob + " kr. Ny bank saldo: " + bankSaldo + " kr."); }
                    Pause();
                }
                else if (adminChoice == "3")
                {
                    int id = PromptInt("ID:");
                    Console.Write("Navn: ");
                    string nameText = Console.ReadLine();
                    int price = PromptInt("Pris:");
                    int amount = PromptInt("Antal (enheder):");

                    if (amount <= 0)
                    {
                        Console.WriteLine("Antal skal være positivt.");
                        Pause();
                        continue;
                    }

                    int n = 0;
                    while (n < amount)
                    {
                        Product p = new Product(id, nameText, price);
                        repo.Add(p);
                        n = n + 1;
                    }

                    Console.WriteLine(amount + " stk " + nameText + " tilføjet.");
                    Pause();
                }
                else if (adminChoice == "4")
                {
                    int id = PromptInt("Indtast produkt-ID:");
                    Product chosen = repo.FindById(id);
                    if (chosen == null)
                    {
                        Console.WriteLine("Udsolgt eller ukendt ID!");
                        Pause();
                        continue;
                    }

                    Console.WriteLine("Er du sikker? Du har valgt " + chosen.Name + " (ja/nej)");
                    string confirm = Console.ReadLine();
                    if (confirm == "ja")
                    {
                        repo.Delete(chosen);
                        Console.WriteLine("Fjernet: " + chosen.Name);
                    }
                    else if (confirm == "nej")
                    {
                        Console.WriteLine("Annulleret.");
                    }
                    else
                    {
                        Console.WriteLine("Ugyldigt svar.");
                    }
                    Pause();
                }
                else if (adminChoice == "5")
                {
                    AdminRemoveAllById(repo);
                    Pause();
                }
                else if (adminChoice == "6")
                {
                    AdminClearAll(repo);
                    Pause();
                }
                else if (adminChoice == "0")
                {
                    Console.WriteLine("Tak for besøget!");
                    if (saldo > 0) { Console.WriteLine("Returpenge: " + saldo + " kr."); }
                    adminRunning = false;
                    return;
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
                        string idText = Console.ReadLine();

                        int id;
                        try
                        {
                            id = Convert.ToInt32(idText);
                        }
                        catch
                        {
                            Console.WriteLine("Ugyldigt ID. Skriv et tal.");
                            Console.WriteLine("Tryk en tast for at fortsætte...");
                            Console.ReadKey();
                            continue;
                        }

                        Product chosen = vmService.FindById(id); // find én fysisk vare
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
                            Console.WriteLine($"Er du sikker på dit valg? du har valgt {chosen.Name}(ja eller nej)");
                            string confirm = Console.ReadLine();
                            if (confirm == "ja")
                            {
                                saldo = saldo - chosen.Price;
                                vmService.Delete(chosen); // fjern præcis én enhed
                                Console.WriteLine("Du købte: " + chosen.Name);
                            }
                            else if (confirm == "nej")
                            {
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Ugyldigt Svar.");
                                Console.WriteLine("Tryk en tast for at fortsætte...");
                                Console.ReadKey();
                                continue;
                            }

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

        private static void PrintStock(IVendingMachineRepo repo)
        {
            List<Product> items = repo.GetAll();

            List<int> ids = new List<int>();
            List<string> names = new List<string>();
            List<int> price = new List<int>();
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
                    price.Add(p.Price);
                    counts.Add(1);
                }
                else
                {
                    counts[idx] = counts[idx] + 1;
                }
                i = i + 1;
            }

            Console.WriteLine("ID   Navn   pris i kr.   Antal");
            Console.WriteLine("----------------------------------");
            int j = 0;
            while (j < ids.Count)
            {
                string line = ids[j].ToString().PadRight(5)
                             + names[j].PadRight(10)
                             + price[j].ToString().PadRight(12)
                             + counts[j].ToString();
                Console.WriteLine(line);
                j = j + 1;
            }

            if (ids.Count == 0)
            {
                Console.WriteLine("(Tomt lager)");
            }
        }
        private static void Pause()
        {
            Console.WriteLine("Tryk en tast for at fortsætte...");
            Console.ReadKey();
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
        private static void AdminRemoveAllById(IVendingMachineRepo repo)
        {
            int id = PromptInt("Indtast produkt-ID som skal fjernes (alle med dette ID):");

            // Tag et snapshot, gennemløb det og slet alle matches fra repo
            List<Product> items = repo.GetAll();
            int i = 0;
            int removed = 0;

            while (i < items.Count)
            {
                Product p = items[i];
                if (p.ID == id)
                {
                    repo.Delete(p);
                    removed = removed + 1;
                }
                i = i + 1;
            }

            if (removed == 0)
            {
                Console.WriteLine("Ingen varer fundet med ID " + id + ".");
            }
            else
            {
                Console.WriteLine("Fjernede " + removed + " vare(r) med ID " + id + ".");
            }
        }
        private static void AdminClearAll(IVendingMachineRepo repo)
        {
            List<Product> items = repo.GetAll(); // snapshot
            int i = 0;
            int removed = 0;

            while (i < items.Count)
            {
                repo.Delete(items[i]);
                removed = removed + 1;
                i = i + 1;
            }
            Console.WriteLine("Hylden er tømt (" + removed + " vare(r)).");
        }
        private static int PromptInt(string label)
        {
            while (true)
            {
                Console.Write(label + " ");
                string txt = Console.ReadLine();
                try
                {
                    return Convert.ToInt32(txt);
                }
                catch
                {
                    Console.WriteLine("Ugyldigt tal. Prøv igen.");
                }
            }
        }
    }
}
