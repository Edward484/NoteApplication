using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApplication.ViewModel.Commands
{
    class RegisterCommand : ICommand
    {
        public LoginViewModel LVM { get; set; }
        public event EventHandler CanExecuteChanged;

        public RegisterCommand(LoginViewModel lvm)
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
