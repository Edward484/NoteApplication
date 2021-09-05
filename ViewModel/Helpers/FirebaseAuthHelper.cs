using Newtonsoft.Json;
using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NoteApplication.ViewModel.Helpers
{
    public class FirebaseAuthHelper
    {
        private static string apiKey = "AIzaSyCGQO42XCHeK4wB8lfu0JCctcS-LlUwvco";

        public static async Task<bool> RegisterAsync(User user)
        {
            using(HttpClient client = new())
            {
                var body = new
                {
                    email = user.Username,
                    password = user.Password,
                    returnSecureToken = true
                };

                string bodyJson = JsonConvert.SerializeObject(body);
                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}", data);

                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FirebaseRegister>(resultJson);
                    App.userID = result.localId;

                    return true;
                }
                else
                {
                    string errorJson = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<Error>(errorJson);
                    MessageBox.Show(error.error.mesage);

                    return false;
                }
            }

        }

        public static async Task<bool> LoginAsync(User user)
        {
            using (HttpClient client = new())
            {
                var body = new
                {
                    email = user.Username,
                    password = user.Password,
                    returnSecureToken = true
                };

                string bodyJson = JsonConvert.SerializeObject(body);
                var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}", data);

                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FirebaseRegister>(resultJson);
                    App.userID = result.localId;

                    return true;
                }
                else
                {
                    string errorJson = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<Error>(errorJson);
                    MessageBox.Show(error.error.mesage);

                    return false;
                }
            }

        }

        public class ErrorDetails
        {
            public int code { get; set; }
            public string mesage { get; set; }
        }

        public class Error
        {
            public ErrorDetails error { get; set; }
        }
    }

    
}
