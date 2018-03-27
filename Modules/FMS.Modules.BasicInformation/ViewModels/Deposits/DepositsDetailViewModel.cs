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
    public class DepositsDetailViewModel : DetailViewModelBase, IDepositDetailViewModel
    {
        private IFMSService _fmsService;
        private DepositWrapper _depositWrapper;
        private AccountWrapper _selectedPrimaryAccount;

        public DepositsDetailViewModel(IEventAggregator eventAggregator,
                                       IMessageDialogService messageDialogService,
                                       IFMSService fmsService)
                                     : base(eventAggregator, messageDialogService)
        {
            Title = "Deposit Information";
            _fmsService = fmsService;
            PrimaryAccounts = new ObservableCollection<AccountWrapper>();
            Years = new ObservableCollection<YearLine>();
        }

        public ObservableCollection<AccountWrapper> PrimaryAccounts { get; }

        public ObservableCollection<YearLine> Years { get; }

        public AccountWrapper SelectedPrimaryAccount
        {
            get { return _selectedPrimaryAccount; }

            set
            {
                _selectedPrimaryAccount = value;
                RaisePropertyChanged();
            }
        }

        public DepositWrapper Deposit
        {
            get { return _depositWrapper; }

            private set
            {
                _depositWrapper = value;
                RaisePropertyChanged();
            }
        }

        public override async Task LoadAsync(int id) { }

        public async void LoadAsync(Deposit deposit)
        {
            Id = deposit.ID;

            PrimaryAccounts.Clear();

            var lookup = await _fmsService.GetAllAccounts();

            var years = await _fmsService.GetAllYears();

            foreach (var year in years)
            {
                Years.Add(year);
            }

            foreach (var item in lookup)
            {
                PrimaryAccounts.Add(new AccountWrapper(item));
            }

            SelectedPrimaryAccount = PrimaryAccounts.FirstOrDefault(acc => acc.ID == deposit.AccountID);
            
            InitializeDeposit(deposit);
        }

        private void InitializeDeposit(Deposit deposit)
        {
            Deposit = new DepositWrapper(deposit);

            Deposit.PropertyChanged += (s, e) =>
            {
                //if (!HasChanges)
                //{
                //    HasChanges = _friendRepository.HasChanges();
                //}
                if (e.PropertyName == nameof(Deposit.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if (e.PropertyName == nameof(Bank.Name))
                {
                    SetTitle();
                }

            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)DeleteCommand).RaiseCanExecuteChanged();
            if (Deposit.ID == 0)
            {
                // Little trick to trigger the validation
                Deposit.DepositAccNumber = "";
            }
            SetTitle();
        }

        private void SetTitle()
        {
            Title = $"{Deposit.DepositAccNumber}";
        }

        protected override bool OnDeleteCanExecute()
        {
            return (Deposit != null && Deposit.ID > 0);
        }

        protected override void OnDeleteExecute()
        {
            if (MessageDialogService.ShowOkCancelDialog("Do you really want to delete the deposit information?", "Delete Deposit") == MessageDialogResult.OK)
            {
                DeleteDepositInfo();
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return (Deposit != null && !Deposit.HasErrors);
        }

        protected override void OnSaveExecute()
        {
            SaveDepositInfo();
        }

        private async void SaveDepositInfo()
        {
            if (Deposit != null)
            {
                //set only FK values, not the navigation properties
                //otherwise EF insert navigation properties as new objects in disconnected approach

                Deposit.AccountID = SelectedPrimaryAccount.Model.ID;

                Deposit.YearID = Years[0].ID;
                
                var result = await Task.Run(() =>
                {
                    int idValue = 0;
                    try
                    {
                        idValue = _fmsService.SaveDeposit(Deposit.Model);
                    }
                    catch (Exception ex)
                    {
                        MessageDialogService.ShowInfoDialog($"Error Occured: { ex.Message}");
                    }
                    return idValue;
                });

                if (result > 0)
                {
                    Deposit.ID = result;
                    this.Id = result;

                    RaiseDetailSavedEvent(Deposit.ID, $"{Deposit.DepositAccNumber}");
                    MessageDialogService.ShowInfoDialog($"Deposit Information Saved For: { Deposit.DepositAccNumber}");
                }

                Deposit.SetFinalCalculatedValues();
            }
        }

        private async void DeleteDepositInfo()
        {
            if (Deposit != null)
            {
                var result = await Task.Run(() =>
                {
                    int idValue = 0;
                    try
                    {
                        idValue = _fmsService.DeleteDeposit(Deposit.Model);
                    }
                    catch (Exception ex)
                    {
                        MessageDialogService.ShowInfoDialog($"Error Occured: { ex.Message}");
                    }
                    return idValue;
                });

                if (result > 0)
                {
                    RaiseDetailDeletedEvent(this.Id); // here we pass the id of view model, (which is same the object it wrapping)
                    MessageDialogService.ShowInfoDialog($"Deposit Information Deleted For: { Deposit.DepositAccNumber}");
                }
            }
        }

        public bool HasChanges { get { return false; } }



    }
}
