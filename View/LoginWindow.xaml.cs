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
    }
}
