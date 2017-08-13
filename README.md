# Ring API
This library for C# will allow you to easily communicate with the Ring API and retrieve details about your Ring doorbells and Ring chimes.

## Version History

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

Check out the UnitTest project in this solution for full insight in the possibilities and working code samples.

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
