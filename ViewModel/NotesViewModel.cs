using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
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
using System.Windows.Media;
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
                GetNotesAsync();
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

        private FontFamily lastUsedFont;

        public FontFamily LastUsedFont
        {
            get { return lastUsedFont; }
            set { lastUsedFont = value; }
        }

        private Double lastUsedFontSize;

        public Double LastUsedFontSize
        {
            get { return lastUsedFontSize; }
            set { lastUsedFontSize = value; }
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
            LastUsedFont = new("Calibri");
            LastUsedFontSize = 12;

            GetNotebooksAsync();
        }
        public async Task CreateNotebookAsync()
        {
            Notebook newNotebook = new()
            {
                Name = "New notebook",
                UserId = App.userID
            };
            await DataBaseHelper.InsertAsync(newNotebook);

            await GetNotebooksAsync();
        }

        public async Task CreateNoteAsync(string notebookID)
        {
            Note newNote = new()
            {
                NotebookId = notebookID,
                CreatedTime = DateTime.Now,
                LastUpdated = DateTime.Now,
                Title = $"New note{DateTime.Now}"
       
            };
            await DataBaseHelper.InsertAsync(newNote);

            await GetNotesAsync();
        }

        public async Task GetNotebooksAsync()
        {
            var notebooks =  ( await DataBaseHelper.ReadAsync<Notebook>()).Where(n => n.UserId == App.userID).ToList();
            Notebooks.Clear();
            foreach(var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private async Task GetNotesAsync()
        {
            if (selectedNotebook != null)
            {
                var notes = (await DataBaseHelper.ReadAsync<Note>()).Where(n => n.NotebookId == selectedNotebook.Id).ToList();
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
            DataBaseHelper.UpdateAsync(notebook);
            GetNotebooksAsync();
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
            DataBaseHelper.UpdateAsync(note);
            GetNotesAsync();
        }

        public async Task<Paragraph> SpeechToTextAsync()
        {
            string region = "westeurope";
            string key = "fd546a023180466a9f117f7bd812eb25";

            var speechConfig = SpeechConfig.FromSubscription(key, region);
            using (var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
            {
                using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
                {
                    var result = await recognizer.RecognizeOnceAsync();
                    var newParagraph = new Paragraph(new Run(result.Text));
                    newParagraph = ChangeFont(newParagraph);
                    if (LastUsedFontSize == 0)
                        LastUsedFontSize = 12;
                    newParagraph.FontSize = LastUsedFontSize;
                    return newParagraph;
                }
            }
        }

        private Paragraph ChangeFont(Paragraph Paragraph)
        {
            if(Paragraph == null)
                Paragraph = new();
            if(LastUsedFont == null)
                LastUsedFont = new FontFamily("Calibri");
            Paragraph.FontFamily = LastUsedFont;
            OnPropertyChanged("lastUsedFont");
            return Paragraph;
        }

        internal void DeleteNotebook(Notebook notebook)
        {
            string fileName = $"{notebook.Id}.id";
            string filePath = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);
        }

        internal void DeleteNotebook(Note note)
        {
            throw new NotImplementedException();
        }
    }
}
