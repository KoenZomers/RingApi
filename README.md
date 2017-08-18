# Ring API
This library for C# will allow you to easily communicate with the Ring API and retrieve details about your Ring doorbells and Ring chimes.

## Version History

0.2 - released August 18, 2017

- Added the option to specify the device information in the Authenticate method

0.1 - released August 13, 2017

- Initial version

## System Requirements

This API is built using the Microsoft .NET 4.6.2 framework and is fully asynchronous

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
