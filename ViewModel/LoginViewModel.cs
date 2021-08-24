using NoteApplication.Model;
using NoteApplication.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.ViewModel
{
    class LoginViewModel
    {
        private User user;

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public RegisterCommand RegisterCommand { get; set; }

        public LoginCommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            RegisterCommand = new(this);
            LoginCommand = new(this);
        }

    }
}
