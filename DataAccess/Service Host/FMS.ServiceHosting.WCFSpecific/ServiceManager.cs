using System;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ServiceModel.Description;


using Microsoft.Practices.Unity;
using FMS.ServiceHosting.WCFSpecific;
using Unity;
using FMS.ServiceHosting.Unity;

namespace FMS.ServiceHosting.WCFSpecific
{
    public class ServiceManager : IDisposable//, IService
    {
        public static ServiceManager Instance = new ServiceManager();

        private static object theSync = new object();

        private Dictionary<string, ServiceDetails> theServices = new Dictionary<string, ServiceDetails>();

        private ServiceFactory theServiceFactory;
        private IUnityContainer theUnityContainer;

        private ServiceManager()
        {
            theServiceFactory = WCFServiceFactory.Instance;  //TODO : You can load the Factory instance using configuration file and reflection to decouple it from here.

            //unity configuration
            theUnityContainer = new UnityContainer();

            //Registration of modules
            var v = new UnityAsContainer(theUnityContainer);
            
            WCFServiceFactory.Instance.Container = theUnityContainer;
        }

        public void Load()
        {
            foreach (ServiceDetails serviceDetails in Configurations.Configurations.GetServicesCofiguration())
            {
                ServiceDetails serviceInstance = theServiceFactory.LoadService(serviceDetails);

                theServices[serviceDetails.Name] = serviceInstance;
            }
        }

        //public void Start()
        //{
        //    foreach (KeyValuePair<string, ServiceDetails> serviceDetailsPair in theServices)
        //    {
        //        if (serviceDetailsPair.Value.IsSingleton)
        //        {
        //            serviceDetailsPair.Value.Service.Start();
        //        }
        //    }
        //}

        //public void Stop()
        //{
        //    foreach (KeyValuePair<string, ServiceDetails> serviceDetailsPair in theServices)
        //    {
        //        if (serviceDetailsPair.Value.IsSingleton)
        //        {
        //            serviceDetailsPair.Value.Service.Stop();
        //        }
        //    }
        //}

        //public void Initialize(IService parent)
        //{
        //    foreach (KeyValuePair<string, ServiceDetails> serviceDetailsPair in theServices)
        //    {
        //        if (serviceDetailsPair.Value.IsSingleton)
        //        {
        //            serviceDetailsPair.Value.Service.Initialize(this);
        //        }
        //    }
        //}

        //public void UnInitialize()
        //{
        //    foreach (KeyValuePair<string, ServiceDetails> serviceDetailsPair in theServices)
        //    {
        //        if (serviceDetailsPair.Value.IsSingleton)
        //        {
        //            serviceDetailsPair.Value.Service.UnInitialize();
        //        }
        //    }
        //}

        //public IService GetService(string serviceName)
        //{
        //    ServiceDetails serviceDetails;

        //    if (theServices.TryGetValue(serviceName, out serviceDetails) == false)
        //    {
        //        throw new ApplicationException("Service not found!!");
        //    }

        //    return serviceDetails.Service;
        //}

        public void Dispose()
        {
            //Stop();
            //UnInitialize();

            foreach (KeyValuePair<string, ServiceDetails> serviceDetailsPair in theServices)
            {
                theServiceFactory.DisposeService(serviceDetailsPair.Value);
            }

            if (theUnityContainer != null)
            {
                theUnityContainer.Dispose();
                theUnityContainer = null;
            }
        }

    }
}
