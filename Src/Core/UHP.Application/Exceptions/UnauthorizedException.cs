using System;

namespace UHP.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string reason)
            : base($"Unauthorized Access: " + reason)
        {
        }
    }
}