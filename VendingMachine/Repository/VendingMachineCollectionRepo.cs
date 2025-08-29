using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Repository
{
    class VendingMachineCollectionRepo : IVendingMachineRepo
    {
        private readonly List<Product> _cokes;

        public VendingMachineCollectionRepo()
        {
            _cokes = new List<Product>();
            for (int i = 0;  i < 10; i++)
            {
                Product cola = new Product(1, "Cola", 0, "0.33L");
                _cokes.Add(cola);
            }
            
        }
        

        public void Add(Product product)
        {
            _cokes.Add(product);
        }
        public List<Product> GetAll()
        {
            return new List<Product>(_cokes);
        }
        public void Delete(Product product)
        {
            for (int i = 0; i < _cokes.Count; i++)
            {
                if (_cokes[i].ID == product.ID)
                {
                    _cokes.RemoveAt(i);
                    break;
                }
            }
        }
        public Product FindById(int id)
        {
            for (int i = 0; i < _cokes.Count; i++)
            {
                if (_cokes[i].ID == id)
                {
                    return _cokes[i];
                }
            }
            return null;
        }
        public void Update(Product product)
        {
            for (int i = 0; i < _cokes.Count; i++)
            {
                if (_cokes[i].ID == product.ID)
                {
                    _cokes[i] = product;
                    break;
                }
            }
        }
    }
}

