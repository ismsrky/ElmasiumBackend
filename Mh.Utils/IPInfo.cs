using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Mh.Utils
{
    public class IPInfo
    {
        private const string c_baseURL = "https://ipinfo.io/";


        /// <summary>
        /// Get a specified information about the specified IP address ( leave blank to get the caller's IP informations ) asynchroniously
        /// </summary>
        /// <returns><see cref="string"/> Result</returns>
        public async static Task<string> GetAsync(string ip = null, string token = null)
        {
            string text = await Task.Run(() => RequestAsync(ip, token));
            return text;
        }



        /// <summary>
        /// Get a specified information about the specified IP addres ( leave blank to get the caller's IP informations )
        /// </summary>
        /// <returns><see cref="string"/> Result</returns>
        public static string Get(string ip = null, string token = null)
        {
            return Request(ip, token);
        }

        private static string Request(string ip = null, string token = null)
        {
            string json = null;
            Task.WaitAll(Task.Run(async () =>
            {
                json = await RequestAsync(ip, token);
            }));
            return json;
        }

        private async static Task<string> RequestAsync(string ip = null, string token = null)
        {
            WebRequest wRequest = WebRequest.Create($"{c_baseURL}{(ip == null ? "" : ip)}{(token == null ? "" : $"?token={token}")}");
            System.Diagnostics.Debug.Print(wRequest.RequestUri.ToString());
            WebResponse wResponse = await wRequest.GetResponseAsync();
            using (StreamReader sReader = new StreamReader(wResponse.GetResponseStream()))
                return sReader.ReadToEnd();
        }
    }
}