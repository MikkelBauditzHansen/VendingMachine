using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Repository
{
    class VendingMachineCollectionRepo : IVendingMachineRepo
    {
        private readonly List<Product> _products;

        public VendingMachineCollectionRepo()
        {
            _products = new List<Product>();

            Product cola = new Product(1, "Cola", 20, "0.33L", 4);
            _products.Add(cola);

            Product sprite = new Product(2,"Sprite", 18, "0.33L", 5);
            _products.Add(sprite);

            Product mars = new Product(3,"MarsBar", 15, "51g", 6);
            _products.Add(mars);

            Product twix = new Product(4,"TwixBar", 15, "50g", 3);
            _products.Add(twix);
        }

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

