using Newtonsoft.Json.Linq;

namespace BloodDonation.Utils.Firebase
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    class FirebaseHelper
    {
        private const string UserAgent = "firebase-net/1.0";

        public static bool ValidateUri(string url)
        {
            //if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out url))
            //{
            //    if (
            //        !(locurl.IsAbsoluteUri &&
            //          (locurl.Scheme == "http" || locurl.Scheme == "https")) ||
            //        !locurl.IsAbsoluteUri)
            //    {
            //        return false;
            //    }
            //}
            //else
            //{
            //    return false;
            //}

            return true;
        }

        public static bool TryParseJson(string inJson, out string output)
        {
            try
            {
                JToken parsedJson = JToken.Parse(inJson);
                output = parsedJson.ToString();
                return true;
            }
            catch (Exception ex)
            {
                output = ex.Message;
                return false;
            }
        }

        public static Task<HttpResponseMessage> RequestHelper(HttpMethod method, Uri uri, string json = null)
        {
            var client = new HttpClient();
            var msg = new HttpRequestMessage(method, uri);
            msg.Headers.Add("user-agent", UserAgent);
            if (json != null)
            {
                msg.Content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");
            }

            return client.SendAsync(msg);
        }
    }
}