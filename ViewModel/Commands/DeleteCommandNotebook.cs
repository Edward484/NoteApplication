using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApplication.ViewModel.Commands
{
    public class DeleteCommandNotebook : ICommand
    {
        public NotesViewModel VM { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DeleteCommandNotebook(NotesViewModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            if(VM.Notes.Count > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            Notebook notebook = parameter as Notebook;
            VM.DeleteNotebookAsync(notebook);
        }
    }
}
