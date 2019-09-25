using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HEREMaps.Base
{
    public class CURL
    {
        private static HttpClient innerClient = new HttpClient();

        public static async Task<string> GET(string endpoint, Dictionary<string, string> arguments=null) {
            if (arguments != null) {
                endpoint += "?";
                foreach (var key in arguments.Keys) {
                    var val = arguments[key];
                    endpoint += key + "=" + val + "&";
                }
                endpoint = endpoint.Substring(0, endpoint.Length - 1);
            }
            try {
                var response = await innerClient.GetAsync(endpoint);
                response.EnsureSuccessStatusCode();
                string responseStr = await response.Content.ReadAsStringAsync();

                return responseStr;
            } catch(HttpRequestException ex) {
                return "Got exception: " + ex.Message;
            }
        }

        public static async Task<string> POST(string endpoint, IJSONable jsonable) {
            return await POST(endpoint, jsonable.ToJSON());
        }

        public static async Task<string> POST(string endpoint, string json) {
            try {
                var response = await innerClient.PostAsync(endpoint,
                                                           new StringContent(json,
                                                                             Encoding.UTF8,
                                                                             "application/json"));
                response.EnsureSuccessStatusCode();
                string responseStr = await response.Content.ReadAsStringAsync();

                return responseStr;
            } catch(HttpRequestException ex) {
                return "Got exception: " + ex.Message;
            }
        }
    }
}
