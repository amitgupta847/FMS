using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FMS.Infrastructure
{
    public class ViewBase : UserControl, IView
    {
        public ViewBase()
        {

        }

        public ViewBase(IViewModel viewModel)
        {
            DataContext = viewModel;
        }

    
    }
}
