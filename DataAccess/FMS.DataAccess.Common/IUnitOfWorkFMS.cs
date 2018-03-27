using FMS.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.DataAccess.Common
{
    public interface IUnitOfWorkFMS : IDisposable
    {
        IBankRepository Banks { get; }

        IAccountRepository Accounts { get; }

        IDepositRepository Deposits { get; }

        Task<List<YearLine>> GetAllYears();

        int CompleteAsync();
    }
}
