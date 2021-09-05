using NoteApplication.Model;
using NoteApplication.ViewModel.Commands;
using NoteApplication.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NoteApplication.ViewModel
{
    public class LoginViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Authenticated;

        private bool isShowingRegister = false;
        private User user;

        public User User
        {
            get { return user; }
            set
            { 
                user = value;
                OnPropertyChanged("User");
            }
        }

        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }
        public ShowRegisterCommand ShowRegisterCommand { get; set; }

        private Visibility loginVisibility;
        public Visibility LoginVisibility
        {
            get { return loginVisibility; }
            set 
            { 
                loginVisibility = value;
                OnPropertyChanged("LoginVisibility");
            }
        }

        private Visibility registerVisibility;
        public Visibility RegisterVisibility
        {
            get { return registerVisibility; }
            set
            {
                registerVisibility = value;
                OnPropertyChanged("RegisterVisibility");
            }
        }



        private string username;

        public string Username
        {
            get { return username; }
            set 
            { 
                username = value;
                User = new User
                {
                    Username = username,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    LastName = this.LastName,
                    FirstName = this.FirstName
                };
                OnPropertyChanged("Username");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set 
            { 
                password = value;
                User = new User
                {
                    Username = this.Username,
                    Password = Password,
                    ConfirmPassword = this.ConfirmPassword,
                    LastName = this.LastName,
                    FirstName = this.FirstName
                };
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    ConfirmPassword = ConfirmPassword,
                    LastName = this.LastName,
                    FirstName = this.FirstName
                  
                };
                OnPropertyChanged("ConfirmPassword");
            }
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    LastName = this.LastName,
                    FirstName = firstName
                };
                OnPropertyChanged("firstName");
            }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                User = new User
                {
                    Username = this.Username,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    LastName = LastName,
                    FirstName = this.FirstName
                };
                OnPropertyChanged("lastName");
            }
        }


        public LoginViewModel()
        {
            LoginVisibility = Visibility.Visible;
            RegisterVisibility = Visibility.Collapsed;

            RegisterCommand = new(this);
            LoginCommand = new(this);
            ShowRegisterCommand = new(this);

            User = new();
        }

        public void SwitchViews()
        {
            isShowingRegister = !isShowingRegister;

            if (isShowingRegister == true)
            {
                RegisterVisibility = Visibility.Visible;
                LoginVisibility = Visibility.Collapsed;
            }
            else
            {
                RegisterVisibility = Visibility.Collapsed;
                LoginVisibility = Visibility.Visible;
            }
        }

        public async Task LoginAsync()
        {
            bool result = await FirebaseAuthHelper.LoginAsync(user);
            if(result == true)
            {
                Authenticated?.Invoke(this, new EventArgs());
            }

        }

        public async Task RegisterAsync()
        {
            bool result = await FirebaseAuthHelper.RegisterAsync(user);
            if(result == true)
            {
                Authenticated?.Invoke(this, new EventArgs());
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
