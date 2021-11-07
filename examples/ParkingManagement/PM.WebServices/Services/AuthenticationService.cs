using System;

namespace PM.WebServices.Services
{
    public class AuthenticationService
    {
        private readonly string _userId;

        public AuthenticationService()
        {
            _userId = Guid.NewGuid().ToString();
        }

        public string GetUserId() => _userId;
    }
}
