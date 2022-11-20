using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using WOM.Models;

namespace WOM.Client.Data
{
    public class WorkOutSimpleData : IWorkOutDataAccess
    {
        private readonly IODataClient _client;

        public WorkOutSimpleData(HttpClient client)
        {
            client.BaseAddress = new Uri("http://localhost:52949/odata");
            var settings = new ODataClientSettings(client);
            _client = new ODataClient(settings);
        }

        public async Task<WorkOut> AddAsync(WorkOut item)
        {
            var results = new List<ValidationResult>();
            var validation = new ValidationContext(item);
            if (Validator.TryValidateObject(item, validation, results))
            {
                return await _client.For<WorkOut>().Set(item).InsertEntryAsync();
            }
            else
            {
                throw new ValidationException();
            }
        }

        public Task<bool> DeleteAsync(WorkOut item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WorkOut>> GetAsync(bool showAll, bool sortByCreatedOn, bool sortByCompletedOn)
        {
            return await _client.For<WorkOut>().FindEntriesAsync();
        }

        public Task<WorkOut> UpdateAsync(WorkOut item)
        {
            throw new NotImplementedException();
        }
    }
}
