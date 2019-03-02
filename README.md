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

## Version History

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

- Authenticate to the Ring API
- Retrieve all registered Ring devices (Rings and Chimes) under your account
- Retrieve the event history of your Ring devices
- Download the movie recording of the event of your Ring devices

## Feedback

Any kind of feedback is welcome! Feel free to drop me an e-mail at mail@koenzomers.nl
