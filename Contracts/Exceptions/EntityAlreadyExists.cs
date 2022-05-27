using System;

namespace Contracts.Exceptions
{
    public class EntityAlreadyExists : Exception
    {
        public EntityAlreadyExists(string message) : base(message)
        {
            
        }
    } 
}