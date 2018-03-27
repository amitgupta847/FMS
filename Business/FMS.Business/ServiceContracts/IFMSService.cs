using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace FMS.Business
{
    [ServiceContract]
    public interface IFMSService
    {
        //Banks

        [OperationContract]
        Task<Bank> GetBank(int id);

        [OperationContract]
        Task<List<Bank>> GetAllBanks();

        [OperationContract]
        int SaveBank(Bank bank);

        [OperationContract]
        int DeleteBank(Bank bank);

        //Accounts

        [OperationContract]
        Task<Account> GetAccount(int id);

        [OperationContract]
        Task<List<Account>> GetAllAccounts();

        [OperationContract]
        int SaveAccount(Account bank);

        [OperationContract]
        bool DeleteAccount(Account account);

        //Years
        [OperationContract]
        Task<List<YearLine>> GetAllYears();


        //Deposits

        [OperationContract]
        Task<Deposit> GetDeposit(int id);

        [OperationContract]
        Task<List<Deposit>> GetAllDeposits();

        [OperationContract]
        Task<List<Deposit>> GetYearDeposits(int yearID);

        [OperationContract]
        int SaveDeposit(Deposit deposit);

        [OperationContract]
        int DeleteDeposit(Deposit deposit);





    }
}
