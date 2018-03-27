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
using System.Windows.Input;

namespace FMS.Modules.BasicInformation
{
    public class MainBanksViewViewModel : ViewModelBase, IMainBankViewModel
    {
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;

        private IFMSService _fmsService;
        private IDetailViewModel _selectedDetailViewModel;
        private readonly Func<IBankDetailViewModel> _bankDetailViewModelCreator;

        public MainBanksViewViewModel(INavigationViewModel navgViewModel,
                                      IEventAggregator eventAggregator,
                                      IMessageDialogService messageService,
                                      Func<IBankDetailViewModel> bankDetailVMCreator,
                                      IFMSService fmsService)
        {
            NavigationViewModel = navgViewModel;
            _eventAggregator = eventAggregator;
            _messageDialogService = messageService;
            _bankDetailViewModelCreator = bankDetailVMCreator;
            _fmsService = fmsService;

            DetailViewModels = new ObservableCollection<IDetailViewModel>();

            //Events
            _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
            _eventAggregator.GetEvent<AfterDetailClosedEvent>().Subscribe(AfterDetailClosed);

            //Commands
            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
            OpenSingleDetailViewCommand = new DelegateCommand<Type>(OnOpenSingleDetailViewExecute);
        }


        public INavigationViewModel NavigationViewModel { get; }

        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        public IDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set
            {
                _selectedDetailViewModel = value;
                RaisePropertyChanged();
            }
        }

        public ICommand CreateNewDetailCommand { get; }

        public ICommand OpenSingleDetailViewCommand { get; }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs { Id = 0, ViewModelName = viewModelType.Name });
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            var detailViewModel = DetailViewModels.SingleOrDefault(vm => vm.Id == args.Id
                                                                && vm.GetType().Name == args.ViewModelName);
            if (detailViewModel == null)
            {
                detailViewModel = _bankDetailViewModelCreator();

                await detailViewModel.LoadAsync(args.Id);

                DetailViewModels.Add(detailViewModel);
            }

            //if (SelectedDetailViewModel != null && SelectedDetailViewModel.HasChanges)
            //{
            //    MessageDialogResult result = _messageDialogService.ShowOkCancelDialog("You have made changes. Do you want to navigate away?", "Confirmation.");
            //    if (result== MessageDialogResult.Cancel)
            //    {
            //        return;
            //    }
            //}

            //switch(args.ViewModelName)
            //{
            //    case nameof(FriendDetailViewModel):
            //        DetailViewModel = _friendDetailViewModelCreator();
            //        break;

            //    case nameof(MeetingDetailViewModel):
            //        DetailViewModel = _meetingDetailViewModelCreator();
            //        break;
            //    default:
            //        throw new Exception($"ViewModel {args.ViewModelName} not mapped");
            //        break;

            //}

            SelectedDetailViewModel = detailViewModel;
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
            SelectedDetailViewModel = null;
        }

        private void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
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
            OnOpenDetailView(new OpenDetailViewEventArgs
            {
                Id = -1,
                ViewModelName = viewModelType.Name
            });
        }
    }
}
