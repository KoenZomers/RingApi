using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;

namespace KoenZomers.Ring.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// Username to use to connect to the Ring API
        /// </summary>
        public string Username => ConfigurationManager.AppSettings["RingUsername"];

        /// <summary>
        /// Password to use to connect to the Ring API
        /// </summary>
        public string Password => ConfigurationManager.AppSettings["RingPassword"];

        /// <summary>
        /// Test the scenario where the authentication should succeed
        /// </summary>
        [TestMethod]
        public async Task AuthenticateSuccessTest()
        {
            var session = new Api.Session(Username, Password);

            var authResult = await session.Authenticate();
            Assert.IsFalse(string.IsNullOrEmpty(authResult.Profile.AuthenticationToken), "Failed to authenticate");
        }

        /// <summary>
        /// Test the scenario where the authentication would fail
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.Net.WebException))]
        public async Task AuthenticateFailTest()
        {
            var session = new Api.Session("test@test.com", "someinvalidpassword");

            await session.Authenticate();
        }

        /// <summary>
        /// Test the scenario where a refresh token is used to successfully set up an authenticated session
        /// </summary>
        [TestMethod]
        public async Task AuthenticateWithRefreshTokenSuccessTest()
        {
            // Authenticate normally the first time in order to get a refresh token to test
            var session = new Api.Session(Username, Password);
            await session.Authenticate();

            // Request a new authenticated session based on the RefreshToken
            var refreshedSession = await Api.Session.GetSessionByRefreshToken(session.OAuthToken.RefreshToken);
            Assert.IsTrue(refreshedSession.IsAuthenticated, "Failed to authenticate using refresh token");
        }

        /// <summary>
        /// Test the scenario where a refresh token is used to set up an authenticated session which fails
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Api.Exceptions.AuthenticationFailedException))]
        public async Task AuthenticateWithRefreshTokenFailTest()
        {
            // Request a new authenticated session based on a non existing RefreshToken
            var refreshedSession = await Api.Session.GetSessionByRefreshToken("abcdefghijklmnopqrstuvwxyz");
        }

        /// <summary>
        /// Test if the devices can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetDevicesTest()
        {
            var session = new Api.Session(Username, Password);
            await session.Authenticate();

            var devices = await session.GetRingDevices();
            Assert.IsTrue(devices.Chimes.Count > 0 && devices.Doorbots.Count > 0, "No doorbots and/or chimes returned");
        }

        /// <summary>
        /// Test if the an SessionNotAuthenticatedException gets thrown when trying to retrieve the Ring devices without authenticating first
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Api.Exceptions.SessionNotAuthenticatedException))]
        public async Task GetDevicesUnauthenticatedTest()
        {
            var session = new Api.Session(Username, Password);
            await session.GetRingDevices();
        }

        /// <summary>
        /// Test if the doorbot history events can be retrieved with the default amount of items
        /// </summary>
        [TestMethod]
        public async Task GetDoorbotsHistoryTest()
        {
            var session = new Api.Session(Username, Password);
            await session.Authenticate();

            var doorbotHistory = await session.GetDoorbotsHistory();
            Assert.IsTrue(doorbotHistory.Count > 0, "No doorbot history items returned");
            Assert.IsTrue(doorbotHistory.Count == 20, $"{doorbotHistory.Count} doorbot history items returned while 20 were expected");
        }

        /// <summary>
        /// Test if the doorbot history events can be retrieved with a specific amount of items
        /// </summary>
        [TestMethod]
        public async Task GetDoorbotsHistoryWithLimitTest()
        {
            var limit = 50;

            var session = new Api.Session(Username, Password);
            await session.Authenticate();

            var doorbotHistory = await session.GetDoorbotsHistory(limit);
            Assert.IsTrue(doorbotHistory.Count > 0, "No doorbot history items returned");
            Assert.IsTrue(doorbotHistory.Count == limit, $"{doorbotHistory.Count} doorbot history items returned while {limit} were expected");
        }

        /// <summary>
        /// Test if the recording for a doorbot history event can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetDoorbotsHistoryRecordingByIdTest()
        {
            var session = new Api.Session(Username, Password);
            await session.Authenticate();

            var doorbotHistory = await session.GetDoorbotsHistory();

            Assert.IsTrue(doorbotHistory.Count > 0, "No doorbot history events were found");

            var tempFilePath = Path.GetTempFileName();

            await session.GetDoorbotHistoryRecording(doorbotHistory[0].Id, tempFilePath);

            File.Delete(tempFilePath);
        }

        /// <summary>
        /// Test if the recording for a doorbot history event can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetDoorbotsHistoryRecordingByInstanceTest()
        {
            var session = new Api.Session(Username, Password);
            await session.Authenticate();

            var doorbotHistory = await session.GetDoorbotsHistory();

            Assert.IsTrue(doorbotHistory.Count > 0, "No doorbot history events were found");

            var tempFilePath = Path.GetTempFileName();

            await session.GetDoorbotHistoryRecording(doorbotHistory[0], tempFilePath);

            File.Delete(tempFilePath);
        }
    }
}
