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
            Assert.AreEqual(new Money(Currency.Rub, 1),  Coin.Create(CoinGrade.One).Value);
            Assert.AreEqual(new Money(Currency.Rub, 2),  Coin.Create(CoinGrade.Two).Value);
            Assert.AreEqual(new Money(Currency.Rub, 5),  Coin.Create(CoinGrade.Five).Value);
            Assert.AreEqual(new Money(Currency.Rub, 10), Coin.Create(CoinGrade.Ten).Value);
        }

        [Test]
        public void Coin_creates_with_right_grade()
        {
            foreach (var grade in new[] { CoinGrade.One, 
                                          CoinGrade.Two,
                                          CoinGrade.Five,
                                          CoinGrade.Ten })
            {
                Coin coin = Coin.Create(grade);
                Assert.AreEqual(grade, coin.Grade);
            }
        }

    }
}
