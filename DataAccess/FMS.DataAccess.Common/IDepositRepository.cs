using FMS.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.DataAccess.Common
{
    public interface IDepositRepository : IGenericRepository<Deposit>
    {
        Task<List<Deposit>> GetYearDeposits(int yearID);
    }
}
