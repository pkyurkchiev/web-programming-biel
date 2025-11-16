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

        public List<string> Errors { get; } = [];
        public string newDescription = null;
        private int asyncCount = 0;
        public bool Loading
        {
            get => asyncCount > 0;
        }
        public string NewDescription
        {
            get => newDescription;
            set
            {
                newDescription = value;
                _newWorkOut.Description = value;
                List<ValidationResult> results = [];
                ValidationContext validation = new(_newWorkOut);
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
                return Errors.Count != 0;
            }
        }

        #region async operation
        private void StartAsyncOperation()
        {
            var cur = Loading;
            asyncCount++;
            if (cur != Loading)
            {
                RaisePropChange(nameof(Loading));
            }
        }

        private void EndAsyncOperation()
        {
            var cur = Loading;
            asyncCount--;
            if (cur != Loading)
            {
                RaisePropChange(nameof(Loading));
            }
        }
        #endregion

        #region methods
        public async Task AddNewAsync()
        {
            if (!string.IsNullOrWhiteSpace(NewDescription) && !ValidationErrors)
            {
                WorkOut newItem = new() { Description = NewDescription };
                StartAsyncOperation();
                await _dataAccess.AddAsync(newItem);
                EndAsyncOperation();
                NewDescription = string.Empty;
                RaisePropChange(nameof(WorkOutAsync));
            }
        }

        public async Task<IEnumerable<WorkOut>> WorkOutAsync()
        {
            StartAsyncOperation();
            var result = await _dataAccess.GetAsync(showAll, sortByCreatedOn, SortByCompletedOn);
            EndAsyncOperation();
            return result;
        }

        public async Task DeleteAsync(WorkOut workOut)
        {
            StartAsyncOperation();
            await _dataAccess.DeleteAsync(workOut);
            EndAsyncOperation();
            RaisePropChange(nameof(WorkOutAsync));
        }

        public async Task MarkWorkOutAsDoneAsync(WorkOut workOut)
        {
            workOut.MarkComplete();
            StartAsyncOperation();
            await _dataAccess.UpdateAsync(workOut);
            EndAsyncOperation();
            RaisePropChange(nameof(WorkOutAsync));
        }
        #endregion

        #region markers
        private bool showAll;
        public bool ShowAll
        {
            get => showAll;
            set
            {
                if (value != showAll)
                {
                    showAll = value;
                    RaisePropChange(nameof(ShowAll), true);
                }
            }
        }

        private bool sortByCreatedOn;
        public bool SortByCreatedOn
        {
            get => sortByCreatedOn;
            set
            {
                if (value != sortByCreatedOn)
                {
                    sortByCreatedOn = value;
                    RaisePropChange(nameof(SortByCreatedOn), true);
                }
            }
        }

        private bool sortByCompletedOn;
        public bool SortByCompletedOn
        {
            get => sortByCompletedOn;
            set
            {
                if (value != sortByCompletedOn)
                {
                    sortByCompletedOn = value;
                    RaisePropChange(nameof(SortByCompletedOn), true);
                }
            }
        }
        #endregion

        #region event
        private void RaisePropChange(string property, bool includeWorkOuts = false)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            if (includeWorkOuts)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WorkOutAsync)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
