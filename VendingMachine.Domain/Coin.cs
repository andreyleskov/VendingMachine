﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VendingMachine.Domain
{
    using System.Collections.Generic;

    public class Coin
    {
        public readonly Money Value;

        private Coin(Money money)
        {
            this.Value = money;
        }

        public override bool Equals(object obj)
        {
            var coin = obj as Coin;
            if (coin == null) return false;

            return coin.Value.Equals(this.Value);
        }

        public static Coin One()  { return new Coin(new Money(Currency.Rub, 100 ));}
        public static Coin Two()  { return new Coin(new Money(Currency.Rub, 200 ));}
        public static Coin Five() { return new Coin(new Money(Currency.Rub, 500 ));}
        public static Coin Ten()  { return new Coin(new Money(Currency.Rub, 1000));}
    }
}