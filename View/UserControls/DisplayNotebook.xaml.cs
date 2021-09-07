using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NoteApplication.View.UserControls
{
    /// <summary>
    /// Interaction logic for DisplayNotebook.xaml
    /// </summary>
    public partial class DisplayNotebook : UserControl 
    {


        public Notebook NotebookDep
        {
            get { return (Notebook)GetValue(NotebookProperty); }
            set { SetValue(NotebookProperty, value); }
        }

        

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotebookProperty =
            DependencyProperty.Register("NotebookDep", typeof(Notebook), typeof(DisplayNotebook), new PropertyMetadata(null,SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DisplayNotebook notebookUserControl = d as DisplayNotebook;
            if(notebookUserControl != null)
            {
                notebookUserControl.DataContext = notebookUserControl.NotebookDep;
            }
        }

        public Visibility Visible
        {
            get { return (Visibility)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Visible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VisibleProperty =
            DependencyProperty.Register("Visible", typeof(Visibility), typeof(DisplayNotebook), new PropertyMetadata(Visibility.Collapsed, SetVisible));

        private static void SetVisible(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        public DisplayNotebook()
        {
            InitializeComponent();
        }
    }
}
