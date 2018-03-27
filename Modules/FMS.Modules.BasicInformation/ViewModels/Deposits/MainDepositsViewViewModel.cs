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
    public class MainDepositsViewViewModel : ViewModelBase, IMainDepositsViewViewModel
    {
        private IEventAggregator _eventAggregator;
        private IMessageDialogService _messageDialogService;
        private readonly Func<IDepositDetailViewModel> _depositDetailVMCreator;

        private IDepositDetailViewModel _selectedDetailViewModel;
        private IFMSService _fmsService;
        public DelegateCommand EditPersonCommand { get; private set; }

        public MainDepositsViewViewModel(IEventAggregator eventAggregator,
                                         IMessageDialogService messageDialogService,
                                         Func<IDepositDetailViewModel> depositDetailVMCreator,
                                         IFMSService fmsService)
        {
            _messageDialogService = messageDialogService;
            this._depositDetailVMCreator = depositDetailVMCreator;
            _eventAggregator = eventAggregator;
            _fmsService = fmsService;

            DetailViewModels = new ObservableCollection<IDepositDetailViewModel>();
            Years = new ObservableCollection<YearWrapper>();

            EditPersonCommand = new DelegateCommand(ShowDetails, CanEditPerson);

            // _eventAggregator.GetEvent<OpenDetailViewEvent>().Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
            //_eventAggregator.GetEvent<AfterDetailClosedEvent>().Subscribe(AfterDetailClosed);

            CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

            // OpenSingleDetailViewCommand = new DelegateCommand<Type>(OnOpenSingleDetailViewExecute);

        }

        public void ShowDetails()
        {
            WindowState = Xceed.Wpf.Toolkit.WindowState.Open;
        }

        private bool CanEditPerson()
        {
            return SelectedDetailViewModel != null;
        }

        public ObservableCollection<IDepositDetailViewModel> DetailViewModels { get; }

        public ObservableCollection<YearWrapper> Years { get; }

        public IDepositDetailViewModel SelectedDetailViewModel
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

            if (Years == null || Years.Count == 0)
                await LoadYears();

            LoadYearDeposits(GetSelectedYears());

            IsBusy = false;
        }

        private async Task<List<YearLine>> LoadYears()
        {
            Years.Clear();

            int currentYear = DateTime.Now.Year;

            var years = await _fmsService.GetAllYears();

            if (years != null && years.Count > 0)
            {
                foreach (var year in years)
                {
                    YearWrapper yearToConfigure = new YearWrapper(year);

                    if (year.Year == currentYear)
                    {
                        yearToConfigure.IsChecked = true;
                    }

                    yearToConfigure.OnChecked += YearToConfigure_OnChecked;

                    Years.Add(yearToConfigure);
                }
            }
            return years;
        }

        private void YearToConfigure_OnChecked(int obj)
        {
            List<int> selectedYears = GetSelectedYears();

            if (selectedYears.Count > 0)
                LoadYearDeposits(selectedYears);
            else
                DetailViewModels.Clear();
        }

        private List<int> GetSelectedYears()
        {
            List<int> years = new List<int>();
            foreach (var year in Years)
            {
                if (year.IsChecked)
                    years.Add(year.ID);
            }

            return years;
        }


        //TODO: need to cache the results.
        private async void LoadYearDeposits(List<int> yearIds)
        {
            DetailViewModels.Clear();

            foreach (var yearID in yearIds)
            {
                var deposits = await _fmsService.GetYearDeposits(yearID);

                if (deposits != null && deposits.Count > 0)
                {
                    foreach (var item in deposits)
                    {
                        var depsoitsDetailViewModel = _depositDetailVMCreator();

                        depsoitsDetailViewModel.LoadAsync(item);

                        depsoitsDetailViewModel.OnClose += CloseChildWindow;

                        DetailViewModels.Add(depsoitsDetailViewModel);
                    }
                }
            }
        }

        private void CloseChildWindow()
        {
            WindowState = Xceed.Wpf.Toolkit.WindowState.Closed;

            if (SelectedDetailViewModel.Id == 0) // if not saved, then remove from the grid
            {
                RemoveDetailViewModel(0, "DepositsDetailViewModel");

                SelectedDetailViewModel = null;
            }
        }

        private void OnCreateNewDetailExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs { Id = 0, ViewModelName = viewModelType.Name });
        }

        private void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            var detailViewModel = DetailViewModels.SingleOrDefault(vm => vm.Id == args.Id
                                                                && vm.GetType().Name == args.ViewModelName);
            if (detailViewModel == null)
            {
                detailViewModel = _depositDetailVMCreator();

                Deposit item = new Deposit();

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


        //public void GetPeopleAsync(EventHandler<ServiceResult<IList<Person>>> callback)
        //{
        //    BackgroundWorker bw = new BackgroundWorker();
        //    bw.DoWork += (o, e) =>
        //    {
        //        e.Result = Load();
        //    };
        //    bw.RunWorkerCompleted += (o, e) =>
        //    {
        //        callback.Invoke(this, new ServiceResult<IList<Person>>((IList<Person>)e.Result));
        //    };
        //    bw.RunWorkerAsync();
        //}

        //TODO: amit: put this code to background thread to make UI thread free..
    }
}
