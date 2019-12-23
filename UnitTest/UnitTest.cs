using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

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
        /// Two factor authentication token to use to connect to the Ring API
        /// </summary>
        public string TwoFactorAuthenticationToken => ConfigurationManager.AppSettings["TwoFactorAuthenticationToken"];

        /// <summary>
        /// Test the scenario where the authentication should succeed
        /// </summary>
        [TestMethod]
        public async Task AuthenticateSuccessTest()
        {
            var session = new Api.Session(Username, Password);

            Api.Entities.Session authResult = null;
            try
            {
                authResult = await session.Authenticate(twoFactorAuthCode: TwoFactorAuthenticationToken);
            }
            catch(Api.Exceptions.TwoFactorAuthenticationRequiredException)
            {
                Assert.Fail("Ring account requires two factor authentication. Add the token received through text message to the config file as 'TwoFactorAuthenticationToken' and run the test again.");
            }
            catch (Api.Exceptions.TwoFactorAuthenticationIncorrectException)
            {
                Assert.Fail("The two factor authentication token provided in the config file as 'TwoFactorAuthenticationToken' is invalid or has expired.");
            }
            Assert.IsFalse(authResult == null || string.IsNullOrEmpty(authResult.Profile.AuthenticationToken), "Failed to authenticate");
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
            await session.Authenticate(twoFactorAuthCode: TwoFactorAuthenticationToken);

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
            await session.Authenticate(twoFactorAuthCode: TwoFactorAuthenticationToken);

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
            await session.Authenticate(twoFactorAuthCode: TwoFactorAuthenticationToken);

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
            var limit = 250;

            var session = new Api.Session(Username, Password);
            await session.Authenticate(twoFactorAuthCode: TwoFactorAuthenticationToken);

            var doorbotHistory = await session.GetDoorbotsHistory(limit);
            Assert.IsTrue(doorbotHistory.Count > 0, "No doorbot history items returned");
            Assert.IsTrue(doorbotHistory.Count == limit, $"{doorbotHistory.Count} doorbot history items returned while {limit} were expected");
        }

        /// <summary>
        /// Test if the doorbot history events can be retrieved within a specific timeframe
        /// </summary>
        [TestMethod]
        public async Task GetDoorbotsHistoryByDateSpanTest()
        {
            var startDate = DateTime.Now.AddDays(-30);
            var endDate = DateTime.Now.AddDays(-1);

            var session = new Api.Session(Username, Password);
            await session.Authenticate(twoFactorAuthCode: TwoFactorAuthenticationToken);

            var doorbotHistory = await session.GetDoorbotsHistory(startDate, endDate);
            Assert.IsTrue(doorbotHistory.Count > 0, "No doorbot history items returned");
            Assert.AreEqual(0, doorbotHistory.Count(h => !h.CreatedAtDateTime.HasValue || (h.CreatedAtDateTime.Value > endDate && h.CreatedAtDateTime.Value < startDate)), "Doorbot history items have been returned which don't fall within the provided period");
        }

        /// <summary>
        /// Test if the recording for a doorbot history event can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetDoorbotsHistoryRecordingByIdTest()
        {
            var session = new Api.Session(Username, Password);
            await session.Authenticate(twoFactorAuthCode: TwoFactorAuthenticationToken);

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
            await session.Authenticate(twoFactorAuthCode: TwoFactorAuthenticationToken);

            var doorbotHistory = await session.GetDoorbotsHistory();

            Assert.IsTrue(doorbotHistory.Count > 0, "No doorbot history events were found");

            var tempFilePath = Path.GetTempFileName();

            await session.GetDoorbotHistoryRecording(doorbotHistory[0], tempFilePath);

            File.Delete(tempFilePath);
        }
    }
}
