namespace BloodDonation.Utils.Firebase
{
    using System;
    using System.Net.Http;

    class FirebaseRequest
    {
        private const string JsonSuffix = ".json";

        private HttpMethod Method { get; set; }

        private string Json { get; set; }

        private string Uri { get; set; }


        public FirebaseRequest(HttpMethod method, string uri, string jsonString = null)
        {
            Method = method;
            Json = jsonString;
            if (uri.Replace("/", string.Empty).EndsWith("firebaseio.com"))
            {
                Uri = uri + '/' + JsonSuffix;
            }
            else
            {
                Uri = uri + JsonSuffix;
            }
        }

        public FirebaseResponse Execute()
        {
            Uri requestUri;
            if (FirebaseHelper.ValidateUri(Uri))
            {
                requestUri = new Uri(Uri);
            }
            else
            {
                return new FirebaseResponse(false, "Proided Firebase path is not a valid HTTP/S URL");
            }

            string json = null;
            if (Json != null)
            {
                if (!FirebaseHelper.TryParseJson(Json, out json))
                {
                    return new FirebaseResponse(false, $"Invalid JSON : {json}");
                }
            }

            var response = FirebaseHelper.RequestHelper(Method, requestUri, json);
            response.Wait();
            var result = response.Result;

            var firebaseResponse = new FirebaseResponse()
            {
                HttpResponse = result,
                ErrorMessage = result.StatusCode.ToString() + " : " + result.ReasonPhrase,
                Success = response.Result.IsSuccessStatusCode
            };

            if (Method.Equals(HttpMethod.Get))
            {
                var content = result.Content.ReadAsStringAsync();
                content.Wait();
                firebaseResponse.JsonContent = content.Result;
            }

            return firebaseResponse;
        }
    }
}