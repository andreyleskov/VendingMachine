using System;

namespace VendingMachine.Tests
{
    using System.Runtime.InteropServices;

    using NUnit.Framework;

    using VendingMachine.Domain;


    [TestFixture]
    public class CoinTests
    {
        [Test]
        public void Coin_been_created_with_correct_value()
        {
            Assert.AreEqual(new Money(Currency.Rub, 100),  Coin.One().Value);
            Assert.AreEqual(new Money(Currency.Rub, 200),  Coin.Two().Value);
            Assert.AreEqual(new Money(Currency.Rub, 500),  Coin.Five().Value);
            Assert.AreEqual(new Money(Currency.Rub, 1000), Coin.Ten().Value);
        }
    }
}
