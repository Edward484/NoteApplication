using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.Model
{
    public class Notebook:HasId
    {
        public string Id { set; get; }
        public string UserId { set; get; }
        public string Name { set; get; }
    }
}
