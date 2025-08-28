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


            int saldo = 0;
            bool running = true;

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
                        if (chosen == null)
                        {
                            Console.WriteLine("Ukendt produkt.");
                        }
                        else if (chosen.Quantity <= 0)
                        {
                            Console.WriteLine("Udsolgt!");
                        }
                        else if (saldo < chosen.Price)
                        {
                            Console.WriteLine("For lav saldo.");
                        }
                        else
                        {
                            saldo -= chosen.Price;
                            chosen.Quantity -= 1;
                            Console.WriteLine("Du købte: " + chosen.Name);
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

        static void PrintProducts(List<Product> products)
        {
            Console.WriteLine("ID  Navn        Pris   Str.   Lager");
            Console.WriteLine("-------------------------------------");
            foreach (Product p in products)
            {
                Console.WriteLine(p.ID + "   " + p.Name.PadRight(10) + p.Price + "kr   " + p.Size.PadRight(5) + "   " + p.Quantity);
            }
        }

        static Product FindById(List<Product> products, int id)
        {
            foreach (Product p in products)
            {
                if (p.ID == id) return p;
            }
            return null;
        }
    }

    class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }

        public Product(int id, string name, int price, string size, int quantity)
        {
            ID = id;
            Name = name;
            Price = price;
            Size = size;
            Quantity = quantity;
        }
    }
}
        
    
