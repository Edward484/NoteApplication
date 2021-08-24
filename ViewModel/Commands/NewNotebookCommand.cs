using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApplication.ViewModel.Commands
{
    public class NewNotebookCommand : ICommand
    {

        public NotesViewModel NVM { get; set; }
        public event EventHandler CanExecuteChanged;

        public NewNotebookCommand(NotesViewModel nvm)
        {
            NVM = nvm;
        }
        public bool CanExecute(object parameter)
        {
            return true;   
        }

        public void Execute(object parameter)
        {
            NVM.CreateNotebook();
        }
    }
}
