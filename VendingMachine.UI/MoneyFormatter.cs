namespace VendingMachine.UI
{
    using VendingMachine.Domain;

    public static class MoneyFormatter
    {
        public static string ValueString(this Money money)
        {
            return string.Format("{0}.{1}", money.Value / 100, money.Value % 100);
        }

        public static string CurrencyString(this Money money)
        {
            return money.Currency == Domain.Currency.Rub ? "Руб" : "";
        }

        public static string ToUIString(this Money money)
        {
            return ValueString(money) + " " + CurrencyString(money);
        }
    }
}