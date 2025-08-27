using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class VendingMachineCollectionRepo : IVendingMachineRepo
    {
        private readonly List<Product> _products;

        public VendingMachineCollectionRepo()
        { }

        public void Add(Product product)
        {
            _products.Add(product);
        }
        public List<Product> GetAll()
        {
            return new List<Product>(_products);
        }
        public void Delete(Product product)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                if (_products[i].ID == product.ID)
                {
                    _products.RemoveAt(i);
                    break;
                }
            }
        }
        public Product FindById(int id)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                if (_products[i].ID == id)
                {
                    return _products[i];
                }
            }
            return null;
        }
        public void Update(Product product)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                if (_products[i].ID == product.ID)
                {
                    _products[i] = product;
                    break;
                }
            }
        }
    }
}

