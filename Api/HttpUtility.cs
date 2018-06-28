using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Specialized;
using System;
using System.Threading.Tasks;

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
        /// <param name="cookieContainer">Cookies which have been recorded for this session</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>Contents of the result returned by the webserver</returns>
        public static async Task<string> GetContents(Uri url, CookieContainer cookieContainer, int timeout = 60000)
        {
            // Construct the request
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = cookieContainer;
            request.Timeout = timeout;

            // Send the request to the webserver
            var response = await request.GetResponseAsync();

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
            var response = await request.GetResponseAsync() as HttpWebResponse;

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
        /// <param name="cookieContainer">Cookies which have been recorded for this session</param>
        /// <param name="timeout">Timeout in milliseconds on how long the request may take. Default = 60000 = 60 seconds.</param>
        /// <returns>Stream with the file download</returns>
        public static async Task<Stream> DownloadFile(Uri url, CookieContainer cookieContainer, int timeout = 60000)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "*/*";
            request.AddRange("bytes", 0);
            request.CookieContainer = cookieContainer;
            request.Timeout = timeout;
            request.AllowAutoRedirect = true;

            // Receive the response from the webserver
            var response = await request.GetResponseAsync() as HttpWebResponse;
            var httpResponseStream = response.GetResponseStream();

            return httpResponseStream;
        }
    }
}
