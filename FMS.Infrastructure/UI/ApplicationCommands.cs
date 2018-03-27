using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Infrastructure
{
    public static class ApplicationCommands
    {
        public static CompositeCommand SaveAllCommand = new CompositeCommand();

        public static CompositeCommand NavigateCommand = new CompositeCommand();
    }
}
