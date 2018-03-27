using FMS.Business;
using FMS.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.DataAccess
{
    public class DepositRepository : GenericRepository<Deposit, FMSContext>, IDepositRepository
    {
        public DepositRepository(FMSContext dbContext) : base(dbContext) { }

        public override Task<Deposit> GetByIdAsync(int dpId)
        {
            return Context.Deposits.Include(a => a.PrimaryLinkedAccount)
                                     .Include(y => y.Year)
                                     .SingleAsync(dp => dp.ID == dpId);

            //Context.Banks.Include(f => f.PhoneNumbers).SingleAsync(f => f.Id == friendId);
            //ctx.Friends.AsNoTracking().SingleAsync(f => f.Id == friendId);
        }

        public override Task<List<Deposit>> GetAllAsync()
        {
            return Context.Deposits.Include(a => a.PrimaryLinkedAccount.Bank)
                                     .Include(y => y.Year)
                                     .ToListAsync();
        }


        public virtual Task<List<Deposit>> GetYearDeposits(int yearID)
        {
            return Context.Deposits.Include(a => a.PrimaryLinkedAccount.Bank)
                                     .Include(y => y.Year)
                                     .Where(dep => dep.YearID == yearID)
                                     .ToListAsync();
        //Context.Set<TEntity>().AsNoTracking().ToListAsync();
    }
    }
}
