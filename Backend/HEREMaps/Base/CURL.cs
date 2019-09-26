using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HEREMaps.Base {
    /// <summary>
    /// Class for making HTTP requests.
    /// </summary>
    public class CURL {
        /// <summary>
        /// The inner http client.
        /// </summary>
        private static readonly HttpClient innerClient = new HttpClient();

        /// <summary>
        /// Making an HTTP GET request (Async request).
        /// </summary>
        /// <param name="endpoint">The URL to send the request to</param>
        /// <param name="arguments">(Optional) request arguments</param>
        /// <returns>The result of the request</returns>
        public static async Task<string> GET(string endpoint, Dictionary<string, string> arguments = null) {
            // Appending the arguments to the URL
            if (arguments != null) {
                endpoint += "?";
                foreach (var key in arguments.Keys) {
                    var val = arguments[key];
                    endpoint += key + "=" + val + "&";
                }
                endpoint = endpoint.Substring(0, endpoint.Length - 1);
            }
            // Sending the request
            try {
                var response = await innerClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string responseStr = await response.Content.ReadAsStringAsync();

                return responseStr;
            } catch (HttpRequestException ex) {
                return "Got exception: " + ex.Message;
            }
        }

        /// <summary>
        /// Making an HTTP POST request (Async result).
        /// </summary>
        /// <param name="endpoint">The URL to send the request to</param>
        /// <param name="jsonable">An arguments object to send as JSON string</param>
        /// <returns></returns>
        public static async Task<string> POST(string endpoint, IJSONable jsonable) {
            return await POST(endpoint, jsonable.ToJSON());
        }

        /// <summary>
        /// Making an HTTP POST request (Async request).
        /// </summary>
        /// <param name="endpoint">The URL to send the request to</param>
        /// <param name="json">Arguments as JSON string</param>
        /// <returns></returns>
        public static async Task<string> POST(string endpoint, string json) {
            try {
                var response = await innerClient.PostAsync(endpoint,
                    new StringContent(json,
                        Encoding.UTF8,
                        "application/json"));
                response.EnsureSuccessStatusCode();
                string responseStr = await response.Content.ReadAsStringAsync();

                return responseStr;
            } catch (HttpRequestException ex) {
                return "Got exception: " + ex.Message;
            }
        }
    }
}