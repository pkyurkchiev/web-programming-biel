using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WOM.Models;

namespace WOM.Client.Data
{
    public interface IWorkOutDataAccess
    {
        Task<WorkOut> AddAsync(WorkOut item);
        Task<bool> DeleteAsync(WorkOut item);
        Task<IEnumerable<WorkOut>> GetAsync(bool showAll, bool sortByCreatedOn, bool sortByCompletedOn);
        Task<WorkOut> UpdateAsync(WorkOut item);
    }
}
