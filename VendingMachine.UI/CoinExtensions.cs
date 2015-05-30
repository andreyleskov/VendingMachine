namespace VendingMachine.UI
{
    using System.Collections.Generic;
    using System.Linq;

    using VendingMachine.Domain;

    public static class CoinExtensions
    {
        public static IEnumerable<CoinPile> ToPiles(this IEnumerable<Coin> coins)
        {
            return coins.GroupBy(c => c.Value).Select(g => new CoinPile() { Value = g.Key, Amount = g.Count() });
        }   
    }
}