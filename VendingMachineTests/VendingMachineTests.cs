using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineTests
{
    using System.Runtime.InteropServices;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using VendingMachine;

    [TestClass]
    public class VendingMachineTests
    {
        //[TestMethod]
        //public void VendingMachine_cant_
    }

    [TestClass]
    public class WalletTests
    {

        [TestMethod]
        public void Empty_wallet_has_zero_total()
        {
            var wallet = new Wallet();
            Assert.AreEqual(Money.Zero, wallet.Total);
        }

        [TestMethod]
        public void Emptied_wallet_has_zero_total()
        {
            var wallet = new Wallet();
            wallet.Put(Coin.One());
            wallet.GetMoney(Coin.One().Value);
            Assert.AreEqual(Money.Zero, wallet.Total);
        }

        [TestMethod]
        public void Added_money_counts_in_total()
        {
            var wallet = new Wallet();
            wallet.Put(Coin.Ten());
            wallet.Put(Coin.One(),
                       Coin.Five());
            
            Assert.AreEqual(14, wallet.Total.Value);
        }

        [TestMethod]
        public void Wallet_returns_added_coins()
        {
            var wallet = new Wallet();
            var coinsToAdd = new[] { Coin.Two(), Coin.Five() };
            wallet.Put();
            var coins = wallet.GetMoney(Money.Rub(7));
            CollectionAssert.AreEquivalent(coinsToAdd,coins);
        }

        [TestMethod]
        public void Wallet_cant_return_coins_it_doesnt_have()
        {
            var wallet = new Wallet();
            var coinsToAdd = new[] { Coin.Five(), Coin.Five() };
            wallet.Put();
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

        [TestMethod]
        public void Left_money_counts_in_total()
        {
            var wallet = new Wallet();
            wallet.Put(Coin.Ten());
            wallet.Put(Coin.One(),
                       Coin.Five());

            wallet.GetMoney(Coin.Five().Value);

            Assert.AreEqual(11, wallet.Total.Value);
        }

        [TestMethod]
        public void Cant_get_money_from_empy_wallet()
        {
            var wallet = new Wallet();
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
