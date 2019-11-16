using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using WOM.Models;

namespace WOM.Client.Data
{
    public class WorkOutSimpleOData : IWorkOutDataAccess
    {
        private readonly IODataClient _client;

        public WorkOutSimpleOData(HttpClient client)
        {
            client.BaseAddress = new Uri("https://localhost:44383/odata");
            var settings = new ODataClientSettings(client);
            _client = new ODataClient(settings);
        }

        public async Task<WorkOut> AddAsync(WorkOut itemToAdd)
        {
            var results = new List<ValidationResult>();
            var validation = new ValidationContext(itemToAdd);
            if (Validator.TryValidateObject(itemToAdd, validation, results))
            {
                return await _client.For<WorkOut>().Set(itemToAdd).InsertEntryAsync();
            }
            else
            {
                throw new ValidationException();
            }
        }

        public async Task<bool> DeleteAsync(WorkOut item)
        {
            await _client.For<WorkOut>().Key(item.Id).DeleteEntryAsync();
            return true;
        }

        public async Task<IEnumerable<WorkOut>> GetAsync(bool showAll, bool byCreated, bool byCompleted)
        {
            var helper = _client.For<WorkOut>();
            if (!showAll)
            {
                helper.Filter(workOut => !workOut.Complete);
            }
            if (showAll && byCompleted)
            {
                helper.OrderByDescending(workOut => workOut.MarkedComplete)
                    .ThenByDescending(workOut => workOut.Created);
            }
            else if (byCreated)
            {
                helper.OrderByDescending(workOut => workOut.Created);
            }
            else
            {
                helper.OrderBy(workOut => workOut.Description);
            }
            return await helper.FindEntriesAsync();
        }

        public Task<WorkOut> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<WorkOut> UpdateAsync(WorkOut item)
        {
            var results = new List<ValidationResult>();
            var validation = new ValidationContext(item);
            if (Validator.TryValidateObject(item, validation, results))
            {
                await _client.For<WorkOut>().Key(item.Id).Set(item).UpdateEntryAsync();
                return item;
            }
            else
            {
                throw new ValidationException();
            }
        }
    }
}
