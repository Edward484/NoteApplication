﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.Model
{
    public class FirebaseRegister
    {
        public string kind { get; set; }
        public string idToken { get; set; }
        public string email { get; set; }
        public string refreshToken { get; set; }
        public string expiresIn { get; set; }
        public string localId { get; set; }


    }
}
