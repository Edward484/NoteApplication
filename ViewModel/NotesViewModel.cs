using NoteApplication.Model;
using NoteApplication.ViewModel.Commands;
using NoteApplication.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.ViewModel
{
    public class NotesViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public Notebook selectedNotebook;


        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            { 
                selectedNotebook = value;
                OnPropertyChanged("selectedNotebook");
                GetNotes();
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public NotesViewModel()
        {
            NewNotebookCommand = new(this);
            NewNoteCommand = new(this);

            Notebooks = new();
            Notes = new();

            GetNotebooks();
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new()
            {
                Name = "New notebook"
            };
            DataBaseHelper.Insert(newNotebook);

            GetNotebooks();
        }

        public void CreateNote(int notebookID)
        {
            Note newNote = new()
            {
                NotebookId = notebookID,
                CreatedTime = DateTime.Now,
                LastUpdated = DateTime.Now,
                Title = $"New note{DateTime.Now}"
       
            };
            DataBaseHelper.Insert(newNote);

            GetNotes();
        }

        private void GetNotebooks()
        {
            var notebooks =  DataBaseHelper.Read<Notebook>();
            Notebooks.Clear();
            foreach(var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private void GetNotes()
        {
            if (selectedNotebook != null)
            {
                var notes = DataBaseHelper.Read<Note>().Where(n => n.NotebookId == selectedNotebook.Id).ToList();
                Notes.Clear();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
