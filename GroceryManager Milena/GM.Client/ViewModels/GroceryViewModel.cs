using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GM.Client.Data;
using GM.Models;

namespace GM.Client.ViewModels {
    public class GroceryViewModel : INotifyPropertyChanged {
        private readonly IGroceryDataAccess _dataAccess;
        public readonly Grocery _newGrocery = new Grocery();

        public GroceryViewModel(IGroceryDataAccess dataAccess) {
            _dataAccess = dataAccess;
        }

        public List<string> Errors { get; } = new List<string>();

        public bool ValidationErrors {
            get { return Errors.Any(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropChange(string property, bool includeGroceries = false) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            if (includeGroceries) {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GroceryAsync)));
            }
        }

        public async Task<IEnumerable<Grocery>> GroceryAsync() {
            return await _dataAccess.GetAsync(showAll, sortByDateOfManufactoring, sortByDateOfExpiration);
        }

        public string newName = null;
        public string NewName {
            get => newName;
            set {
                newName = value;
                _newGrocery.Name = value;
                var results = new List<ValidationResult>();
                var validation = new ValidationContext(_newGrocery);
                Errors.Clear();
                if (!Validator.TryValidateObject(_newGrocery, validation, results)) {
                    foreach (var result in results) {
                        Errors.Add(result.ErrorMessage);
                    }
                    RaisePropChange(nameof(Errors));
                }
                RaisePropChange(nameof(NewName));
            }
        }

        public async Task AddItemAsync() {
            if (!string.IsNullOrWhiteSpace(NewName) && !ValidationErrors) {
                var newItem = new Grocery { Name = NewName };
                StartAsyncOperation();
                await _dataAccess.AddItemAsync(newItem);
                EndAsyncOperation();
                newName = string.Empty;
                RaisePropChange(nameof(GroceryAsync));
            }
        }

        public async Task RemoveItemAsync(Grocery grocery) {
            StartAsyncOperation();
            await _dataAccess.RemoveItemAsync(grocery);
            EndAsyncOperation();
            RaisePropChange(nameof(GroceryAsync));
        }

        public async Task MarkGroceryAsExpiredAsync(Grocery grocery) {
            grocery.MarkAsExpire();
            StartAsyncOperation();
            await _dataAccess.UpdateAsync(grocery);
            EndAsyncOperation();
            RaisePropChange(nameof(GroceryAsync));
        }

        #region markers
        private bool showAll;
        public bool ShowAll {
            get => showAll;
            set {
                if (value != ShowAll) {
                    showAll = value;
                    RaisePropChange(nameof(ShowAll), true);
                }
            }

        }

        private bool sortByDateOfExpiration;
        public bool SortByDateOfExpiration {
            get => sortByDateOfExpiration;
            set {
                if (value != SortByDateOfExpiration) {
                    sortByDateOfExpiration = value;
                    RaisePropChange(nameof(SortByDateOfExpiration), true);
                }
            }
        }

        private bool sortByDateOfManufactoring;
        public bool SortByDateOfManufactoring {
            get => sortByDateOfManufactoring;
            set {
                if (value != SortByDateOfManufactoring) {
                    sortByDateOfManufactoring = value;
                    RaisePropChange(nameof(SortByDateOfManufactoring), true);
                }
            }
        }
        #endregion

        #region async Loader
        public int asyncCount = 0;
        public bool Loading {
            get => asyncCount > 0;
        }

        private void StartAsyncOperation() {
            var cur = Loading;
            asyncCount++;
            if (cur != Loading) {
                RaisePropChange(nameof(Loading));
            }
        }

        private void EndAsyncOperation() {
            var cur = Loading;
            asyncCount--;
            if (cur != Loading) {
                RaisePropChange(nameof(Loading));
            }
        }
        #endregion
    }
}
