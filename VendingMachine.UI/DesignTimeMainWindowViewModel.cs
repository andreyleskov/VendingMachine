namespace VendingMachine.UI
{
    using VendingMachine.Domain;
    using VendingMachine.Domain.Products;
    using VendingMachine.Domain.Wallet;


    public static class TestData
    {
        public static MainWindowViewModel MainWindowViewModel
        {
           get
           {
               return AdjustableMainWindowViewModel(new SimpleInteractionService());
           }
        }

        public static MainWindowViewModel AdjustableMainWindowViewModel(IInteractionService interactionService)
        {
            var machineWallet = new MinCoinsWallet(CoinsBuilder.New().PileOf(Coin.One).Size(5)
                                                                           .PileOf(Coin.Two).Size(4)
                                                                           .PileOf(Coin.Five).Size(3)
                                                                           .PileOf(Coin.Ten).Size(2)
                                                                .GetCoins());

            var customerWallet = new MinCoinsWallet(CoinsBuilder.New().PileOf(Coin.One).Size(10)
                                                                      .PileOf(Coin.Five).Size(3)
                                                                      .PileOf(Coin.Ten).Size(20)
                                                            .GetCoins());

            var machine = new NumpadVendingMachine(machineWallet, new[]
                                                                        {
                                                                            ProductBusket.Of<Tea>(1,Money.Rub(10),2),
                                                                            ProductBusket.Of<Coffe>(2,Money.Rub(20),3),
                                                                            ProductBusket.Of<Juice>(3,Money.Rub(30),1),
                                                                        });

            return new MainWindowViewModel(machine, machineWallet, customerWallet, interactionService);
        }
    }

}