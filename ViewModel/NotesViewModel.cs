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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using Xceed.Wpf.Toolkit;

namespace NoteApplication.ViewModel
{
    public class NotesViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;

        public ExitCommand ExitCommand { get; set; }
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }

        public RenameEditCommandNotebook RenameEditCommandNotebook { get; set; }
        public RenameEndEditCommandNotebook RenameEndEditCommandNotebook { get; set; }

        public RenameEditCommandNote RenameEditCommandNote { get; set; }

        public RenameEndEditCommandNote RenameEndEditCommandNote { get; set; }
        public ToggleButton BoldButton { get; set; }

        private Visibility notebookRenameVisibility;

        public Visibility NotebookRenameVisibility
        {
            get { return notebookRenameVisibility; }
            set 
            { 
                notebookRenameVisibility = value;
                OnPropertyChanged("notebookRenameVisibility");
            }
        }

        private Visibility noteRenameVisibility;

        public Visibility NoteRenameVisibility
        {
            get { return noteRenameVisibility; }
            set
            {
                noteRenameVisibility = value;
                OnPropertyChanged("noteRenameVisibility");
            }
        }

        private string content;
        public string Content
        {
            get { return content; }
            set 
            { 
                content = value;
                OnPropertyChanged("content");
            }
        }

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

        private Note selectedNote;
        public  Note SelectedNote
        {
            get { return selectedNote; }
            set 
            { 
                selectedNote = value;
                OnPropertyChanged("selectedNote");
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }


        public BoldCommand BoldCommand { get; set; }
        public DependencyProperty FontWeightProperty { get; private set; }



        public NotesViewModel()
        {
            NewNotebookCommand = new(this);
            NewNoteCommand = new(this);
            BoldCommand = new(this);
            ExitCommand = new(this);
            RenameEditCommandNotebook = new(this);
            RenameEndEditCommandNotebook = new(this);
            RenameEndEditCommandNote = new(this);
            RenameEditCommandNote = new(this);

            Notebooks = new();
            Notes = new();
            NotebookRenameVisibility = Visibility.Collapsed;
            NoteRenameVisibility = Visibility.Collapsed;

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

        public void MakeSelectedTextBold(object sender)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false; 
            
        }


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public void StartEditingRenameNotebook(Notebook notebook)
        {
            if (SelectedNotebook.Id == notebook.Id)
            {
                NotebookRenameVisibility = Visibility.Visible;
            }
            
        }
        public void StopEditingRenameNotebook(Notebook notebook)
        {
            NotebookRenameVisibility = Visibility.Collapsed;
            DataBaseHelper.Update(notebook);
            GetNotebooks();
        }
        public void StartEditingRenameNote(Note note)
        {
            if (SelectedNote.Id == note.Id)
            {
                NoteRenameVisibility = Visibility.Visible;
            }

        }
        public void StopEditingRenameNote(Note note)
        {
            NoteRenameVisibility = Visibility.Collapsed;
            DataBaseHelper.Update(note);
            GetNotes();
        }
    }
}
