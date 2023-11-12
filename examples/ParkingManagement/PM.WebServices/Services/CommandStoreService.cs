using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PM.WebServices.Models;
using System;

namespace PM.WebServices.Services
{
    public class CommandStoreService
    {
        private readonly DbContext _dbContext;
        private readonly AuthenticationService _authenticationService;

        public CommandStoreService(
            DbContext dbContext,
            AuthenticationService authenticationService
        )
        {
            _dbContext = dbContext;
            _authenticationService = authenticationService;
        }

        public void Push(object command)
        {
            _dbContext.Set<Command>().Add(
                new Command
                {
                    Type = command.GetType().Name,
                    Data = JsonConvert.SerializeObject(command),
                    CreatedAt = DateTime.Now,
                    UserId = _authenticationService.GetUserId()
                }
            );
            _dbContext.SaveChanges();
        }
    }
}
