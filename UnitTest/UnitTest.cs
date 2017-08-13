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
        /// <returns></returns>
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
        /// <returns></returns>
        [TestMethod]
        [ExpectedException(typeof(System.Net.WebException))]
        public async Task AuthenticateFailTest()
        {
            var session = new Api.Session("test@test.com", "someinvalidpassword");

            await session.Authenticate();
        }

        /// <summary>
        /// Test if the Base64 Credential Encoding works properly
        /// </summary>
        [TestMethod]
        public void ValidateEncodedCredentialsTest()
        {
            var session = new Api.Session(Username, Password);

            var base64DecodedCredentials = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(session.CredentialsEncoded));
            Assert.AreEqual(base64DecodedCredentials, $"{Username}:{Password}", "Base64 Credential Decoding failed");
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
        /// Test if the doorbot history events can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetDoorbotsHistoryTest()
        {
            var session = new Api.Session(Username, Password);
            await session.Authenticate();

            var doorbotHistory = await session.GetDoorbotsHistory();
            Assert.IsTrue(doorbotHistory.Count > 0, "No doorbot history items returned");
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
