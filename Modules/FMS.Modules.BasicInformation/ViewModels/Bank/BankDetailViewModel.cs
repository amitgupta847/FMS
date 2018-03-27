using FMS.Business;
using FMS.Infrastructure;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.BasicInformation
{
    public class BankDetailViewModel : DetailViewModelBase, IBankDetailViewModel
    {
        private IFMSService _fmsService;
        private BankWrapper _bankWrapper;

        public BankDetailViewModel(IEventAggregator eventAggregator,
                                   IMessageDialogService messageService,
                                   IFMSService fmsService)
            : base(eventAggregator, messageService)
        {
            //GlobalCommands.SaveAllCommand.RegisterCommand(SaveCommand);

            _fmsService = fmsService;

            Title = "Bank Information";
        }

        public BankWrapper Bank
        {
            get { return _bankWrapper; }

            private set
            {
                _bankWrapper = value;
                RaisePropertyChanged();
            }
        }

        public override async Task LoadAsync(int id)
        {
            var bank = id > 0 ? await _fmsService.GetBank(id)
                              : CreateNewBank();
            Id = id;

            InitializeBankWrapper(bank);
        }

        private Bank CreateNewBank()
        {
            return new Bank();
        }

        private void InitializeBankWrapper(Bank bank)
        {
            Bank = new BankWrapper(bank);

            Bank.PropertyChanged += (s, e) =>
            {
                //if (!HasChanges)
                //{
                //    HasChanges = _friendRepository.HasChanges();
                //}

                if (e.PropertyName == nameof(Bank.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

                if (e.PropertyName == nameof(Bank.Name))
                {
                    SetTitle();
                }
            };

            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Bank.ID == 0)
            {
                Bank.Name = "";  // Little trick to trigger the validation
            }

            SetTitle();
        }

        private void SetTitle()
        {
            Title = $"{Bank.Name}";
        }

        protected override void OnDeleteExecute()
        {
            if (MessageDialogService.ShowOkCancelDialog("Do you really want to delete the bank information?", "Delete Bank") == MessageDialogResult.OK)
            {
                DeleteBankInfo();
            }
        }

        protected override bool OnDeleteCanExecute()
        {
            return Bank != null;
        }

        protected override bool OnSaveCanExecute()
        {
            return Bank != null;
        }

        protected override void OnSaveExecute()
        {
            SaveBankInfo();
        }

        private async void SaveBankInfo()
        {
            if (Bank != null)
            {
                var result = await Task.Run(() =>
                {
                    int idValue = 0;
                    try
                    {
                        idValue = _fmsService.SaveBank(Bank.Model);
                    }
                    catch (Exception ex)
                    {
                        MessageDialogService.ShowInfoDialog($"Error Occured: { ex.Message}");
                    }
                    return idValue;
                });

                if (result > 0)
                {
                    Bank.ID = result;
                    this.Id = result;

                    RaiseDetailSavedEvent(Bank.ID, $"{Bank.Name}");
                    MessageDialogService.ShowInfoDialog($"Bank Information Saved For: { Bank.Name}");
                }
            }
        }

        private async void DeleteBankInfo()
        {
            if (Bank != null)
            {
                var result = await Task.Run(() =>
                {
                    int idValue = 0;
                    try
                    {
                        idValue = _fmsService.DeleteBank(Bank.Model);
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
                    MessageDialogService.ShowInfoDialog($"Bank Information Deleted For: { Bank.Name}");
                }
            }
        }

        public bool HasChanges { get { return false; } }
    }
}
