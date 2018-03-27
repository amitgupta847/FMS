using FMS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.ServiceConnectivity
{
    public class FMSServiceProxy : ClientBase<IFMSService>, IFMSService
    {
        //public FMSServiceProxy():base()
        //{ }

        public bool DeleteAccount(Account account)
        {
            return Channel.DeleteAccount(account);
        }

        public bool DeleteBank(int bankId)
        {
            throw new NotImplementedException();
        }

        public int DeleteBank(Bank bank)
        {
            return Channel.DeleteBank(bank);
        }

        public int DeleteDeposit(Deposit deposit)
        {
            return Channel.DeleteDeposit(deposit);
        }

        public Task<Account> GetAccount(int id)
        {
            return Channel.GetAccount(id);
        }

        public Task<List<Account>> GetAllAccounts()
        {
            return Channel.GetAllAccounts();
        }

        public Task<List<Bank>> GetAllBanks()
        {
            return Channel.GetAllBanks();
        }

        public Task<List<Deposit>> GetAllDeposits()
        {
            return Channel.GetAllDeposits();
        }

        public Task<List<YearLine>> GetAllYears()
        {
            return Channel.GetAllYears();
        }

        public Task<Bank> GetBank(int id)
        {
            return Channel.GetBank(id);
        }

        public Task<Deposit> GetDeposit(int id)
        {
            return Channel.GetDeposit(id);
        }

        public Task<List<Deposit>> GetYearDeposits(int yearID)
        {
            return Channel.GetYearDeposits(yearID);
        }

        public int SaveAccount(Account account)
        {
            return Channel.SaveAccount(account);
        }

        public int SaveBank(Bank bank)
        {
            return Channel.SaveBank(bank);
        }

        public int SaveDeposit(Deposit deposit)
        {
            return Channel.SaveDeposit(deposit);
        }


    }
}
