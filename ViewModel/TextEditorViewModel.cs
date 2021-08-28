using NoteApplication.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace NoteApplication.ViewModel
{
    class TextEditorViewModel
    {
        public ToggleButton BoldButton { get; set; }

        public string Content { get; set; }

        public RichTextBox ContentRichTextBox { get; set; }
        public BoldCommand BoldCommand { get; set; }
        public DependencyProperty FontWeightProperty { get; private set; }

        public void MakeSelectedTextBold(object sender)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
                ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                ContentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
        }

        public void SelectionChanged()
        {
            var selectedWeight = ContentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);
            BoldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedWeight.Equals(FontWeights.Bold));
        }
    }
}
