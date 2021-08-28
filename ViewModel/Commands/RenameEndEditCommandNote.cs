using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NoteApplication.ViewModel.Commands
{
    public class RenameEndEditCommandNote : ICommand
    {
        public event EventHandler CanExecuteChanged;

        NotesViewModel VM { get; set; }

        public RenameEndEditCommandNote(NotesViewModel vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Note note = parameter as Note;
            if (note != null)
                VM.StopEditingRenameNote(note);
        }
    }
}
