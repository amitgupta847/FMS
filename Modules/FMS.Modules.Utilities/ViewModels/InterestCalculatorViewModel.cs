using FMS.Business;
using FMS.Infrastructure;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.Utilities
{
    public class InterestCalculatorViewModel : ViewModelBase, IInterestViewModel
    {
        private string _title;

        public DelegateCommand<object> CalculateInterest { get; set; }

        public InterestCalculatorViewModel(IEventAggregator eventAggregator)
        {
            Title = "Interest Calculation";

            CalculateInterest = new DelegateCommand<object>(Calculate, CanCalculate);

            Initialize();
        }

        private void Initialize()
        {
            InitialDeposit = 100;
            InterestRate = 6.5;
            TermMonths = 12;
            CompoundingFrequency = CompoundingFrequency.Quarterly;
            FederalTaxRate = 20;
            StateTaxRate = 10;
        }

        private bool CanCalculate(object arg)
        {
            return true;
        }

        private void Calculate(object obj)
        {
            if (obj is CompoundingFrequency)
            {
                CompoundingFrequency = (CompoundingFrequency)obj;
            }
        }

        private void CalculateInterestValues(CompoundingFrequency compFreq)
        {
            int freq = GetCompoundingFrequency(compFreq);

            var result = CalculateFinalAmount(freq);

            MaturityValue = (decimal)result;

            var compoundedAmount = MaturityValue - InitialDeposit;

            Taxes = Math.Round((double)compoundedAmount * ((FederalTaxRate + StateTaxRate) / 100), 2); ;

            ValueAfterTaxes = result - Taxes;
        }

        //formule    = P ( 1+ r/n) raise to (n * t)
        private double CalculateFinalAmount(int compFreq)
        {
            var r_by_n = (InterestRate / 100) / compFreq;

            var powTerm = compFreq * (TermMonths / 12);

            var finalValue = (double)InitialDeposit * Math.Pow(1 + r_by_n, powTerm);

            return Math.Round(finalValue, 2);
        }


        private int GetCompoundingFrequency(CompoundingFrequency freq)
        {
            switch (freq)
            {
                case CompoundingFrequency.Daily:
                    return 365;
                case CompoundingFrequency.Monthly:
                    return 12;
                case CompoundingFrequency.Quarterly:
                    return 4;
                case CompoundingFrequency.Yearly:
                    return 1;
                default:
                    return 4;
            }
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

        private decimal _initialDeposit;

        public decimal InitialDeposit
        {
            get { return _initialDeposit; }

            set
            {
                _initialDeposit = value;
                RaisePropertyChanged();
                CalculateInterestValues(CompoundingFrequency);
            }
        }

        private double _interestRate;
        public double InterestRate
        {
            get { return _interestRate; }

            set
            {
                _interestRate = value;
                RaisePropertyChanged();
                CalculateInterestValues(CompoundingFrequency);
            }
        }

        private int _termMonths;
        public int TermMonths
        {
            get { return _termMonths; }

            set
            {
                _termMonths = value;
                RaisePropertyChanged();
                CalculateInterestValues(CompoundingFrequency);
            }
        }

        private double _federalTaxRate;
        public double FederalTaxRate
        {
            get { return _federalTaxRate; }

            set
            {
                _federalTaxRate = value;
                RaisePropertyChanged();
                CalculateInterestValues(CompoundingFrequency);
            }
        }

        private double _stateTaxRate;
        public double StateTaxRate
        {
            get { return _stateTaxRate; }

            set
            {
                _stateTaxRate = value;
                RaisePropertyChanged();
                CalculateInterestValues(CompoundingFrequency);
            }
        }

        private CompoundingFrequency _compoundingFrequency;
        public CompoundingFrequency CompoundingFrequency
        {
            get { return _compoundingFrequency; }

            set
            {
                _compoundingFrequency = value;
                RaisePropertyChanged();
                CalculateInterestValues(CompoundingFrequency);
            }
        }

        private decimal _maturityValue;
        public decimal MaturityValue
        {
            get { return _maturityValue; }

            set
            {
                _maturityValue = value;
                RaisePropertyChanged();
            }
        }

        private double _taxes;
        public double Taxes
        {
            get { return _taxes; }

            set
            {
                _taxes = value;
                RaisePropertyChanged();
            }
        }

        private double _valueAfterTaxes;
        public double ValueAfterTaxes
        {
            get { return _valueAfterTaxes; }

            set
            {
                _valueAfterTaxes = value;
                RaisePropertyChanged();
            }
        }

    }

 
}
