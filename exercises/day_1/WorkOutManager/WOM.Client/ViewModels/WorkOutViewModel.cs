using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WOM.Client.Data;
using WOM.Models;

namespace WOM.Client.ViewModels
{
    public class WorkOutViewModel : INotifyPropertyChanged
    {
        private readonly IWorkOutDataAccess _dataAccess;
        private readonly WorkOut _newWorkOut = new WorkOut();

        public WorkOutViewModel(IWorkOutDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public bool ValidationHasErrors
        {
            get 
            {
                return Errors.Any();
            }
        }

        public List<string> Errors { get; } = new List<string>();
        private string _newDescription = null;
        private int _asyncCount = 0;

        public string NewDescription
        {
            get => _newDescription;
            set 
            {
                if (value != _newDescription)
                {
                    _newDescription = value;
                    _newWorkOut.Description = value;
                    var context = new ValidationContext(_newWorkOut);
                    var results = new List<ValidationResult>();
                    Errors.Clear();
                    if (!Validator.TryValidateObject(_newWorkOut, context, results))
                    {
                        foreach (var item in results)
                        {
                            Errors.Add(item.ErrorMessage);
                        }
                        RaisePropChange(nameof(Errors));
                    }
                    RaisePropChange(nameof(NewDescription));
                }
            }
        }

        public async Task MarkdoneAsync(WorkOut workOutItem)
        {
            workOutItem.MarkComplete();
            await _dataAccess.UpdateAsync(workOutItem);
            RaisePropChange(nameof(WorkOutAsync));
        }

        public async Task DeleteAsync(WorkOut workOutItem)
        {
            await _dataAccess.DeleteAsync(workOutItem);
            RaisePropChange(nameof(WorkOutAsync));
        }

        public async Task AddNewAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewDescription) && !ValidationHasErrors)
            {
                var newItem = new WorkOut { Description = NewDescription };
                await _dataAccess.AddAsync(newItem);
                NewDescription = string.Empty;
                RaisePropChange(nameof(WorkOutAsync));
            }
        }

        public async Task<IEnumerable<WorkOut>> WorkOutAsync()
        {
            var result = await _dataAccess.GetAsync(true, false, false);
            return result;
        }

        private void RaisePropChange(string property, bool includeWorkOuts = false)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            if (includeWorkOuts)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WorkOutAsync)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
