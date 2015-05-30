﻿namespace VendingMachine.UI
{
    using VendingMachine.Domain;
    using VendingMachine.Domain.Wallet;

    public class DesignTimeMainWindowViewModel : MainWindowViewModel
    {

        private static readonly IWallet MachineWallet;

        private static readonly IWallet CustomerWallet;

        private static readonly IVendingMachine Machine;

        static DesignTimeMainWindowViewModel()
        {
            MachineWallet = new MinCoinsWallet(CoinsBuilder.New().PileOf(Coin.One).Size(5)
                                                                 .PileOf(Coin.Two).Size(4)
                                                                 .PileOf(Coin.Five).Size(3)
                                                                 .PileOf(Coin.Ten).Size(2)
                                                          .GetCoins());

            CustomerWallet = new MinCoinsWallet(CoinsBuilder.New().PileOf(Coin.One).Size(10)
                                                                  .PileOf(Coin.Five).Size(3)
                                                                  .PileOf(Coin.Ten).Size(2)
                                                           .GetCoins());

            Machine = new NumpadVendingMachine(MachineWallet);
        }

        public DesignTimeMainWindowViewModel(): base(Machine, MachineWallet, CustomerWallet)
        {

        }
    }
}