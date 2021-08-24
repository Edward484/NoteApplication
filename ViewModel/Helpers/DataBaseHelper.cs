using NoteApplication.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.ViewModel.Helpers
{
    public class DataBaseHelper
    {
        private static string dataBaseFile = Path.Combine(Environment.CurrentDirectory, "NotesAplicationDB1.db3");

        public static bool Insert<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection connection = new SQLiteConnection(dataBaseFile))
            {
                connection.CreateTable<T>();
                int rowNum = connection.Insert(item);
                if(rowNum > 0)
                {
                    result = true;
                }
            }

            var notes = DataBaseHelper.Read<Note>().Where(n => n.Id == 5).ToList();

            return result;

        }

        public static bool Update<T>(T item)
        {
            bool result = false;

            using (var connection = new SQLiteConnection(dataBaseFile))
            {
                connection.CreateTable<T>();
                int rowNum = connection.Update(item);
                if (rowNum > 0)
                {
                    result = true;
                }
            }
            return result;

        }

        public static bool Delete<T>(T item)
        {
            bool result = false;

            using (var connection = new SQLiteConnection(dataBaseFile))
            {
                connection.CreateTable<T>();
                int rowNum = connection.Delete(item);
                if (rowNum > 0)
                {
                    result = true;
                }
            }
            return result;

        }

        public static List<T> Read<T>() where T:new()
        {
            List<T> items = new();

            using (var connection = new SQLiteConnection(dataBaseFile))
            {
                connection.CreateTable<T>();
                items = connection.Table<T>().ToList();
            }
            return items;

        }

    }
}
