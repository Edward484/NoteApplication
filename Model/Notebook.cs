using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.Model
{
    public class Notebook
    {
        [PrimaryKey,AutoIncrement]
        public int Id { set; get; }
        [Indexed]
        public int UserId { set; get; }
        public string Name { set; get; }
    }
}
