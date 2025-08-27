using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Repository
{
    public interface IVendingMachineRepo
    {
        void Add(Product product);
        void Delete(Product product);
        List<Product> GetAll();
        Product FindById(int id);
        void Update(Product product);

    }
}
