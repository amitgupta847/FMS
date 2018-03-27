﻿using FMS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.BasicInformation
{
    public interface IMainAccountsViewViewModel: IViewModel
    {
        Task LoadAsync();

        void ShowDetails();
    }
}
