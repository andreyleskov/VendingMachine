using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    interface IWallet
    {
        Coin[] GetMoney(Money amount);

        void Put(params Coin[] coin);

        Money Total { get; }
    }
}
