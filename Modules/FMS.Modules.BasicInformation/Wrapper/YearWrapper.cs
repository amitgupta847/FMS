using FMS.Business;
using FMS.Infrastructure;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FMS.Modules.BasicInformation
{
    public class YearWrapper : ModelWrapper<YearLine>
    {
        public YearWrapper(YearLine model) : base(model)
        {
           
        }

        
        public event Action<int> OnChecked;

        private void CheckMePlease()
        {
            OnChecked?.Invoke(ID);
        }

        public ICommand CheckMe;

        public int ID
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int Year
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int NumberOfDeposits
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        private bool _isChecked { get; set; }
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                _isChecked = value;
                RaisePropertyChanged();
                CheckMePlease();
            }
        }



        //protected override IEnumerable<string> ValidateProperty(string propertyName)
        //{
        //    switch (propertyName)
        //    {
        //        case nameof(Name):
        //            if (string.Equals(Name, "Amit", StringComparison.OrdinalIgnoreCase))
        //            {
        //                yield return "Amit is not a valid name";
        //            }
        //            break;
        //    }
        //}
    }
}
