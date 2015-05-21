using System;
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

    /// <summary>
    /// Кошелёк с логикой выдачи запрошенной суммы минимальным количеством монет. 
    /// </summary>
    public class Wallet : IWallet
    {

        public Coin[] GetMoney(Money amount)
        {
            throw new NotImplementedException();
        }

        public void Put(params Coin[] coin)
        {
            throw new NotImplementedException();
        }

        public Money Total { get; private set; }
    }

    public class NotAnoughMoneyException : Exception
    {
        public NotAnoughMoneyException():base("Недостаточно денег")
        {
            
        }
    }

    public class NotAnoughCoinsException : Exception
    {
        public NotAnoughCoinsException()
            : base("Недостаточно монет для запрошенной суммы")
        {
        }
    }
}
