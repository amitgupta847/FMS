using FMS.Infrastructure;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.Infrastructure.Statusbar
{
    public class StatusBarViewModel : ViewModelBase
    {
        private string _message = "Ready";
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        public StatusBarViewModel(IEventAggregator eventAggregator)
        {
        //    eventAggregator.GetEvent<PersonUpdatedEvent>().Subscribe(PersonUpdated);
        }

        //private void PersonUpdated(string payload)
        //{
        //    Message = String.Format("{0} was updated.", payload);
        //}

    }
}
