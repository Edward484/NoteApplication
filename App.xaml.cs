using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.MobileServices;

namespace NoteApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string userID = string.Empty;
        public static MobileServiceClient MobileServiceClient = new("https://mynotesapplication.azurewebsites.net");
    }
}
