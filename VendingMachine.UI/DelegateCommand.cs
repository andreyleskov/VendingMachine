namespace VendingMachine.UI
{
    using System;

    public class DelegateCommand<T> : Command<T>
    {
        private readonly Func<T, bool> _canExecute;

        private readonly Action<T> _execute;

        public DelegateCommand(Action<T> execute, Func<T,bool> canExecute = null)
        {
            this._execute = execute;
            this._canExecute = canExecute ?? (p => true);
        }

        public override bool CanExecute(T parameter)
        {
            return this._canExecute(parameter);
        }

        public override void Execute(T parameter)
        {
            this._execute(parameter);
        }
    }
}