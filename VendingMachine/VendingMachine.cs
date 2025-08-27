using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
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
