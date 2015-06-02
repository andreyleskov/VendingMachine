namespace VendingMachine.UI
{
    using System;
    using System.Windows.Input;

    public class Command<T> : ICommand
    {

        public virtual bool CanExecute(T parameter)
        {
            return true;
        }

        public bool CanExecute(object parameter)
        {
            return parameter is T && this.CanExecute((T)parameter);
        }

        public virtual void Execute(T parameter)
        {
            
        }

        public void Execute(object parameter)
        {
            this.Execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}