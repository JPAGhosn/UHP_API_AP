using System;

namespace UHP.Application.Exceptions
{
    public class LockedEntityException : Exception
    {
        public LockedEntityException(string name, string key ,string reason)
            : base($"Entity {name} with Key {key} is locked" + reason)
        {
        }
    }
}