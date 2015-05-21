using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineTests
{
    using System.Runtime.InteropServices;

    [TestClass]
    public class CoinTests
    {
        [TestMethod]
        public void Coin_been_created_with_correct_value()
        {
            Assert.AreEqual(1, Coin.Create(CoinGrade.One));
            Assert.AreEqual(2, Coin.Create(CoinGrade.Two));
            Assert.AreEqual(5, Coin.Create(CoinGrade.Five));
            Assert.AreEqual(10, Coin.Create(CoinGrade.Ten));
        }

        [TestMethod]
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
