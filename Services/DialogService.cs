using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using JPO.ViewModel.Services;

namespace JPO.Services
{
    public class DialogService : IDialogService
    {
        public bool Confirm(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question)
                   == MessageBoxResult.Yes;
        }
    }
}
