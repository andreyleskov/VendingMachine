namespace VendingMachine.UI
{
    using System;
    using System.Linq;

    using VendingMachine.Domain;

    public class PutCoinCommand : Command<CoinPile>
    {
        private readonly IWallet _wallet;

        private readonly IVendingMachine _machine;

        public PutCoinCommand(IWallet wallet, IVendingMachine machine)
        {
            this._machine = machine;
            this._wallet = wallet;
        }

        public override bool CanExecute(CoinPile parameter)
        {
            return parameter.Amount > 0;
        }

        public override void Execute(CoinPile parameter)
        {
            var coin = _wallet.GetCoinLike(parameter.Coin);
            parameter.Amount --;
            _machine.Insert(coin);
        }
    }
}