using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApplication.ViewModel.Commands
{
    public class OpenWeatherWindowCommand : ICommand
    {
        NotesViewModel VM;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public OpenWeatherWindowCommand(NotesViewModel vm)
        {
            VM = vm;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.OpenWeatherWindow();
        }
    }
}
