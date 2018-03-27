using FMS.Business;
using FMS.Infrastructure;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.BasicInformation
{
    public class DepositWrapper : ModelWrapper<Deposit>
    {
        public DepositWrapper(Deposit model) : base(model)
        {

        }

        public int ID
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public string DepositAccNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int YearID
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public YearLine Year
        {
            get { return GetValue<YearLine>(); }
            set { SetValue(value); }
        }

        public Account PrimaryLinkedAccount
        {
            get { return GetValue<Account>(); }
            set { SetValue(value); }
        }

        public int AccountID
        {
            get { return GetValue<int>(); }
            set { SetValue(value);
                SetFinalCalculatedValues();
            }
        }


        public decimal InitialDeposit
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        public decimal CurrentValue
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }

        public decimal MaturityValue
        {
            get { return GetValue<decimal>(); }
            set { SetValue(value); }
        }


        public DateTime DepositDate
        {
            get { return GetValue<DateTime>(); }
            set { SetValue(value); }
        }

        private int _months = 0;
        public int Months
        {
            get { return GetMonths(); }
            set
            {
                _months = value;

                if (value != 0)
                    Days = 0;

                CalculateMaturityDate();
                SetFinalCalculatedValues();
                RaisePropertyChanged();
            }
        }

        private int GetMonths()
        {
            Period period = Period.Between(new LocalDate(DepositDate.Year, DepositDate.Month, DepositDate.Day),
                                           new LocalDate(MaturityDate.Year, MaturityDate.Month, MaturityDate.Day),
                                           PeriodUnits.Months);
            return period.Months;
        }

        private int GetDays()
        {
            Period period = Period.Between(new LocalDate(DepositDate.Year, DepositDate.Month, DepositDate.Day),
                                           new LocalDate(MaturityDate.Year, MaturityDate.Month, MaturityDate.Day),
                                           PeriodUnits.Days);
            return period.Days;

        }

        private int _days = 0;
        public int Days
        {
            get { return GetDays(); }
            set
            {
                _days = value;

                if (value != 0)
                    Months = 0;

                CalculateMaturityDate();
                SetFinalCalculatedValues();
                RaisePropertyChanged();

            }
        }


        private void CalculateMaturityDate()
        {
            int aMnths = _months;
            int adays = _days;

            if (_months != 0) // take either months or days, if months are specified then ignore days.
                adays = 0;

            MaturityDate = DepositDate.AddMonths(aMnths).AddDays(adays);
        }

        private void SetMaturityMonthsDaysDate()
        {
            int years = 0;
            int months = Months;
            int days = Days;

            if (Months > 12)
            {
                months = months % 12;
                years = months / 12;
            }

            DateTime matDate = new DateTime(years, months, days);
            MaturityDate = matDate;

        }

        public DateTime MaturityDate
        {
            get { return GetValue<DateTime>(); }
            set
            {
                SetValue(value);
                SetFinalCalculatedValues();
            }
        }

        public float Interest
        {
            get { return GetValue<float>(); }
            set { SetValue(value);
                SetFinalCalculatedValues();
            }
        }


        public string Comment
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public void SetFinalCalculatedValues()
        {
            MaturityValue= (decimal)Calculations.CalculateFinalAmount(InitialDeposit, Interest, Months);
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(DepositAccNumber):
                    if (string.Equals(DepositAccNumber, "Amit", StringComparison.OrdinalIgnoreCase))
                    {
                        yield return "Amit is not a valid account number";
                    }
                    break;
            }
        }
    }
}
