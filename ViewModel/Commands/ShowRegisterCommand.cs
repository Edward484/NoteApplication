using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApplication.ViewModel.Commands
{
    public class ShowRegisterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public LoginViewModel VM { get; set; }

        public ShowRegisterCommand(LoginViewModel vm)
        {
            VM = vm;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.SwitchViews();
        }
    }
}
