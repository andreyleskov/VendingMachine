namespace VendingMachine.Domain
{
    using System;

    public class MoneyCurrencyMismatchException : Exception
    {
        #region Constructors and Destructors

        public MoneyCurrencyMismatchException()
            : base("���� ����� �� ���������")
        {
        }

        #endregion
    }
}