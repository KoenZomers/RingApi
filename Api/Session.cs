using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace KoenZomers.Ring.Api
{
    public class Session
    {
        #region Properties

        /// <summary>
        /// Username to use to connect to the Ring API. Set by providing it in the constructor.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Password to use to connect to the Ring API. Set by providing it in the constructor.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Returns the Base64 Encoded username and password to use in the authenticate header
        /// </summary>
        public string CredentialsEncoded => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Username}:{Password}"));

        /// <summary>
        /// Base Uri with which all Ring API requests start
        /// </summary>
        public Uri RingApiBaseUrl => new Uri("https://api.ring.com/clients_api/");

        /// <summary>
        /// Boolean indicating if the current session is authenticated
        /// </summary>
        public bool IsAuthenticated => !string.IsNullOrEmpty(AuthenticationToken);

        /// <summary>
        /// Authentication Token that will be used to communicate with the Ring API
        /// </summary>
        public string AuthenticationToken { get; private set; }

        #endregion

        #region Fields



        #endregion

        #region Constructors

        /// <summary>
        /// Initiates a new session to the Ring API
        /// </summary>
        public Session(string username, string password)
        {
            Username = username;
            Password = password;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Authenticates to the Ring API
        /// </summary>
        /// <returns>Session object if the authentication was successful</returns>
        public async Task<Entities.Session> Authenticate()
        {
            var response = await HttpUtility.FormPost(
                                                new Uri(RingApiBaseUrl, "session"),
                                                new Dictionary<string, string>
                                                {
                                                    { "device[metadata][device_model]", "Test" },
                                                    { "device[metadata][device_name]", "Test" },
                                                    { "device[metadata][resolution]", "800x600" },
                                                    { "device[metadata][app_version]", "1.3.810" },
                                                    { "device[metadata][app_instalation_date]", string.Format("{0:yyyy-MM-dd}+{0:HH}%3A{0:mm}%3A{0:ss}Z", DateTime.Now) },
                                                    { "device[metadata][manufacturer]", "Test" },
                                                    { "device[metadata][device_type]", "tablet" },
                                                    { "device[metadata][architecture]", "x64" },
                                                    { "device[metadata][language]", "en" },
                                                    { "device[os]", "windows" },
                                                    { "device[hardware_id]", "Test" },
                                                    { "device[app_brand]", "ring" }
                                                },
                                                new System.Collections.Specialized.NameValueCollection
                                                {
                                                    { "Accept-Encoding", "gzip, deflate" },
                                                    { "X-API-LANG", "en" },
                                                    { "Authorization", $"Basic {CredentialsEncoded}" }
                                                },
                                                null);

            var session = JsonConvert.DeserializeObject<Entities.Session>(response);
            AuthenticationToken = session.Profile.AuthenticationToken;

            return session;
        }

        /// <summary>
        /// Returns all devices registered with Ring under the current account being used
        /// </summary>
        /// <returns>Devices registered with Ring under the current account</returns>
        public async Task<Entities.Devices> GetRingDevices()
        {
            if(!IsAuthenticated)
            {
                throw new Exceptions.SessionNotAuthenticatedException();
            }

            var response = await HttpUtility.GetContents(new Uri(RingApiBaseUrl, $"ring_devices?auth_token={AuthenticationToken}&api_version=9"), null);

            var devices = JsonConvert.DeserializeObject<Entities.Devices>(response);
            return devices;
        }

        /// <summary>
        /// Returns all events registered for the doorbots
        /// </summary>
        /// <returns>All events triggered by registered doorbots under the current account</returns>
        public async Task<List<Entities.DoorbotHistoryEvent>> GetDoorbotsHistory()
        {
            if (!IsAuthenticated)
            {
                throw new Exceptions.SessionNotAuthenticatedException();
            }

            var response = await HttpUtility.GetContents(new Uri(RingApiBaseUrl, $"doorbots/history?auth_token={AuthenticationToken}&api_version=9"), null);

            var doorbotHistory = JsonConvert.DeserializeObject<List<Entities.DoorbotHistoryEvent>>(response);
            return doorbotHistory;
        }

        /// <summary>
        /// Returns a stream with the recording of the provided Ding Id of a doorbot
        /// </summary>
        /// <param name="doorbotHistoryEvent">The doorbot history event to retrieve the recording for</param>
        /// <returns>Stream containing contents of the recording</returns>
        public async Task<Stream> GetDoorbotHistoryRecording(Entities.DoorbotHistoryEvent doorbotHistoryEvent)
        {
            return await GetDoorbotHistoryRecording(doorbotHistoryEvent.Id);
        }

        /// <summary>
        /// Returns a stream with the recording of the provided Ding Id of a doorbot
        /// </summary>
        /// <param name="dingId">Id of the doorbot history event to retrieve the recording for</param>
        /// <returns>Stream containing contents of the recording</returns>
        public async Task<Stream> GetDoorbotHistoryRecording(string dingId)
        {
            if (!IsAuthenticated)
            {
                throw new Exceptions.SessionNotAuthenticatedException();
            }

            var stream = await HttpUtility.DownloadFile(new Uri(RingApiBaseUrl, $"dings/{dingId}/recording?auth_token={AuthenticationToken}&api_version=9"), null);
            return stream;
        }

        /// <summary>
        /// Saves the recording of the provided Ding Id of a doorbot to the provided location
        /// </summary>
        /// <param name="doorbotHistoryEvent">The doorbot history event to retrieve the recording for</param>
        public async Task GetDoorbotHistoryRecording(Entities.DoorbotHistoryEvent doorbotHistoryEvent, string saveAs)
        {
            await GetDoorbotHistoryRecording(doorbotHistoryEvent.Id, saveAs);
        }

        /// <summary>
        /// Saves the recording of the provided Ding Id of a doorbot to the provided location
        /// </summary>
        /// <param name="dingId">Id of the doorbot history event to retrieve the recording for</param>
        public async Task GetDoorbotHistoryRecording(string dingId, string saveAs)
        {
            using (var stream = await GetDoorbotHistoryRecording(dingId))
            {
                using (var fileStream = File.Create(saveAs))
                {
                    await stream.CopyToAsync(fileStream);
                }
            }
        }

        #endregion
    }
}
