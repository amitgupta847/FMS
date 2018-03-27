using FMS.Business;
using FMS.Infrastructure;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;


namespace FMS.Modules.BasicInformation
{
    public class MainAccountsViewViewModel : ViewModelBase, IMainAccountsViewViewModel
    {
        private readonly Func<IAccountDetailViewModel> _accountDetailVMCreator;
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;

        private IAccountDetailViewModel _selectedDetailViewModel;
        private IFMSService _fmsService;
        public DelegateCommand EditPersonCommand { get; private set; }

        public MainAccountsViewViewModel(IEventAggregator eventAggregator,
                                         IMessageDialogService messageDialogService,
                                         Func<IAccountDetailViewModel> accountDetailVMCreator,
                                         IFMSService fmsService)
        {
            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;
            _accountDetailVMCreator = accountDetailVMCreator;
            _fmsService = fmsService;

            DetailViewModels = new ObservableCollection<IAccountDetailViewModel>();
            EditPersonCommand = new DelegateCommand(ShowDetails, CanEditPerson);
            // _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
            // _eventAggregator.GetEvent<AfterDetailClosedEvent>().Subscribe(AfterDetailClosed);
            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
            // OpenSingleDetailViewCommand = new DelegateCommand<Type>(OnOpenSingleDetailViewExecute);

        }

        private bool CanEditPerson()
        {
            return SelectedDetailViewModel != null;
        }


        public ObservableCollection<IAccountDetailViewModel> DetailViewModels { get; }

        public IAccountDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set
            {
                _selectedDetailViewModel = value;
                RaisePropertyChanged();
                ((DelegateCommand)EditPersonCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand CreateNewDetailCommand { get; }

        public ICommand OpenSingleDetailViewCommand { get; }

        public async Task LoadAsync()
        {
            IsBusy = true;
            DetailViewModels.Clear();

            var lookup = await _fmsService.GetAllAccounts();

            foreach (var item in lookup)
            {
                var accountDetailViewModel = _accountDetailVMCreator();

                accountDetailViewModel.LoadAsync(item);

                accountDetailViewModel.OnClose += CloseChildWindow;

                DetailViewModels.Add(accountDetailViewModel);
            }

            IsBusy = false;
        }

        private void CloseChildWindow()
        {
            WindowState = Xceed.Wpf.Toolkit.WindowState.Closed;

            if (SelectedDetailViewModel.Id == 0) // if not saved, then remove from the grid
            {
                RemoveDetailViewModel(0, "AccountDetailViewModel");

                SelectedDetailViewModel = null;
            }
        }

        private int nextNewItemId = 0;
        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs { Id = 0, ViewModelName = viewModelType.Name });
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args) // making friendId nullable to support adding new friend.
        {
            var detailViewModel = DetailViewModels.SingleOrDefault(vm => vm.Id == args.Id
                                                                 && vm.GetType().Name == args.ViewModelName);
            if (detailViewModel == null)
            {
                detailViewModel = _accountDetailVMCreator();

                Account item = new Account();

                detailViewModel.LoadAsync(item);

                detailViewModel.OnClose += CloseChildWindow;

                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;

            ShowDetails();
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
            SelectedDetailViewModel = null;
        }


        private void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            //RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void RemoveDetailViewModel(int id, string vmName)
        {
            var detailViewModel = DetailViewModels.FirstOrDefault(vm => vm.Id == id
                          && vm.GetType().Name == vmName);

            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }

        private void OnOpenSingleDetailViewExecute(Type viewModelType)
        {
            OnOpenDetailView(
           new OpenDetailViewEventArgs
           {
               Id = -1,
               ViewModelName = viewModelType.Name
           });
        }

        public void ShowDetails()
        {
            WindowState = Xceed.Wpf.Toolkit.WindowState.Open;
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }

        }

        public WindowState WindowState
        {
            get { return _windosState; }
            set
            {
                _windosState = value;
                RaisePropertyChanged();

            }
        }

        private WindowState _windosState;
    }
}
