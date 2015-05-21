﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum Currency
{
    Rub,
    Fake
}

public class Money
{
    public readonly Currency Currency;

    public readonly decimal Value;

    public Money(Currency currency, decimal value)
    {
        Currency = currency;
        Value = value;
    }

    
    public static readonly Money Zero = new Money(Currency.Rub, 0);

    public static Money Rub(decimal value)
    {
        return new Money(global::Currency.Rub, value);
    }

    //TODO: proper implementation
    public override bool Equals(object obj)
    {
        if(!(obj is Money)) return false;
        Money otherMoney = (Money)obj;

        return Currency.Equals(otherMoney.Currency) && otherMoney.Value.Equals(Value);
    }

    //TODO: proper implementation
    public override int GetHashCode()
    {
        return Currency.GetHashCode() ^ Value.GetHashCode();
    }
}

