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

        public bool Loading
        {
            get => _asyncCount > 0;
        }

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

        private bool _showCompleted;

        public bool ShowCompleted
        {
            get => _showCompleted;
            set
            {
                if (value != _showCompleted)
                {
                    _showCompleted = value;
                    RaisePropChange(nameof(ShowCompleted), true);
                }
            }
        }

        private bool _sortByCompleted;

        public bool SortByCompleted
        {
            get => _sortByCompleted;
            set
            {
                if (value != _sortByCompleted)
                {
                    _sortByCompleted = value;
                    RaisePropChange(nameof(SortByCompleted), true);
                }
            }
        }

        private bool _sortByCreated;

        public bool SortByCreated
        {
            get => _sortByCreated;
            set
            {
                if (value != _sortByCreated)
                {
                    _sortByCreated = value;
                    RaisePropChange(nameof(SortByCreated), true);
                }
            }
        }

        private void StartAsyncOperation()
        {
            var cur = Loading;
            _asyncCount++;
            if (cur != Loading)
            {
                RaisePropChange(nameof(Loading));
            }
        }

        private void EndAsyncOperation()
        {
            var cur = Loading;
            _asyncCount--;
            if (cur != Loading)
            {
                RaisePropChange(nameof(Loading));
            }
        }

        public async Task MarkdoneAsync(WorkOut workOutItem)
        {
            workOutItem.MarkComplete();
            StartAsyncOperation();
            await _dataAccess.UpdateAsync(workOutItem);
            EndAsyncOperation();
            RaisePropChange(nameof(WorkOutsAsync));
        }

        public async Task DeleteAsync(WorkOut workOutItem)
        {
            StartAsyncOperation();
            await _dataAccess.DeleteAsync(workOutItem);
            EndAsyncOperation();
            RaisePropChange(nameof(WorkOutsAsync));
        }

        public async Task AddNewAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewDescription) && !ValidationHasErrors)
            {
                var newItem = new WorkOut { Description = NewDescription };
                StartAsyncOperation();
                await _dataAccess.AddAsync(newItem);
                EndAsyncOperation();
                NewDescription = string.Empty;
                RaisePropChange(nameof(WorkOutsAsync));
            }
        }

        public async Task<IEnumerable<WorkOut>> WorkOutsAsync()
        {
            StartAsyncOperation();
            var result = await _dataAccess.GetAsync(_showCompleted, _sortByCreated, _sortByCompleted);
            EndAsyncOperation();
            return result;
        }

        private void RaisePropChange(string property, bool includeWorkOuts = false)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            if (includeWorkOuts)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WorkOutsAsync)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
