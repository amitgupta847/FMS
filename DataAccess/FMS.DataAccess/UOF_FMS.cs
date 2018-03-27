using FMS.Business;
using FMS.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.DataAccess
{
    public class UOF_FMS : IUnitOfWorkFMS
    {
        public FMSContext _dbContext;

        public UOF_FMS(FMSContext context)
        {
            _dbContext = context;
            _dbContext.Database.Log = LogText;

            Banks = new BankRepository(_dbContext);
            Accounts = new AccountRepository(_dbContext);
            Deposits = new DepositRepository(_dbContext);
        }

        public IBankRepository Banks { get; private set; }

        public IAccountRepository Accounts { get; private set; }

        public IDepositRepository Deposits { get; private set; }

        public int CompleteAsync()
        {
            return _dbContext.SaveChanges();//.SaveChangesAsync();
        }

        void LogText(string msg)
        {
            Debug.WriteLine(msg);
        }


        public void Dispose()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }

        public Task<List<YearLine>> GetAllYears()
        {
            return _dbContext.Years.AsNoTracking().ToListAsync();
        }

        

    }
}
