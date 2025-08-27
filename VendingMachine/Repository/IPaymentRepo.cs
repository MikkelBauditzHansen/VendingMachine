using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Repository
{
    public interface IPaymentRepo
    {
        public List<Payment> GetAll();
        public void Add(Payment payment);
        public void Delete(Payment payment);
    }
}
