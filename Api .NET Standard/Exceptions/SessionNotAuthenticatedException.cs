using System;

namespace KoenZomers.Ring.Api.Exceptions
{
    /// <summary>
    /// Exception thrown when functionality is called which requires the session to be authenticated while it isn't yet
    /// </summary>
    public class SessionNotAuthenticatedException : Exception
    {
        public SessionNotAuthenticatedException() : base("This session has not yet been authenticated. Please call Authenticate() first.")
        {
        }
    }
}
