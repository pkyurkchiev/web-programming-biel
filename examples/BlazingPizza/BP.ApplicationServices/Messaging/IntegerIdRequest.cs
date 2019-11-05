using System;

namespace BP.ApplicationServices.Messaging
{
    public abstract class GuidIdRequest : ServiceRequestBase
    {
        private Guid _id;

        public GuidIdRequest(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentException("ID must be positive.");
            }
            _id = id;
        }

        public Guid Id { get { return _id; } }
    }
}
