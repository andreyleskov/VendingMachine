using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.UI
{
    using System.Windows;

    public class SimpleInteractionService : IInteractionService
    {
        public void Notify(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
