using System.Collections.Generic;
using System.Threading.Tasks;
using WOM.Models;

namespace WOM.Client.Data
{
    public interface IWorkOutDataAccess
    {
        Task<IEnumerable<WorkOut>> GetAsync(bool showAll, bool byCreated, bool byCompleted);
        Task<WorkOut> GetAsync(int id);
        Task<WorkOut> AddAsync(WorkOut itemToAdd);
        Task<WorkOut> UpdateAsync(WorkOut item);
        Task<bool> DeleteAsync(WorkOut item);
    }
}
