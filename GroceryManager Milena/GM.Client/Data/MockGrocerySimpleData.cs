using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GM.Models;

namespace GM.Client.Data {
    public class MockGrocerySimpleData : IGroceryDataAccess {
        private readonly List<Grocery> _database = new List<Grocery>(new Grocery[] {
            new Grocery {
                Id = 1,
                IsExpire = true,
                Name = "Coffee",
                DateOfExpiration = DateTime.UtcNow.AddDays(-1),
                DateOfManufactoring = DateTime.UtcNow.AddDays(-2)
            },
            new Grocery {
                Id = 2,
                IsExpire = false,
                Name = "Biscuits",
                DateOfExpiration = DateTime.UtcNow.AddDays(-3),
                DateOfManufactoring = DateTime.UtcNow.AddDays(-4)
            },
            new Grocery {
                Id = 3,
                IsExpire = false,
                Name = "Chocolate",
                DateOfExpiration = DateTime.UtcNow.AddDays(-3),
                DateOfManufactoring = DateTime.UtcNow.AddDays(-4)
            }


        });

        public Task<bool> RemoveItemAsync(Grocery item) {
            var itemToRemove = _database.Where(Grocery => Grocery.Id == item.Id).FirstOrDefault();
            if (itemToRemove != null) {
                _database.Remove(itemToRemove);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<IEnumerable<Grocery>> GetAsync(bool showAll, bool sortByDateOfManufactoring, bool sortByDateOfExpiration) {
            IQueryable<Grocery> query = _database.AsQueryable();
            if (sortByDateOfManufactoring == true) {
                sortByDateOfExpiration = false;
            }
            if (!showAll) {
                query = query.Where(Grocery => !Grocery.IsExpire);
            }
            if (showAll && sortByDateOfExpiration) {
                query = query.OrderBy(Grocery => -(Grocery.DateOfExpiration.HasValue ?
                    Grocery.DateOfExpiration.Value.Ticks : -(long.MaxValue - Grocery.DateOfManufactoring.Ticks)));
            }
            else if (sortByDateOfManufactoring) {
                query = query.OrderBy(Grocery => -Grocery.DateOfManufactoring.Ticks);
            }
            else {
                query = query.OrderBy(Grocery => Grocery.Name);
            }
            return Task.FromResult(query.AsEnumerable());
        }

        public Task<Grocery> GetAsync(int id) {
            return Task.FromResult(_database.Where(item => item.Id == id).FirstOrDefault());
        }

        public Task<Grocery> AddItemAsync(Grocery item) {
            var results = new List<ValidationResult>();
            var validation = new ValidationContext(item);
            if (Validator.TryValidateObject(item, validation, results)) {
                item.Id = _database.Max(Grocery => Grocery.Id) + 1;
                _database.Add(item);
                return Task.FromResult(item);
            }
            else {
                throw new ValidationException();
            }
        }

        public Task<Grocery> UpdateAsync(Grocery item) {
            var results = new List<ValidationResult>();
            var validation = new ValidationContext(item);
            if (Validator.TryValidateObject(item, validation, results)) {
                var dbItem = _database.Where(Grocery => Grocery.Id == item.Id).First();
                if (!dbItem.IsExpire && item.IsExpire) {
                    dbItem.MarkAsExpire();
                }
                dbItem.Name = item.Name;
                return Task.FromResult(dbItem);
            }
            else {
                throw new ValidationException();
            }
        }
    }
}