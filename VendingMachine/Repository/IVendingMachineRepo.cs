using System.Collections.Generic;
using VendingMachine.Models;

namespace VendingMachine.Repository
{
    public interface IVendingMachineRepo
    {
        void Add(Product product);          // læg én fysisk vare på hylden
        List<Product> GetAll();             // snapshot af hylden
        Product FindById(int id);           // find første vare med ID
        void Delete(Product product);       // fjern præcis den vare
    }
}
