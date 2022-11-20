using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WOM.Models;

namespace WOM.Client.Data
{
    public class MockWorkOutSimpleData : IWorkOutDataAccess
    {
        private readonly List<WorkOut> _database = new(new WorkOut[]
{
            new WorkOut
            {
                Id = 1,
                Complete = true,
                Description = "Biceps triceps workout",
                MarkedComplete = DateTime.UtcNow.AddDays(-1),
                Created = DateTime.UtcNow.AddDays(-2)
            },
            new WorkOut
            {
                Id = 2,
                Complete = false,
                Description = "Back workout"
            }
        });

        public Task<bool> DeleteAsync(WorkOut item)
        {
            var delete = _database.Where(WorkOut => WorkOut.Id == item.Id).FirstOrDefault();
            if (delete != null)
            {
                _database.Remove(delete);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<IEnumerable<WorkOut>> GetAsync(bool showAll, bool sortByCreatedOn, bool sortByCompletedOn)
        {
            IQueryable<WorkOut> query = _database.AsQueryable();
            if (sortByCreatedOn == true)
            {
                sortByCompletedOn = false;
            }
            if (!showAll)
            {
                query = query.Where(WorkOut => !WorkOut.Complete);
            }
            if (showAll && sortByCompletedOn)
            {
                query = query.OrderBy(WorkOut => -(WorkOut.MarkedComplete.HasValue ?
                    WorkOut.MarkedComplete.Value.Ticks : -(long.MaxValue - WorkOut.Created.Ticks)));
            }
            else if (sortByCreatedOn)
            {
                query = query.OrderBy(WorkOut => -WorkOut.Created.Ticks);
            }
            else
            {
                query = query.OrderBy(WorkOut => WorkOut.Description);
            }
            return Task.FromResult(query.AsEnumerable());
        }

        public Task<WorkOut> GetAsync(int id)
        {
            return Task.FromResult(_database.Where(item => item.Id == id).FirstOrDefault());
        }

        public Task<WorkOut> AddAsync(WorkOut item)
        {
            List<ValidationResult> results = new();
            var validation = new ValidationContext(item);
            if (Validator.TryValidateObject(item, validation, results))
            {
                item.Id = _database.Max(WorkOut => WorkOut.Id) + 1;
                _database.Add(item);
                return Task.FromResult(item);
            }
            else
            {
                throw new ValidationException();
            }
        }

        public Task<WorkOut> UpdateAsync(WorkOut item)
        {
            List<ValidationResult> results = new();
            ValidationContext validation = new(item);
            if (Validator.TryValidateObject(item, validation, results))
            {
                var dbItem = _database.Where(WorkOut => WorkOut.Id == item.Id).First();
                if (!dbItem.Complete && item.Complete)
                {
                    dbItem.MarkComplete();
                }
                dbItem.Description = item.Description;
                return Task.FromResult(dbItem);
            }
            else
            {
                throw new ValidationException();
            }
        }
    }
}
