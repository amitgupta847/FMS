using FMS.Business;
using FMS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.BasicInformation
{
    public class BankWrapper : ModelWrapper<Bank>
    {
        public BankWrapper(Bank model) : base(model)
        {
        }

        public int ID
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Address
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string IFSC
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Phone
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Comment
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(Name):
                    if (string.Equals(Name, "Amit", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Amit is not a valid name";
                    }
                    break;
            }
        }
    }
}
