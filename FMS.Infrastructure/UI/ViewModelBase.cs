using Prism;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Infrastructure
{
    public class ViewModelBase : BindableBase, IActiveAware, IViewModel
    {
        public bool IsActive { get; set; }

        public event EventHandler IsActiveChanged;

    }
}
