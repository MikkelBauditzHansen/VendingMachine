using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Models
{
    class Role
    {
        private bool _admin;

        private bool _costumer;

        public bool Admin
        {
            get { return _admin; }
            set { _admin = value; }
        }
        public bool Costumer
        {
            get { return _costumer; }
            set { _costumer = value; }
        }

        public Role(bool admin, bool costumer)
        {
            Admin = admin;
            Costumer = costumer;
            
        }
    }
}
