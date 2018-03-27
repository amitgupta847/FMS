using FMS.Infrastructure;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.Utilities
{

    public class UtilitiesViewModel : ViewModelBase, IUtilitiesViewModel
    {

        private string _title;


        public UtilitiesViewModel(IEventAggregator eventAggregator)
        {
            //Person = Person.CreatePerson(DateTime.Now.Millisecond % 2);
            //SaveCommand = new DelegateCommand<string>(Save, CanSave);

            //GlobalCommands.SaveAllCommand.RegisterCommand(SaveCommand);

            //_eventAggregator = eventAggregator;

            Title = "Utilities";
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
    }
}
