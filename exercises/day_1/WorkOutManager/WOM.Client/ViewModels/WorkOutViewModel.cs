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
        private readonly WorkOut _newWorkOut = new();

        public WorkOutViewModel(IWorkOutDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<string> Errors { get; } = new();
        public string newDescription = null;
        public string NewDescription
        {
            get => newDescription;
            set
            {
                newDescription = value;
                _newWorkOut.Description = value;
                var results = new List<ValidationResult>();
                var validation = new ValidationContext(_newWorkOut);
                Errors.Clear();
                if (!Validator.TryValidateObject(_newWorkOut, validation, results))
                {
                    foreach (var result in results)
                    {
                        Errors.Add(result.ErrorMessage);
                    }
                    RaisePropChange(nameof(Errors));
                }
                RaisePropChange(nameof(NewDescription));
            }
        }
        public bool ValidationErrors
        {
            get
            {
                return Errors.Any();
            }
        }

        public async Task AddNewAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewDescription) && !ValidationErrors)
            {
                WorkOut newItem = new() { Description = NewDescription };
                await _dataAccess.AddAsync(newItem);
                NewDescription = string.Empty;
                RaisePropChange(nameof(WorkOutAsync));
            }
        }

        public async Task<IEnumerable<WorkOut>> WorkOutAsync()
        {
            return await _dataAccess.GetAsync(true, false, false);
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
