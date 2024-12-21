using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AuthLib.Models
{
        /// <summary>
        /// Handles backend service interactions for authentication.
        /// </summary>
        public class BackendService
        {
            private readonly HttpClient _httpClient;

            /// <summary>
            /// Initializes a new instance of the BackendService class with specified base address.
            /// </summary>
            /// <param name="baseAddress">Base address of the backend API.</param>
            public BackendService(string baseAddress)
            {
                _httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
            }

            /// <summary>
            /// Attempts to login to the backend with the provided credentials.
            /// </summary>
            /// <param name="email">Email for login.</param>
            /// <param name="password">Password for login.</param>
            /// <returns>Response from the backend indicating success or failure.</returns>
            public async Task<HttpResponseMessage> LoginAsync(string email, string password)
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(new { email, password }), 
                    Encoding.UTF8, 
                    "application/json"
                );
                return await _httpClient.PostAsync("api/login", content);
            }
        }
}