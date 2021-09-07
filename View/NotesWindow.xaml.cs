using Azure.Storage.Blobs;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using NoteApplication.ViewModel;
using NoteApplication.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoteApplication.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window 
    {
        NotesViewModel viewModel; 
        public NotesWindow()
        {
            InitializeComponent();

            viewModel = Resources["vm"] as NotesViewModel;
            viewModel.SelectedNoteChanged += ViewModel_SelectedNoteChanged;

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f=> f.Source);
            fontFamilyComboBox.ItemsSource = fontFamilies;

            List<double> fontSizes = new() { 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 18, 20, 22, 24, 26 };
            fontSizeComboBox.ItemsSource = fontSizes;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (string.IsNullOrEmpty(App.userID))
            {
                LoginWindows loginWindow = new();
                loginWindow.ShowDialog();
                

                viewModel.GetNotebooksAsync();
            }
        }



        private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ammountCharacters = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd).Text.Length;
            statusTextBlock.Text = $"Document lenght: {ammountCharacters} characters";
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if(isButtonChecked)
                 contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
            {
                TextDecorationCollection textDecorations;
                (contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection).TryRemove(TextDecorations.Underline, out textDecorations);
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
        }

        private async void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            Paragraph newParagraph;
            newParagraph = await viewModel.SpeechToTextAsync();
            contentRichTextBox.Document.Blocks.Add(newParagraph);
        }

        private void contentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = contentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);
            BoldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedWeight.Equals(FontWeights.Bold));

            var selectedStyle = contentRichTextBox.Selection.GetPropertyValue(FontStyleProperty);
            ItalicButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) && (selectedStyle.Equals(FontStyles.Italic));

            var selectedDecoration = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            UnderlineButton.IsChecked = (selectedDecoration != DependencyProperty.UnsetValue) && (selectedDecoration.Equals(TextDecorations.Underline));

            fontFamilyComboBox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            fontSizeComboBox.SelectedItem = (contentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty)).ToString();
        }

        private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(fontFamilyComboBox.SelectedItem != null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
                viewModel.LastUsedFont = (FontFamily)fontFamilyComboBox.SelectedItem;
            }

        }

        private void fontSizeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (fontSizeComboBox.SelectedItem != null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.Text);
                viewModel.LastUsedFontSize = (double)fontSizeComboBox.SelectedItem;
            }
        }

        private async void SaveButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            string fileName = $"{viewModel.SelectedNote.Id}.rtf";
            string rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);

            using (FileStream fileStream = new(rtfFile, FileMode.Create))
            {
                var contents = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
                contents.Save(fileStream, DataFormats.Rtf);
            }
            try
            {
                viewModel.SelectedNote.FilePath = await UpdateFileAsync(rtfFile, fileName);
            }
            catch(Azure.RequestFailedException)
            {
                viewModel.SelectedNote.FilePath = await UpdateFileAsync(rtfFile, fileName,true);

            }
            await DataBaseHelper.UpdateAsync(viewModel.SelectedNote);


        }

        private async Task<string> UpdateFileAsync(string rtfFilePath, string fileName, bool overwrite1 = false)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=mynotesapplication;AccountKey=p+mA+BiT9EbAuxEU4964sqM2lI7Ddcskj7+GJMJg7LlzRKL8s9hEPBTyh38IYeCMqqFSGzs/4pDh2KLUAY5pFg==;EndpointSuffix=core.windows.net";
            string containerName = "mynotes";

            var container = new BlobContainerClient(connectionString, containerName);
            var blob = container.GetBlobClient(fileName);
            await blob.UploadAsync(rtfFilePath, overwrite : overwrite1);

            return $"https://mynotesapplication.blob.core.windows.net/mynotes/{fileName}";
        }

        private async void ViewModel_SelectedNoteChanged(object sender, EventArgs e)
        {
            contentRichTextBox.Document.Blocks.Clear();
            if (viewModel.SelectedNote != null)
            {
                if (string.IsNullOrEmpty(viewModel.SelectedNote.FilePath) == false)
                {
                    //azure download note from container start
                    string downloadPath = $"{viewModel.SelectedNote.Id}.rtf";
                    await new BlobClient(new Uri(viewModel.SelectedNote.FilePath)).DownloadToAsync(downloadPath);
                    //azure download note from container end

                    using (FileStream fileStream = new(downloadPath, FileMode.Open))
                    {
                        var contents = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
                        contents.Load(fileStream, DataFormats.Rtf);
                    }
                }
            }
        }
    }
}
