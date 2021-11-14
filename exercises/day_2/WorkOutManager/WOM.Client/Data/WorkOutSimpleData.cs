using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public async Task<bool> DeleteAsync(WorkOut item)
        {
            await _client.For<WorkOut>().Key(item.Id).DeleteEntryAsync();
            return true;
        }

        public async Task<IEnumerable<WorkOut>> GetAsync(bool showAll, bool sortByCreatedOn, bool sortByCompletedOn)
        {
            var helper = _client.For<WorkOut>();
            if (!showAll)
            {
                helper.Filter(workOut => !workOut.Complete);
            }
            if (showAll && sortByCompletedOn)
            {
                helper.OrderByDescending(workOut => workOut.MarkedComplete)
                      .ThenByDescending(workOut => workOut.Created);
            }
            else if (sortByCreatedOn)
            {
                helper.OrderByDescending(workOut => workOut.Created);
            }
            else 
            {
                helper.OrderBy(workOut => workOut.Description);
            }

            return await helper.FindEntriesAsync();
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
