using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Domain
{
    class Customer
    {
        public IWallet _wallet { get; private set; }
        public List<IProduct> Products { get; private set; }
        public Customer(IWallet wallet)
        {
            this._wallet = wallet;
            Products = new List<IProduct>();
        }
    }
}
