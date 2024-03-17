using System;

namespace KoenZomers.Ring.UnitTest
{
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// Username to use to connect to the Ring API
        /// </summary>
#pragma warning disable CS8603 // Possible null reference return.
        public static string Username => ConfigurationManager.AppSettings["RingUsername"];
#pragma warning restore CS8603 // Possible null reference return.

        /// <summary>
        /// Password to use to connect to the Ring API
        /// </summary>
#pragma warning disable CS8603 // Possible null reference return.
        public static string Password => ConfigurationManager.AppSettings["RingPassword"];
#pragma warning restore CS8603 // Possible null reference return.

        /// <summary>
        /// Two factor authentication token to use to connect to the Ring API
        /// </summary>
        public static string TwoFactorAuthenticationToken
        {
#pragma warning disable CS8603 // Possible null reference return.
            get { return ConfigurationManager.AppSettings["TwoFactorAuthenticationToken"]; }
#pragma warning restore CS8603 // Possible null reference return.
            set
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (configFile.AppSettings.Settings["TwoFactorAuthenticationToken"] != null)
                {
                    configFile.AppSettings.Settings["TwoFactorAuthenticationToken"].Value = value;
                }
                else
                {
                    configFile.AppSettings.Settings.Add("TwoFactorAuthenticationToken", value);
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
        }
        /// <summary>
        /// Refresh token used to connect to the Ring API
        /// </summary>
        public static string RefreshToken
        {
#pragma warning disable CS8603 // Possible null reference return.
            get { return ConfigurationManager.AppSettings["RingRefreshToken"]; }
#pragma warning restore CS8603 // Possible null reference return.
            set
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (configFile.AppSettings.Settings["RingRefreshToken"] != null)
                {
                    configFile.AppSettings.Settings["RingRefreshToken"].Value = value;
                }
                else
                {
                    configFile.AppSettings.Settings.Add("RingRefreshToken", value);
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
        }

        /// <summary>
        /// Session set up by the initializer and used by the Unit Tests to perform actions against the Ring API
        /// </summary>
        public static Api.Session? session;

        /// <summary>
        /// Prepares the Unit Test by setting up a session to Ring
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize]
        public static async Task TestInitialize(TestContext testContext)
        {
            // Check if we have a refresh token to authenticate to Ring with
            if (string.IsNullOrEmpty(RefreshToken))
            {
                // No refresh token available, try to authenticate with the credentials from the config file
                session = new Api.Session(Username, Password);

                Api.Entities.Session? authResult = null;
                try
                {
                    authResult = await session.Authenticate(twoFactorAuthCode: TwoFactorAuthenticationToken);

                    if (!string.IsNullOrEmpty(TwoFactorAuthenticationToken))
                    {
                        // Clear the configured two factor authentication code in the configuration file after we've used it once as it won't be valid anymore next time
                        TwoFactorAuthenticationToken = string.Empty;
                    }
                }
                catch (Api.Exceptions.TwoFactorAuthenticationRequiredException)
                {
                    Assert.Fail("Ring account requires two factor authentication. Add the token received through text message to the config file as 'TwoFactorAuthenticationToken' and run the test again.");
                }
                catch (Api.Exceptions.TwoFactorAuthenticationIncorrectException)
                {
                    Assert.Fail("The two factor authentication token provided in the config file as 'TwoFactorAuthenticationToken' is invalid or has expired.");
                }
                Assert.IsFalse(authResult == null, "Failed to authenticate");

                // Store the refresh token for subsequent runs
                RefreshToken = session.OAuthToken.RefreshToken;
            }
            else
            {
                // Use the refresh token to set up a new session with Ring so we don't have to deal with the two factor authentication anymore
                session = await Api.Session.GetSessionByRefreshToken(RefreshToken);

                Assert.IsFalse(session == null || session.OAuthToken == null || string.IsNullOrEmpty(session.OAuthToken.AccessToken), "Failed to authenticate using refresh token");
            }
        }

        /// <summary>
        /// Test the scenario where the authentication would fail
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Api.Exceptions.AuthenticationFailedException))]
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
            if (!IsSessionActive()) return;

            Assert.IsNotNull(session, "No active session available");

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
            await Api.Session.GetSessionByRefreshToken("abcdefghijklmnopqrstuvwxyz");
        }

        /// <summary>
        /// Test if the devices can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetDevicesTest()
        {
            if (!IsSessionActive()) return;

            Assert.IsNotNull(session, "No active session available");

            var devices = await session.GetRingDevices();
            Assert.IsTrue(devices.Chimes.Count > 0 || devices.Doorbots.Count > 0 || devices.AuthorizedDoorbots.Count > 0 || devices.StickupCams.Count > 0, "No doorbots, stickup cams and/or chimes returned");
        }

        /// <summary>
        /// Test if the an SessionNotAuthenticatedException gets thrown when trying to retrieve the Ring devices without authenticating first
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Api.Exceptions.SessionNotAuthenticatedException))]
        public async Task GetDevicesUnauthenticatedTest()
        {
            var session = new Api.Session("", "");
            await session.GetRingDevices();
        }

        /// <summary>
        /// Test if the doorbot history events can be retrieved with the default amount of items
        /// </summary>
        [TestMethod]
        public async Task GetDoorbotsHistoryTest()
        {
            if (!IsSessionActive()) return;

            Assert.IsNotNull(session, "No active session available");

            var doorbotHistory = await session.GetDoorbotsHistory();
            Assert.IsTrue(doorbotHistory.Count > 0, "No doorbot history items returned");
            Assert.IsTrue(doorbotHistory.Count == 20, $"{doorbotHistory.Count} doorbot history items returned while 20 were expected");
        }

        /// <summary>
        /// Test if the doorbot history events can be retrieved only for a specific doorbot with the default amount of items
        /// </summary>
        [TestMethod]
        public async Task GetDoorbotsHistoryForSpecificDoorbotTest()
        {
            if (!IsSessionActive()) return;

            Assert.IsNotNull(session, "No active session available");

            // Get the available Ring devices
            var devices = await session.GetRingDevices();

            // Ensure there's at least one doorbot available
            if (devices.Doorbots.Count == 0 && devices.AuthorizedDoorbots.Count == 0)
            {
                Assert.Inconclusive("There are no Ring doorbots available under this account to perform this test with");
                return;
            }

            // Take the first doorbot to retrieve the historical items for
            var doorbot = devices.Doorbots.Count > 0 ? devices.Doorbots[0] : devices.AuthorizedDoorbots[0];

            // Get the historical items for the specific doorbot
            var doorbotHistory = await session.GetDoorbotsHistory(doorbotId: doorbot.Id);

            Assert.IsFalse(doorbotHistory.Count == 0, "No doorbot history items returned");
        }

        /// <summary>
        /// Test if the result if doorbot history events are tried to be retrieved only for a specific doorbot which does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(Api.Exceptions.DeviceUnknownException))]
        public async Task GetDoorbotsHistoryForSpecificNonExistingDoorbotTest()
        {
            if (!IsSessionActive()) return;

            Assert.IsNotNull(session, "No active session available");

            // Try getting the historical items for the a doorbot that does not exist
            await session.GetDoorbotsHistory(doorbotId: 1234567);
        }

        /// <summary>
        /// Test if the doorbot history events can be retrieved with a specific amount of items
        /// </summary>
        [TestMethod]
        public async Task GetDoorbotsHistoryWithLimitTest()
        {
            if (!IsSessionActive()) return;

            Assert.IsNotNull(session, "No active session available");

            var limit = 250;

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
            if (!IsSessionActive()) return;

            Assert.IsNotNull(session, "No active session available");

            var startDate = DateTime.Now.AddDays(-2);
            var endDate = DateTime.Now.AddDays(-1);

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
            if (!IsSessionActive()) return;

            Assert.IsNotNull(session, "No active session available");

            var doorbotHistory = await session.GetDoorbotsHistory();

            Assert.IsTrue(doorbotHistory.Count > 0, "No doorbot history events were found");

            var tempFilePath = Path.GetTempFileName();

            await session.GetDoorbotHistoryRecording(doorbotHistory[0].Id.ToString(), tempFilePath);

            File.Delete(tempFilePath);
        }

        /// <summary>
        /// Test if the recording for a doorbot history event can be retrieved
        /// </summary>
        [TestMethod]
        public async Task GetDoorbotsHistoryRecordingByInstanceTest()
        {
            if (!IsSessionActive()) return;

            Assert.IsNotNull(session, "No active session available");

            var doorbotHistory = await session.GetDoorbotsHistory(limit: 1);

            Assert.IsTrue(doorbotHistory.Count > 0, "No doorbot history events were found");

            var tempFilePath = Path.GetTempFileName();

            await session.GetDoorbotHistoryRecording(doorbotHistory[0], tempFilePath);

            File.Delete(tempFilePath);
        }

        /// <summary>
        /// Test if the recording for a doorbot history event can be shared
        /// </summary>
        [TestMethod]
        public async Task ShareRecordingTest()
        {
            if (!IsSessionActive()) return;

            Assert.IsNotNull(session, "No active session available");

            var doorbotHistory = await session.GetDoorbotsHistory(limit: 1);

            Assert.IsTrue(doorbotHistory.Count > 0, "No doorbot history events were found");

            await session.ShareRecording(doorbotHistory[0]);
        }

        /// <summary>
        /// Test if the latest snapshot from a doorbot can be downloaded
        /// </summary>
        [TestMethod]
        public async Task DownloadLatestSnapshotTest()
        {
            if (!IsSessionActive()) return;

            Assert.IsNotNull(session, "No active session available");

            var devices = await session.GetRingDevices();
            Assert.IsTrue(devices != null, "Unable to retrieve Ring devices");
            Assert.IsTrue((devices.AuthorizedDoorbots != null && devices.AuthorizedDoorbots.Count > 0) || (devices.Doorbots != null && devices.Doorbots.Count > 0), "Retrieved Ring devices do not contain any doorbots");

            var tempFilePath = Path.GetTempFileName();

            await session.GetLatestSnapshot(devices.AuthorizedDoorbots?.Count > 0 ? devices.AuthorizedDoorbots[0] : devices.Doorbots[0], tempFilePath);

            File.Delete(tempFilePath);
        }

        /// <summary>
        /// Test if requesting snapshots to be refreshed succeeds
        /// </summary>
        [TestMethod]
        public async Task UpdateSnapshotTest()
        {
            if (!IsSessionActive()) return;
            
            Assert.IsNotNull(session, "No active session available");

            var devices = await session.GetRingDevices();
            Assert.IsTrue(devices != null, "Unable to retrieve Ring devices");
            Assert.IsTrue((devices.AuthorizedDoorbots != null && devices.AuthorizedDoorbots.Count > 0) || (devices.Doorbots != null && devices.Doorbots.Count > 0), "Retrieved Ring devices do not contain any doorbots");

            await session.UpdateSnapshot((devices.AuthorizedDoorbots?.Count > 0 ? devices.AuthorizedDoorbots : devices.Doorbots)[0]);
        }

        /// <summary>
        /// Test if we can retrieve the date and time at which a snapshot was last taken from a Ring doorbot device
        /// </summary>
        [TestMethod]
        public async Task GetSnapshotTimestampTest()
        {
            if (!IsSessionActive()) return;

            Assert.IsNotNull(session, "No active session available");

            var devices = await session.GetRingDevices();
            Assert.IsTrue(devices != null, "Unable to retrieve Ring devices");
            Assert.IsTrue((devices.AuthorizedDoorbots != null && devices.AuthorizedDoorbots.Count > 0) || (devices.Doorbots != null && devices.Doorbots.Count > 0), "Retrieved Ring devices do not contain any doorbots");

            var doorbotSnapshotTimestamps = await session.GetDoorbotSnapshotTimestamp((devices.AuthorizedDoorbots?.Count > 0 ? devices.AuthorizedDoorbots : devices.Doorbots)[0]);

            Assert.IsTrue(doorbotSnapshotTimestamps.Timestamp.Count > 0, "No timestamps were returned for the doorbot");
            Assert.IsTrue(doorbotSnapshotTimestamps.Timestamp[0].Timestamp.HasValue, "Unable to define the date and time for the last snapshot of the doorbot");
        }

        /// <summary>
        /// Check if there is an active Ring session created by the class initializer
        /// </summary>
        /// <returns>True if there's an active session, false if not</returns>
        private bool IsSessionActive()
        {
            if (session == null)
            {
                Assert.Inconclusive("Test can't be done as there's no active session");
                return false;
            }

            return true;
        }
    }
}
