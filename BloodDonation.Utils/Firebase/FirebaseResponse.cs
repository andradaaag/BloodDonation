namespace BloodDonation.Utils.Firebase
{
    using System.Net.Http;

    public class FirebaseResponse
    {
        public bool Success { get; set; }

        public string JsonContent { get; set; }

        public string ErrorMessage { get; set; }

        public HttpResponseMessage HttpResponse { get; set; }

        public FirebaseResponse()
        {
        }

        public FirebaseResponse(bool success, string errorMessage, HttpResponseMessage httpResponse = null,
            string jsonContent = null)
        {
            Success = success;
            JsonContent = jsonContent;
            ErrorMessage = errorMessage;
            HttpResponse = httpResponse;
        }
    }
}