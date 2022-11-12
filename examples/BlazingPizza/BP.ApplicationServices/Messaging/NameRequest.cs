using System;

namespace BP.ApplicationServices.Messaging
{
    public abstract class NameRequest : ServiceRequestBase
    {
        public NameRequest(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be empty string.");
            }

            Name = name;
        }

        public string Name { get; }
    }
}
