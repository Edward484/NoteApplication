using Newtonsoft.Json;
using NoteApplication.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.ViewModel.Helpers
{
    public class DataBaseHelper
    {
        private static string dataBaseFile = Path.Combine(Environment.CurrentDirectory, "NotesAplicationDB1.db3");
        private static string dataBasePath = "https://notesappwpf-b01d5-default-rtdb.europe-west1.firebasedatabase.app/";

        public static async Task<bool> InsertAsync<T>(T item) where T : HasId
        {
            var body = JsonConvert.SerializeObject(item);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            using(HttpClient client = new())
            {
                var result =  await client.PostAsync($"{dataBasePath}{item.GetType().Name.ToLower()}.json", content);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public static async Task<bool> UpdateAsync<T>(T item) where T : HasId
        {
            var body = JsonConvert.SerializeObject(item);
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            using (HttpClient client = new())
            {
                var result = await client.PatchAsync($"{dataBasePath}{item.GetType().Name.ToLower()}/{item.Id}.json", content);
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public static async Task<bool> DeleteAsync<T>(T item) where T: HasId
        {
            using (HttpClient client = new())
            {
                var result = await client.DeleteAsync($"{dataBasePath}{item.GetType().Name.ToLower()}.json");
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public static async Task<List<T>> ReadAsync<T>() where T: HasId
        {
            using (HttpClient client = new())
            {
                var result = await client.GetAsync($"{dataBasePath}{typeof(T).Name.ToLower()}.json");

                var jsonResult = await result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    var objects = JsonConvert.DeserializeObject<Dictionary<string,T>>(jsonResult);
                    List<T> list = new();
                    foreach (var obj in objects)
                    {
                        obj.Value.Id = obj.Key;
                        list.Add(obj.Value);
                    }
                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
