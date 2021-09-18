using NoteApplication.ViewModel;
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
using System.Windows.Shapes;

namespace NoteApplication.View
{
    /// <summary>
    /// Interaction logic for LoginWindows.xaml
    /// </summary>
    public partial class LoginWindows : Window
    {
        LoginViewModel vm;
        public string PlaceholderText { get; set; }
        public static DependencyProperty PlaceholderTextProperty { get; }
        public LoginWindows()
        {
            InitializeComponent();

            vm = Resources["vm"] as LoginViewModel;
            vm.Authenticated += Vm_Authenticated;
        }

        private void Vm_Authenticated(object sender, EventArgs e)
        {
            Close();
        }

        private void UsernameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PasswordTextBox.Focus();
            }
        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                vm.LoginAsync();
            }
        }

        

        private void FirstNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LastNameTextBox.Focus();
            }
        }

        private void LastNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                UsernameTextBoxR.Focus();
            }
        }

        private void UsernameTextBoxR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PasswordTextBoxR.Focus();
            }
        }

        private void PasswordTextBoxR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ConfirmPasswordTextBoxR.Focus();
            }
        }

        private void ConfirmPasswordTextBoxR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                vm.RegisterAsync();
            }
        }

        private void PlacheholderTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            textbox.Text = "";
        }

        private void PlaceholderTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            vm.PutPlaceholdersBack();
        }
    }
}
