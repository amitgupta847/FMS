using FMS.Business;
using FMS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.BasicInformation
{
    public class AccountWrapper : ModelWrapper<Account>
    {
        public AccountWrapper(Account model) : base(model)
        {
      
        }

        public int ID
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public int BankID
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public Bank Bank
        {
            get { return GetValue<Bank>(); }
            set { SetValue(value); }
        }

        public string AccountNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string OwnerName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string User_Id
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Pwd_1
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Pwd_2
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public DateTime Date_Acc_Open
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        public bool IsAccActive
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool IsJointlyOwned
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public string Comment
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string AccountName
        {
            get { return GetValue<string>(); }
        }


        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(AccountNumber):
                    if (string.Equals(AccountNumber, "Amit", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Amit is not a valid account number";
                    }
                    break;
            }
        }
    }
}
