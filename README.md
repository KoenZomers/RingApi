# Ring API
[![licence badge]][licence]
[![stars badge]][stars]
[![forks badge]][forks]
[![issues badge]][issues]

[licence badge]:https://img.shields.io/badge/license-MIT-blue.svg
[stars badge]:https://img.shields.io/github/stars/koenzomers/RingApi.svg
[forks badge]:https://img.shields.io/github/forks/koenzomers/RingApi.svg
[issues badge]:https://img.shields.io/github/issues/koenzomers/RingApi.svg

[licence]:https://github.com/koenzomers/RingApi/blob/master/LICENSE.md
[stars]:https://github.com/koenzomers/RingApi/stargazers
[forks]:https://github.com/koenzomers/RingApi/network
[issues]:https://github.com/koenzomers/RingApi/issues

This library for C# compiled against .NET Standard will allow you to easily communicate with the Ring API and retrieve details about your Ring doorbells and Ring chimes.

If you're just looking for a tool to download your Ring recordings, [go here instead](https://github.com/KoenZomers/RingRecordingDownload).

## Version History

0.4.0.2 - released December 24, 2019

 - Added support for AuthorizedDoorbots in the devices response from Ring. Thanks to [insane4sure](https://github.com/insane4sure) for reporting this in [issue 6](https://github.com/KoenZomers/RingApi/issues/6).

0.4.0.1 - released December 24, 2019

- Fixed issue where having certain special characters such as a + in your e-mail or password used to log on would make the authentication fail. Thanks to [insane4sure](https://github.com/insane4sure) for reporting this in [issue 5](https://github.com/KoenZomers/RingApi/issues/5).

0.4.0.0 - released December 23, 2019

- Added support for Multi Factor Authentication on Ring accounts. To trigger receiving the text message from Ring with the token, call `session.Authenticate()` first. Then once you have received the token, call `session.Authenticate(twoFactorAuthCode: "12345")` where you replace 12345 with the token you received. Once this returns the access token, you can use this access token to access the Ring API without any further multi factor authentication requirements anymore.

0.3.5.0 - released October 27, 2019

- Added method `public async Task RefreshSession()` which will try to renew the session based on the refresh token in the session
- Added method `public async Task EnsureSessionValid()` which validates if the current session is still valid and renews it if it isn't anymore. This method is called inside every method that retrieves data from the Ring service so you should not have to call this method yourself.

0.3.4.0 - released October 4, 2019

- Added method `public async Task<List<Entities.DoorbotHistoryEvent>> GetDoorbotsHistory(DateTime startDate, DateTime? endDate)` which allows for retrieving historical items between a specific date/time span. Note though that since the Ring API does not expose this functionality, it relies on retrieving historical items in batches until it has found all that fit within the date/time span, so it's not super efficient, but it works.'

0.3.3.0 - released October 4, 2019

- Further improvements to support Ring Stickup Cams
- It could be that this version introduces backwards compatibility issues and requires you to update your code. I.e. switching from non nullable types to nullable types. Keep this in mind when upgrading to this version.

0.3.2.0 - released August 9, 2019

- Added support for working with Ring Stickup Cams
- Removed CredentialsEncoded property in Session as it was no longer used and deprecated a few versions back

0.3.1.0 - released August 9, 2019

- Changed the implementation of GetDoorbotsHistory(int limit) so that it will return as many items as you request, instead of just a maximum of 100 items, even if you would provide a higher number. Discussed in [issue #2](https://github.com/KoenZomers/RingApi/issues/2).

0.3.0.1 - released March 2, 2019

- Added optional int parameter to GetDoorbotsHistory which allows setting a specific number of history items that should be returned. If you don't provide a number, it will default to the Ring default of the most recent 20 items

0.3.0.0 - released March 2, 2019

- Converted API library into .NET Standard so it can be used on non Windows platforms as well
- Upgraded the Unit Test NuGet Packages so the Unit Tests work again
- Upgraded to Newtonsoft JSON 12.0.1

0.2.2.1 - released September 13, 2018

- Fixed issue when using GetDoorbotHistoryRecording and it not downloading the actual recording. Thanks to Gary Quigley for reporting it!

0.2.2 - released July 2, 2018

- Ring seems to have switched off their old API command support. Updated the methods to use the new API.
- Added static `Api.Session.GetSessionByRefreshToken(string refreshToken)` method to support using OAuth Refresh Tokens for getting an Access Token.
- Ring seems to have introduced throttling protection against too many requests sent to their API which seems to kick in pretty easily. I've added a specific `Api.Exceptions.ThrottlingException` to notify you if the request has failed due too throttling. Just try it again in a few minutes and it typically works again. Check the `InnerException` of it for the glory details on why it failed.
- Added property `OAuthToken` on the `Api.Session` class which gives you access to the full OAuth Token retrieved during authentication against the Ring API.

0.2.1 - released June 28, 2018

- Ring had changed their authentication from Basic authentication to simple HTTP Form POST authentication. Updated the code to accommodate this. Thanks to Kevin Chemali for bringing this to my attention.
- Few additional new properties provided in the session by the Ring service are now mapped to the typed Session object.

0.2 - released August 18, 2017

- Added the option to specify the device information in the Authenticate method

0.1 - released August 13, 2017

- Initial version

## System Requirements

This API is built using Microsoft .NET Standard 2.0 and is fully asynchronous

## Usage Instructions

To communicate with the Ring API, add the NuGet package to your solution and add a using reference in your code:

```C#
using KoenZomers.Ring.Api;
```

Then create a new session instance using:

```C#
var session = new Session("your@email.com", "yourpassword");
```

Note that this line does not perform any communications with the Ring API yet. You need to manually trigger authenticate before you can start using the session:

```C#
await session.Authenticate();
```

If the Ring account you're connecting with has been set up with a two factor authentication requirement, wait for the text message from Ring to arrive on your mobile phone and run Authenticate again providing this code:

```C#
await session.Authenticate(twoFactorAuthCode: "12345");
```

If the account does not require two factor authentication, you can skip this step.

Once this succeeds, you can call one of the methods on the session instance to retrieve data, i.e.:

```C#
// Retrieves all recorded doorbell events
var doorbotHistory = await session.GetDoorbotsHistory();
```

To save a recording directly to your disk:

```C#
await session.GetDoorbotHistoryRecording("6000000004618901011", "c:\\temp\\recording.mp4");
```

### Unit Tests

Check out the UnitTest project in this solution for full insight in the possibilities and working code samples. If you want to run the Unit Tests, just copy the App.sample.config file in the UnitTest project to App.config and fill in your Ring username and password and you're good to go to run all tests. They will not make any changes to your Ring devices or Ring profile, just retrieve information, so you can run it without any risk.

## Available via NuGet

You can also pull this API in as a NuGet package by adding "KoenZomers.Ring.Api" or running:

Install-Package KoenZomers.Ring.Api

Package statistics: https://www.nuget.org/packages/KoenZomers.Ring.Api

## Current functionality

With this API at its current state you can:

- Authenticate to the Ring API with or without two factor authentication
- Retrieve all registered Ring devices (Rings and Chimes) under your account
- Retrieve the event history of your Ring devices
- Download the movie recording of the event of your Ring devices

## Feedback

Any kind of feedback is welcome! Feel free to drop me an e-mail at mail@koenzomers.nl
