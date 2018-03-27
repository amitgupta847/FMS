using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.ServiceHosting.WCFSpecific
{
    public class ServiceDetails
    {
        public string AssembleName { get; set; }

        public string ClassName { get; set; }

        public string Name { get; set; }

        public string InterfaceAssemblyName { get; set; }

        public string InterfaceName { get; set; }

        //public IService Service { get; set; }

        public bool IsSingleton { get; set; }

        public bool IsUnityConfigured { get; set; }
    }
}
