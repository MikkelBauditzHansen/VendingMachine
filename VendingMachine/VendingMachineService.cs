using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class VendingMachineService
    {
        
        private  IVendingMachineRepo _productRepo;
        private  PaymentService _paymentService;

        public VendingMachineService(IVendingMachineRepo productRepo, PaymentService paymentService)
        {
            _productRepo = productRepo;
            _paymentService = paymentService;
        }

        public void Add(Product product)
        {
            _productRepo.Add(product);
        }

        public void Delete(Product product)
        {
            _productRepo.Delete(product);
        }

        public List<Product> GetAll()
        {
            return _productRepo.GetAll();
        }
    }
}
