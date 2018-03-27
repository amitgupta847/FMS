using FMS.Business;
using FMS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.BasicInformation
{
    public interface IDepositDetailViewModel : IDetailViewModel, IViewModel
    {

        void LoadAsync(Deposit deposit);


    }
}
