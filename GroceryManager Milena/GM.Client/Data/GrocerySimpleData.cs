using Simple.OData.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using GM.Models;

namespace GM.Client.Data {
    public class GrocerySimpleData : IGroceryDataAccess {
        private readonly IODataClient _client;
        public GrocerySimpleData(HttpClient client) {
            client.BaseAddress = new Uri("https://localhost:44303/odata");
            var setting = new ODataClientSettings(client);
            _client = new ODataClient(setting);
        }
        public async Task<Grocery> AddItemAsync(Grocery item) {
            var results = new List<ValidationResult>();
            var validation = new ValidationContext(item);
            if (Validator.TryValidateObject(item, validation, results)) {
                return await _client.For<Grocery>().Set(item).InsertEntryAsync();
            }
            else {
                throw new ValidationException();
            }
        }

        public async Task<bool> RemoveItemAsync(Grocery item) {
            await _client.For<Grocery>().Key(item.Id).DeleteEntryAsync();
            return true;
        }

        public async Task<IEnumerable<Grocery>> GetAsync(bool showAll, bool sortByDateOfManufactoring, bool sortByDateOfExpiration) {
            var helper = _client.For<Grocery>();
            if (!showAll) {
                helper.Filter(w => !w.IsExpire);
            }
            else if (showAll && sortByDateOfExpiration) {
                helper.OrderByDescending(w => w.DateOfExpiration)
                    .ThenByDescending(w => w.DateOfManufactoring);
            }
            else if (sortByDateOfManufactoring) {
                helper.OrderByDescending(w => w.DateOfManufactoring);
            }
            else {
                helper.OrderBy(w => w.Name);
            }
            
            return await helper.FindEntriesAsync();
        }

        public async Task<Grocery> UpdateAsync(Grocery item) {
            var results = new List<ValidationResult>();
            var validation = new ValidationContext(item);
            if (Validator.TryValidateObject(item, validation, results)) {
                await _client.For<Grocery>().Key(item.Id).Set(item).UpdateEntryAsync();
                return item;
            }
            else {
                throw new ValidationException();
            }
        }
    }
}
