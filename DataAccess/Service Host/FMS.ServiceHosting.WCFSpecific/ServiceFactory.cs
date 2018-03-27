using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.ServiceHosting.WCFSpecific
{
    public abstract class ServiceFactory
    {
        public ServiceDetails LoadService(ServiceDetails service)
        {
            return GetServiceInstance(service);
        }

        public abstract ServiceDetails GetServiceInstance(ServiceDetails serviceDetails);

        public abstract void DisposeService(ServiceDetails serviceDetails);

    }
}
