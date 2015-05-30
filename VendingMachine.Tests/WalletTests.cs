namespace VendingMachine.Tests
{
    using System;

    using NUnit.Framework;

    using VendingMachine;
    using VendingMachine.Domain;
    using VendingMachine.Domain.Wallet;


    [TestFixture]
    public class WalletTests
    {

        [Test]
        public void Empty_wallet_has_zero_total()
        {
            var wallet = new MinCoinsWallet();
            Assert.AreEqual(Money.Zero, wallet.Total);
        }

        [Test]
        public void Wallet_shows_coins_that_it_contains()
        {
            var wallet = new MinCoinsWallet();
 
            var coins = new []{Coin.Five(),Coin.Ten()};

            wallet.Put(coins[0]);
            wallet.Put(coins[1]);

            CollectionAssert.AreEquivalent(coins, wallet.Coins);
        }

        [Test]
        public void Wallet_take_money_by_minimal_coins_number()
        {
            var wallet = new MinCoinsWallet();
            var walletCoins = new []{Coin.One(),
                                     Coin.One(),
                                     Coin.One(),
                                     Coin.One(),
                                     Coin.One(),
                                     Coin.Two(),
                                     Coin.Two(),
                                     Coin.Two()};
                         
            wallet.Put(walletCoins);
            var coins = wallet.GetMoney(Money.Rub(5));
            var expectedCoins = new[] { walletCoins[5],walletCoins[6],walletCoins[0]};
            CollectionAssert.AreEquivalent(expectedCoins, coins);
        }

        [Test]
        public void Emptied_wallet_has_zero_total()
        {
            var wallet = new MinCoinsWallet();
            wallet.Put(Coin.One());
            wallet.GetMoney(Coin.One().Value);
            Assert.AreEqual(Money.Zero, wallet.Total);
        }

        [Test]
        public void Added_money_counts_in_total()
        {
            var wallet = new MinCoinsWallet();
            wallet.Put(Coin.Ten());
            wallet.Put(Coin.One(),
                       Coin.Five());
                
            Assert.AreEqual(1600, wallet.Total.Value);
        }

        [Test]
        public void Wallet_returns_added_coins()
        {
            var wallet = new MinCoinsWallet();
            var coinsToAdd = new[] { Coin.Two(), Coin.Five() };
            wallet.Put(coinsToAdd);
            var coins = wallet.GetMoney(Money.Rub(7));
            CollectionAssert.AreEquivalent(coinsToAdd,coins);
        }

        [Test]
        public void Wallet_doesnt_change_after_not_anough_coins_error_occured()
        {
            var wallet = new MinCoinsWallet();
            wallet.Put(Coin.Two(), Coin.Five());

            try
            {
                 wallet.GetMoney(Money.Rub(10));
            }
            catch (NotAnoughMoneyException)
            {
            }

            Assert.AreEqual(Money.Rub(7), wallet.Total);
        }

        [Test]
        public void Wallet_cant_return_coins_it_doesnt_have()
        {
            var wallet = new MinCoinsWallet();
            var coinsToAdd = new[] { Coin.Five(), Coin.Five() };
            wallet.Put(coinsToAdd);
            //TODO: убрать try
            try
            {
                wallet.GetMoney(Money.Rub(1));
            }
            catch (NotAnoughCoinsException)
            {
                return;
            }
            Assert.Fail("Не было выброшено исключение при попытке снять сумму, которая не может быть скомплектованна имеющимися монетами");
        }

        [Test]
        public void Left_money_counts_in_total()
        {
            var wallet = new MinCoinsWallet();
            wallet.Put(Coin.Ten());
            wallet.Put(Coin.One(),
                Coin.Five());

            wallet.GetMoney(Coin.Five().Value);

            Assert.AreEqual(1100, wallet.Total.Value);
        }

        [Test]
        public void Cant_get_money_from_empy_wallet()
        {
            var wallet = new MinCoinsWallet();
            //TODO: убрать TODO
            try
            {
                wallet.GetMoney(Coin.One().Value);
            }
            catch (NotAnoughMoneyException)
            {
                return;
            }
            Assert.Fail("Не было вброшено исключение при попытке взять деньги из пустого кошелька");
        }
    }
}