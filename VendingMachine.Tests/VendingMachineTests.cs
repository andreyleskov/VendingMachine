using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Tests
{
    using NUnit.Framework;


    using VendingMachine.Domain;
    using VendingMachine.Domain.Products;
    using VendingMachine.Domain.Wallet;

    [TestFixture]
    public class VendingMachineTests
    {

        private NumpadVendingMachine CreateMachine(params ProductBusket[] buskets)
        {
            return new NumpadVendingMachine(new MinCoinsWallet(),
                                            buskets);    
        }

        [Test]
        public void VendingMachine_reduce_customer_balance_by_product_cost()
        {
            var machine = this.CreateMachine(ProductBusket.Of<Tea>(1, Money.Rub(4), 1));

            machine.Insert(Coin.One());
            machine.Insert(Coin.One());
            machine.Insert(Coin.One());
            machine.Insert(Coin.One());
            machine.Insert(Coin.One());

            machine.Sell(1);

            Assert.AreEqual(Money.Rub(1),machine.Balance);
        }

        [Test]
        public void VendingMachine_can_give_change_from_self_coins()
        {
            var machineInitialCoins = new[] { Coin.One(), Coin.Five() };

            var machine = new NumpadVendingMachine( new MinCoinsWallet(machineInitialCoins),
                                                    ProductBusket.Of<Tea>(1, Money.Rub(4), 1));
            machine.Insert(Coin.Ten());
            machine.Sell(1);
            var change = machine.GetChange();

            CollectionAssert.AreEquivalent(machineInitialCoins, change);
        }

        [Test]
        public void VendingMachine_shows_total_sum_after_coin_insert()
        {
            var machine = this.CreateMachine();
            machine.Insert(Coin.Five());
            machine.Insert(Coin.One());

            Assert.AreEqual(Money.Rub(6),machine.Balance);
        }

        [Test]
        public void VendingMachine_without_buy_product_gives_change_for_all_inserted_coins()
        {
            var machine = this.CreateMachine();
            var coins = new [] {Coin.Five(), Coin.Ten()};

            machine.Insert(coins[0]);
            machine.Insert(coins[1]);

            var change = machine.GetChange();
            CollectionAssert.AreEquivalent(coins, change);
        }

        [Test]
        public void VendingMachine_gave_no_change_on_zero_balance()
        {
            var machine = this.CreateMachine();
            var coins = machine.GetChange();

            Assert.IsFalse(coins.Any());
        }

        [Test]
        public void VendingMachine_initial_total_is_zero()
        {
            var machine = this.CreateMachine();
            Assert.AreEqual(Money.Zero, machine.Balance);

        }
    }
}
