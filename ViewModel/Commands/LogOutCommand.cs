using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApplication.ViewModel.Commands
{
    public class LogOutCommand : ICommand
    {
        public NotesViewModel VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public LogOutCommand(NotesViewModel vm)
        {
            VM = vm;
        }
        public bool CanExecute(object parameter)
        {
            if (App.userID != null)
                return true;
            else
                return false;
        }

        public void Execute(object parameter)
        {
            VM.LogOut();
        }
    }
}
