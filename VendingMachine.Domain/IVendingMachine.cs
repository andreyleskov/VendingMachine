﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VendingMachine.Domain
{
    using System.Collections.Generic;

    public interface IVendingMachine 
    {
        IReadOnlyList<IShowcaseItem> Showcase { get;}

        Money Balance {get;}

        void Insert(Coin coin);

        Coin[] GetChange();

        IProduct Sell(int number);
    }
}

