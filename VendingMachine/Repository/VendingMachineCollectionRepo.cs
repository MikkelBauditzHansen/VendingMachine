using System.Collections.Generic;
using VendingMachine.Models;

namespace VendingMachine.Repository
{
    public class VendingMachineCollectionRepo : IVendingMachineRepo
    {
        private readonly List<Product> _shelf;

        public VendingMachineCollectionRepo()
        {
            _shelf = new List<Product>();

            int i = 0;
            while (i < 5) { _shelf.Add(new Product(1, "Cola", 20, "0.33L")); i = i + 1; }
            i = 0;
            while (i < 3) { _shelf.Add(new Product(2, "Sprite", 18, "0.33L")); i = i + 1; }
        }

        public void Add(Product product)
        {
            _shelf.Add(product);
        }

        public List<Product> GetAll()
        {
            return new List<Product>(_shelf);
        }

        public Product FindById(int id)
        {
            int i = 0;
            while (i < _shelf.Count)
            {
                if (_shelf[i].ID == id) { return _shelf[i]; }
                i = i + 1;
            }
            return null;
        }

        public void Delete(Product product)
        {
            _shelf.Remove(product);
        }
    }
}
