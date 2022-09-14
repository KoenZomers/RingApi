using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace KoenZomers.Ring.Api
{
    /// <summary>
    /// Internal utility class for Http communication with the Ring API
    /// </summary>
    internal static class HttpUtility
    {
        /// <summary>
        /// Performs a GET request to the provided url to return the contents
        /// </summary>
        /// <param name="url">Url of the request to make</param>
        /// <param name="bearerToken">Bearer token to authenticate the request with. Leave out to not authenticate the session.</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>Contents of the result returned by the webserver</returns>
        /// <exception cref="Exceptions.ThrottledException">Thrown when the web server indicates too many requests have been made (HTTP 429).</exception>
        public static async Task<string> GetContents(Uri url, string bearerToken = null, int timeout = 60000)
        {
            // Construct the request
            var request = (HttpWebRequest)WebRequest.Create(url);

            // Check if the OAuth Bearer Authorization token should be added to the request
            if (!string.IsNullOrEmpty(bearerToken))
            {
                request.Headers[HttpRequestHeader.Authorization] = $"Bearer {bearerToken}";
            }

            request.Timeout = timeout;

            // Send the request to the webserver
            WebResponse response;
            try
            {
                response = await request.GetResponseAsync();
            }
            catch (WebException e)
            {
                // Check if the response is HTTP 429 Too Many Requests throttling
                if (e.Message.IndexOf("Too many requests", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    throw new Exceptions.ThrottledException(e);
                }

                throw;
            }

            // Get the stream containing content returned by the server.
            var dataStream = response.GetResponseStream();
            if (dataStream == null) return null;

            // Open the stream using a StreamReader for easy access.
            var reader = new StreamReader(dataStream);

            // Read the content returned
            var responseFromServer = await reader.ReadToEndAsync();
            return responseFromServer;
        }

        /// <summary>
        /// Sends a POST request using the url encoded form method
        /// </summary>
        /// <param name="url">Url to POST to</param>
        /// <param name="formFields">Dictonary with key/value pairs containing the forms data to POST to the webserver</param>
        /// <param name="headerFields">NameValueCollection with the fields to add to the header sent to the server with the request</param>
        /// <param name="cookieContainer">Cookies which have been recorded for this session</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>The website contents returned by the webserver after posting the data</returns>
        /// <exception cref="Exceptions.ThrottledException">Thrown when the web server indicates too many requests have been made (HTTP 429).</exception>
        /// <exception cref="Exceptions.TwoFactorAuthenticationIncorrectException">Thrown when the web server indicates the two-factor code was incorrect (HTTP 400).</exception>
        /// <exception cref="Exceptions.TwoFactorAuthenticationRequiredException">Thrown when the web server indicates two-factor authentication is required (HTTP 412).</exception>
        public static async Task<string> FormPost(Uri url, Dictionary<string, string> formFields, NameValueCollection headerFields, CookieContainer cookieContainer, int timeout = 60000)
        {
            // Construct the POST request which performs the login
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Post;
            request.ServicePoint.Expect100Continue = false;
            request.CookieContainer = cookieContainer;
            request.Timeout = timeout;

            if (headerFields != null)
            {
                request.Headers.Add(headerFields);
            }

            // Construct POST data
            var postData = new StringBuilder();
            foreach (var formField in formFields)
            {
                if (postData.Length > 0) postData.Append("&");
                postData.Append($"{formField.Key}={formField.Value}");
            }

            // Convert the POST data to a byte array
            var postDataByteArray = Encoding.UTF8.GetBytes(postData.ToString());

            // Set the ContentType property of the WebRequest
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = postDataByteArray.Length;

            // Get the request stream
            var dataStream = await request.GetRequestStreamAsync();

            // Write the POST data to the request stream
            await dataStream.WriteAsync(postDataByteArray, 0, postDataByteArray.Length);

            // Close the Stream object
            dataStream.Close();

            // Receive the response from the webserver
            WebResponse response;
            try
            {
                response = await request.GetResponseAsync() as HttpWebResponse;
            }
            catch (WebException e)
            {
                // Check if the response is HTTP 429 Too Many Requests throttling
                if (e.Message.IndexOf("Too many requests", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    throw new Exceptions.ThrottledException(e);
                }

                // Check if two factor authentication is enabled by checking for the response: "The remote server returned an error: (412) Precondition Failed."
                if (e.Message.IndexOf("Precondition Failed", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    throw new Exceptions.TwoFactorAuthenticationRequiredException(e);
                }

                // Check if the two factor authentication token was incorrect or has expired. HTTP 400 Bad Request.
                if (e.Message.IndexOf("Bad Request", StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    throw new Exceptions.TwoFactorAuthenticationIncorrectException(e);
                }

                throw;
            }

            // Make sure the webserver has sent a response
            if (response == null) return null;

            dataStream = response.GetResponseStream();

            // Make sure the datastream with the response is available
            if (dataStream == null) return null;

            var reader = new StreamReader(dataStream);
            return await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Downloads the file from the provided Url
        /// </summary>
        /// <param name="url">Url to download the file from</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <param name="bearerToken">Bearer token to authenticate the request with. Leave out to not authenticate the session.</param>
        /// <returns>Stream with the file download</returns>
        public static async Task<Stream> DownloadFile(Uri url, string bearerToken = null, int timeout = 60000)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "*/*";
            request.AddRange("bytes", 0);
            request.Timeout = timeout;
            request.AllowAutoRedirect = true;

            // Check if the OAuth Bearer Authorization token should be added to the request
            if (!string.IsNullOrEmpty(bearerToken))
            {
                request.Headers[HttpRequestHeader.Authorization] = $"Bearer {bearerToken}";
            }

            // Receive the response from the webserver
            var response = await request.GetResponseAsync() as HttpWebResponse;
            var httpResponseStream = response?.GetResponseStream();

            return httpResponseStream;
        }

        /// <summary>
        /// Performs a HTTP request expecting a certain status code to be returned by the server
        /// </summary>
        /// <param name="url">Url of the request to make</param>
        /// <param name="httpMethod">The HTTP method to use to call the provided Url</param>
        /// <param name="expectedStatusCode">The expected HTTP status code to be replied by the Ring API. An exception will be thrown if the expectation was wrong. Leave NULL to not set an expectation.</param>
        /// <param name="bodyContent">Content to send along with the request in the body. Leave NULL to not send along any content.</param>
        /// <param name="bearerToken">Bearer token to authenticate the request with. Leave out to not authenticate the session.</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <exception cref="Exceptions.UnexpectedOutcomeException">Thrown if the actual HTTP response is different from what was expected</exception>
        public static async Task SendRequestWithExpectedStatusOutcome(Uri url, HttpMethod httpMethod, HttpStatusCode? expectedStatusCode, string bodyContent = null, string bearerToken = null, int timeout = 60000)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(timeout);

                var request = new HttpRequestMessage(httpMethod, url);

                if (bearerToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                }

                if (bodyContent != null)
                {
                    request.Content = new StringContent(bodyContent, Encoding.UTF8, "application/json");
                }

                // Send the HTTP request
                var response = await client.SendAsync(request);

                // Validate the resulting HTTP status against the expected status
                if (expectedStatusCode.HasValue && response.StatusCode != expectedStatusCode.Value)
                {
                    throw new Exceptions.UnexpectedOutcomeException(response.StatusCode, expectedStatusCode.Value);
                }
            }
        }

        /// <summary>
        /// Sends a HttpRequest to the Ring API server
        /// </summary>
        /// <typeparam name="T">Type of entity to try to parse the result from the Ring API in</typeparam>
        /// <param name="url">Url of the request to make</param>
        /// <param name="httpMethod">The HTTP method to use to call the provided Url</param>
        /// <param name="bodyContent">Content to send along with the request in the body. Leave NULL to not send along any content.</param>
        /// <param name="bearerToken">Bearer token to authenticate the request with. Leave out to not authenticate the session.</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>Contents of the result returned by the Ring API parsed in the type T provided</returns>
        public static async Task<T> SendRequest<T>(Uri url, HttpMethod httpMethod, string bodyContent, string bearerToken = null, int timeout = 60000)
        {
            // Make the request and get the body contents of the response
            var response = await SendRequest(url, httpMethod, bodyContent, bearerToken, timeout);

            // Try parsing the response to the type provided with this method
            T responseEntity = JsonConvert.DeserializeObject<T>(response);
            return responseEntity;
        }

        /// <summary>
        /// Sends a HttpRequest to the Ring API server
        /// </summary>
        /// <param name="url">Url of the request to make</param>
        /// <param name="httpMethod">The HTTP method to use to call the provided Url</param>
        /// <param name="bodyContent">Content to send along with the request in the body. Leave NULL to not send along any content.</param>
        /// <param name="bearerToken">Bearer token to authenticate the request with. Leave out to not authenticate the session.</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>Contents of the result returned by the Ring API</returns>
        public static async Task<string> SendRequest(Uri url, HttpMethod httpMethod, string bodyContent, string bearerToken = null, int timeout = 60000)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(timeout);

                var request = new HttpRequestMessage(httpMethod, url);

                if (bearerToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                }

                if (bodyContent != null)
                {
                    request.Content = new StringContent(bodyContent, Encoding.UTF8, "application/json");
                }

                // Send the HTTP request
                var response = await client.SendAsync(request);

                // Get the response body and return it
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }
    }
}
