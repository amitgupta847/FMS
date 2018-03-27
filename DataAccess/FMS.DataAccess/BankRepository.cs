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
    public class BankRepository : GenericRepository<Bank, FMSContext>, IBankRepository
    {
        public BankRepository(FMSContext dbContext) : base(dbContext)
        {

        }

        //public override Task<Bank> GetByIdAsync(int bankId)
        //{
        //    var task= _dbcontext.Banks.SingleAsync(bank => bank.ID == bankId);
        //    return task;


        //    //Context.Banks.Include(f => f.PhoneNumbers).SingleAsync(f => f.Id == friendId);
        //    //ctx.Friends.AsNoTracking().SingleAsync(f => f.Id == friendId);
        //}

        //public override Task<List<Bank>> GetAllAsync()
        //{

        //    return _dbcontext.Banks.ToListAsync();

        //    //var friends = await ctx.Friends.AsNoTracking().ToListAsync();
        //    //await Task.Delay(5000);    //simulating a delay of 5 sec to demonstarte that UI is responsive while data is being fetched.
        //    //return friends;
        //    // }

        //    //    //TODO: later data from the real data base
        //    //  
        //    //    yield return new Friend { FirstName = "Andreas", LastName = "Boehler" };
        //    //    yield return new Friend { FirstName = "Julia", LastName = "Huber" };
        //    //    yield return new Friend { FirstName = "Christ", LastName = "Egin" };
        //    //
        //}


        //we can use this method also to enable and disable the save button or 
        // to disable navigation if there are any changes.
        //public bool HasChanges()
        //{
        //    return _context.ChangeTracker.HasChanges();
        //}

        //public void Add(Friend friend)
        //{
        //    _context.Friends.Add(friend);

        //}

        //public void Remove(Friend model)
        //{
        //    _context.Friends.Remove(model);
        //}

        //public void RemovePhoneNumber(FriendPhoneNumber model)
        //{
        //    Context.FriendPhoneNumbers.Remove(model);
        //}


        //public async Task<bool> HasMeetingsAsync(int friendId)
        //{
        //    return await Context.Meetings.AsNoTracking()
        //      .Include(m => m.Friends)
        //      .AnyAsync(m => m.Friends.Any(f => f.Id == friendId));
        //}


        //public Task<int> SaveAsync(Bank bank)
        //{
        //    _dbcontext.Banks.Attach(bank);
        //    ctx.Entry(bank).State = EntityState.Modified;
        //    return ctx.SaveChangesAsync();

        //}

        //public async Task<int> DeleteAsync(Bank bank)
        //{
        //    using (var ctx = _dbcontext())
        //    {
        //        ctx.Banks.Attach(bank);
        //        ctx.Entry(bank).State = EntityState.Deleted;

        //        return await ctx.SaveChangesAsync();
        //    }
        //}

    }
}
