using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GM.Models;

namespace GM.Client.Data {
    public interface IGroceryDataAccess {
        Task<Grocery> AddItemAsync(Grocery item);
        Task<bool> RemoveItemAsync(Grocery item);
        Task<IEnumerable<Grocery>> GetAsync(bool showAll, bool sortByDateOfManufactoring, bool sortByDateOfExpiration);
        Task<Grocery> UpdateAsync(Grocery item);
    }
}
