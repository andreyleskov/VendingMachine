namespace VendingMachine.UI
{
    using System.ComponentModel;

    public interface IPileViewModelOf<T> : INotifyPropertyChanged
    {
        T Item { get; }

        string ImagePath { get; }

        int Amount { get; set; }
    }
}