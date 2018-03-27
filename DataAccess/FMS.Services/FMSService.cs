using FMS.Business;
using FMS.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FMS.Services
{
    public class FMSService : IFMSService
    {
        private readonly Func<IUnitOfWorkFMS> _unitOfWorkFMS;

        public FMSService(Func<IUnitOfWorkFMS> unitFMS)
        {
            this._unitOfWorkFMS = unitFMS;
        }

        public bool DeleteAccount(Account account)
        {
            using (var uowFMS = _unitOfWorkFMS())
            {
                uowFMS.Accounts.Update(account, EntityState.Deleted);

                var result = uowFMS.CompleteAsync();
                return result > 0;
            }
        }

        public int DeleteBank(Bank bank)
        {
            using (var uowFMS = _unitOfWorkFMS())
            {
                uowFMS.Banks.Update(bank, EntityState.Deleted);

                var result = uowFMS.CompleteAsync();
                return result;
            }
        }

        public int DeleteDeposit(Deposit deposit)
        {
            using (var uowFMS = _unitOfWorkFMS())
            {
                uowFMS.Deposits.Update(deposit, EntityState.Deleted);

                var result = uowFMS.CompleteAsync();
                return result;
            }
        }

        public async Task<Account> GetAccount(int id)
        {
            using (var uowFMS = _unitOfWorkFMS())
            {
                var result = await uowFMS.Accounts.GetByIdAsync(id);
                return result;
            }
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            //Thread.Sleep(5000);
            using (var uowFMS = _unitOfWorkFMS())
            {
                var result = await uowFMS.Accounts.GetAllAsync();
                return result;
            }
        }

        public async Task<List<Bank>> GetAllBanks()
        {
            // Thread.Sleep(5000);
            using (var uowFMS = _unitOfWorkFMS())
            {
                var result = await uowFMS.Banks.GetAllAsync();
                return result;
            }
        }

        public async Task<List<Deposit>> GetAllDeposits()
        {
            using (var uowFMS = _unitOfWorkFMS())
            {
                var result = await uowFMS.Deposits.GetAllAsync();
                return result;
            }
        }

        public async Task<List<YearLine>> GetAllYears()
        {
            using (var uowFMS = _unitOfWorkFMS())
            {
                var result = await uowFMS.GetAllYears();
                return result;
            }
        }


        public async Task<Bank> GetBank(int id)
        {
            using (var uowFMS = _unitOfWorkFMS())
            {
                var result = await uowFMS.Banks.GetByIdAsync(id);
                return result;
            }
        }

        public async Task<Deposit> GetDeposit(int id)
        {
            using (var uowFMS = _unitOfWorkFMS())
            {
                var result = await uowFMS.Deposits.GetByIdAsync(id);
                return result;
            }
        }

        public async Task<List<Deposit>> GetYearDeposits(int yearID)
        {
            using (var uowFMS = _unitOfWorkFMS())
            {
                var result = await uowFMS.Deposits.GetYearDeposits(yearID);
                return result;
            }
        }

        public int SaveAccount(Account account)
        {
            var result = SaveAccountInfo(account);
            return result;
        }

        public int SaveBank(Bank bank)
        {
            int result = SaveBankInfo(bank);
            return result;
        }

        private int SaveAccountInfo(Account account)
        {
            using (var uowFMS = _unitOfWorkFMS())
            {
                if (account.ID == 0)
                {
                    uowFMS.Accounts.Add(account);
                }
                else
                {
                    uowFMS.Accounts.Update(account, EntityState.Modified);
                }

                int i = uowFMS.CompleteAsync();
                return account.ID;
            }
        }

        private int SaveBankInfo(Bank bank)
        {
            // Thread.Sleep(5000);
            using (var uowFMS = _unitOfWorkFMS())
            {
                if (bank.ID == 0)
                {
                    uowFMS.Banks.Add(bank);
                }
                else
                {
                    uowFMS.Banks.Update(bank, EntityState.Modified);

                }

                int i = uowFMS.CompleteAsync();
                return bank.ID;
            }
        }

        public int SaveDeposit(Deposit deposit)
        {
            int result = SaveDepositInfo(deposit);
            return result;
        }

        private int SaveDepositInfo(Deposit deposit)
        {
            using (var uowFMS = _unitOfWorkFMS())
            {
                if (deposit.ID == 0)
                {
                    uowFMS.Deposits.Add(deposit);
                }
                else
                {
                    uowFMS.Deposits.Update(deposit, EntityState.Modified);

                }

                int i = uowFMS.CompleteAsync();
                return deposit.ID;
            }
        }
    }
}
