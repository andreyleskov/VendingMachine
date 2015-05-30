namespace VendingMachine.UI
{
    using System.Collections.Generic;
    using System.Windows.Input;

    using Microsoft.Practices.Prism.Commands;

    using VendingMachine.Domain;

    class WalletViewModel
    {
        public CoinPile ChosenPile { get; set; }

        public Money MoneyTotal { get; private set; }

        public IEnumerable<CoinPile> WalletContent { get; private set; }

        private List<CoinPile> _walletContent = new List<CoinPile>(); 

        public WalletViewModel(IWallet wallet)
        {
            GetChosenCoinCommand = new DelegateCommand(GetCoin, CanGetCoin);
            _wallet = wallet;

        }

        private bool CanGetCoin()
        {
            return _wallet
        }

        private void GetCoin()
        {
            throw new System.NotImplementedException();
        }

        public ICommand GetChosenCoinCommand { get; private set; }
        
    }
}