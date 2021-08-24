using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApplication.ViewModel.Commands
{
    class LoginCommand : ICommand
    {
        public LoginViewModel LVM { get; set; }
        public event EventHandler CanExecuteChanged;

        public LoginCommand(LoginViewModel lvm)
        {
            LVM = lvm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
