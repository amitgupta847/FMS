using FMS.Business;
using FMS.Infrastructure;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.BasicInformation
{
    public class AccountDetailViewModel : DetailViewModelBase, IAccountDetailViewModel
    {
        private IFMSService _fmsService;
        private AccountWrapper _accWrapper;

        private BankWrapper _selectedLinkedBank;

        public AccountDetailViewModel(IEventAggregator eventAggregator,
                                      IMessageDialogService msgService,
                                      IFMSService fmsService)
                : base(eventAggregator, msgService)
        {
            Title = "Account Information";
            _fmsService = fmsService;

            Banks = new ObservableCollection<BankWrapper>();
        }

        public ObservableCollection<BankWrapper> Banks { get; }

        public BankWrapper SelectedLinkedBank
        {
            get { return _selectedLinkedBank; }

            set
            {
                _selectedLinkedBank = value;
                RaisePropertyChanged();
            }
        }

        public AccountWrapper Account
        {
            get { return _accWrapper; }

            private set
            {
                _accWrapper = value;
                RaisePropertyChanged();
            }
        }

        public override async Task LoadAsync(int id) { }

        public async void LoadAsync(Account account)
        {
            Id = account.ID;

            Banks.Clear();

            var lookup = await _fmsService.GetAllBanks();

            foreach (var item in lookup)
            {
                Banks.Add(new BankWrapper(item));
            }

            SelectedLinkedBank = Banks.FirstOrDefault(bank => bank.ID == account.BankID);

            InitializeAccount(account);
        }

        private void InitializeAccount(Account account)
        {
            Account = new AccountWrapper(account);

            Account.PropertyChanged += (s, e) =>
            {
                //if (!HasChanges)
                //{
                //    HasChanges = _friendRepository.HasChanges();
                //}
                if (e.PropertyName == nameof(Account.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if (e.PropertyName == nameof(Account.AccountNumber))
                {
                    SetTitle();
                }

            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
            if (Account.ID == 0)
            {
                // Little trick to trigger the validation
                Account.AccountNumber = "";
            }
            SetTitle();
        }

        private void SetTitle()
        {
            Title = $"{Account.AccountNumber}";
        }

        protected override bool OnDeleteCanExecute()
        {
            return (Account != null && Account.ID > 0);
        }

        protected override void OnDeleteExecute()
        {
            if (MessageDialogService.ShowOkCancelDialog("Do you really want to delete the account information?", "Delete Account") == MessageDialogResult.OK)
            {
                DeleteAccountInfo();
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return (Account != null && !Account.HasErrors);
        }

        protected override void OnSaveExecute()
        {
            SaveAccountInfo();
        }

        private async void SaveAccountInfo()
        {
            if (Account != null)
            {
                //set only FK values, not the navigation properties
                //otherwise EF insert navigation properties as new objects in disconnected approach
                Account.BankID = SelectedLinkedBank.Model.ID;

                var result = await Task.Run(() =>
                {
                    int idValue = 0;
                    try
                    {
                        idValue = _fmsService.SaveAccount(Account.Model);
                    }
                    catch (Exception ex)
                    {
                        MessageDialogService.ShowInfoDialog($"Error Occured: { ex.Message}");
                    }
                    return idValue;
                });

                if (result > 0)
                {
                    Account.ID = result;
                    this.Id = result;

                    RaiseDetailSavedEvent(Account.ID, $"{Account.AccountNumber}");
                    MessageDialogService.ShowInfoDialog($"Account Information Saved For: { Account.AccountNumber}");
                }
            }
        }

        private async void DeleteAccountInfo()
        {
            if (Account != null)
            {
                var result = await Task.Run(() =>
                {
                    bool isSuccess = false;
                    try
                    {
                        isSuccess = _fmsService.DeleteAccount(Account.Model);
                    }
                    catch (Exception ex)
                    {
                        MessageDialogService.ShowInfoDialog($"Error Occured: { ex.Message}");
                    }
                    return isSuccess;
                });

                if (result)
                {
                    RaiseDetailDeletedEvent(this.Id); // here we pass the id of view model, (which is same the object it wrapping)
                    MessageDialogService.ShowInfoDialog($"Account Information Deleted For: { Account.AccountNumber}");
                }
            }
        }

        public bool HasChanges { get { return false; } }


    }
}
