using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Tests
{
    using NUnit.Framework;


    using VendingMachine.Domain;


    [TestFixture]
    public class VendingMachineTests
    {
        [Test]
        public void VendingMachine_shows_total_sum_after_coin_insert()
        {
            var machine = new VendingMachine();
            machine.Insert(Coin.Five());
            machine.Insert(Coin.One());

            Assert.AreEqual(Money.Rub(6),machine.Balance);
        }

        [Test]
        public void VendingMachine_without_buy_product_gives_change_for_all_inserted_coins()
        {
            var machine = new VendingMachine();
            var coins = new [] {Coin.Five(), Coin.Ten()};

            machine.Insert(coins[0]);
            machine.Insert(coins[1]);

            var change = machine.GetChange();
            CollectionAssert.AreEqual(coins, change);
        }

        [Test]
        public void VendingMachine_gave_no_change_on_zero_balance()
        {
            var machine = new VendingMachine();
            var coins = machine.GetChange();

            Assert.IsFalse(coins.Any());
        }

        [Test]
        public void VendingMachine_initial_total_is_zero()
        {
            var machine = new VendingMachine();
            Assert.AreEqual(Money.Zero, machine.Balance);

        }
    }
}
