using FMS.Business;
using FMS.Infrastructure;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.BasicInformation
{
   
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        //private NavigationItemViewModel _selectedFriend;

        IFMSService _fmsService;
        private IEventAggregator _eventAggregator;


        public NavigationViewModel(IFMSService friendLookUpDataService,
                                   IEventAggregator eventAggregator)
        {
            _fmsService = friendLookUpDataService;

            ItemsToNavigate = new ObservableCollection<NavigationItemViewModel>();
            //Meetings = new ObservableCollection<NavigationItemViewModel>();

            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
        }

        public async Task LoadAsync()
        {
            ItemsToNavigate.Clear();

            var lookup = await _fmsService.GetAllBanks();

            foreach (var item in lookup)
            {
                ItemsToNavigate.Add(new NavigationItemViewModel(item.ID,item.Name,  nameof(BankDetailViewModel),
                                                        _eventAggregator));
            }

            //Meetings.Clear();

            //lookup = await _meetingLookUpService.GetMeetingLookupAsync();

            //foreach (var meeting in lookup)
            //{
            //    Meetings.Add(new NavigationItemViewModel(meeting.Id, meeting.DisplayMember,
            //                                            nameof(MeetingDetailViewModel),
            //                                            _eventAggregator));
            //}
        }
        public ObservableCollection<NavigationItemViewModel> ItemsToNavigate { get; }

        public ObservableCollection<NavigationItemViewModel> Meetings { get; }

        private void AfterDetailSaved(AfterDetailSavedEventArgs arg)
        {
            switch (arg.ViewModelName)
            {
                case nameof(BankDetailViewModel):

                    AfterDetailSaved(ItemsToNavigate, arg);
                    break;

                //case nameof(MeetingDetailViewModel):
                //    AfterDetailSaved(Meetings, arg);
                //    break;
            }
        }

        private void AfterDetailSaved(ObservableCollection<NavigationItemViewModel> items, AfterDetailSavedEventArgs arg)
        {
            var lookUpItem = items.SingleOrDefault(l => l.ID == arg.Id);

            if (lookUpItem == null)
            {
                lookUpItem = new NavigationItemViewModel(arg.Id, arg.DisplayMember,
                                                         arg.ViewModelName,
                                                         _eventAggregator);
                items.Add(lookUpItem);
            }
            else
            {
                lookUpItem.DisplayMember = arg.DisplayMember;
            }
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(BankDetailViewModel):
                    AfterDetailDeleted(ItemsToNavigate, args);
                    break;

                //case nameof(MeetingDetailViewModel):
                //    AfterDetailDeleted(Meetings, args);
                //    break;
            }
        }

        private void AfterDetailDeleted(ObservableCollection<NavigationItemViewModel> items, AfterDetailDeletedEventArgs args)
        {
            var itemToDelete = items.SingleOrDefault(item => item.ID == args.Id);
            if (itemToDelete != null)
            {
                items.Remove(itemToDelete);
            }
        }
        //public NavigationItemViewModel SelectedFriend
        //{
        //    get { return _selectedFriend; }
        //    set
        //    {
        //        _selectedFriend = value;
        //        OnPropertyChanged();

        //        if(_selectedFriend!=null)
        //        {
        //            _eventAggregator.GetEvent<OpenFriendDetailViewEvent>().Publish(_selectedFriend.ID);
        //        }
        //    }
        //}

    }
}
