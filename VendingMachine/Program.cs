namespace VendingMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
          

            bool running = true;
            while (running == true)
            {
                Console.Clear();
                Console.WriteLine("=== VENDING MACHINE ===");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Skriv et tal som følgende");
                Console.WriteLine("1) Indsæt penge");
                Console.WriteLine("2) Køb vare (skriv ID)");
                Console.WriteLine("3) Få returpenge");
                Console.WriteLine("0) Afslut");
                Console.Write("Vælg: ");
            }
        }
    }
}
