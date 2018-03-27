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
    public class AccountRepository : GenericRepository<Account, FMSContext>, IAccountRepository
    {
        public AccountRepository(FMSContext dbContext) : base(dbContext)
        {
        }

        public override Task<Account> GetByIdAsync(int accountId)
        {
            return Context.Accounts.Include(b => b.Bank).SingleAsync(acc => acc.ID == accountId);
         
        }

        public override Task<List<Account>> GetAllAsync()
        {
            return Context.Accounts.Include(b => b.Bank).ToListAsync();
        }

        //Context.Banks.Include(f => f.PhoneNumbers).SingleAsync(f => f.Id == friendId);
        //ctx.Friends.AsNoTracking().SingleAsync(f => f.Id == friendId);


        //var friends = await ctx.Friends.AsNoTracking().ToListAsync();
        //await Task.Delay(5000);    //simulating a delay of 5 sec to demonstarte that UI is responsive while data is being fetched.
        //return friends;
        // }

        //    //TODO: later data from the real data base
        //    yield return new Friend { FirstName="Thomas",LastName="Huber"};
        //    yield return new Friend { FirstName = "Andreas", LastName = "Boehler" };
        //    yield return new Friend { FirstName = "Julia", LastName = "Huber" };
        //    yield return new Friend { FirstName = "Christ", LastName = "Egin" };


    }
}
