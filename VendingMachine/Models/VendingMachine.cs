using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    class VendingMachine
    {
        private Payment _payment;

        
        public Payment Payment
        {
            get { return _payment; }
            set { _payment = value; }
        }
    }
}
