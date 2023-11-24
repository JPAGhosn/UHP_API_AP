using System;

namespace UHP.Application.Exceptions
{
    public class DuplicatedEmailException : Exception
    {
        public DuplicatedEmailException(string email)
            : base($"Email \"{email}\" already exists.")
        {
        }
    }
}