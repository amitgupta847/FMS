using System;
using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;

using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;

using System.Reflection;
using System.Diagnostics;
using Unity;
using FMS.ServiceHosting.Behaviors;
using FMS.ServiceHosting.Behaviors.Behaviors;
using FMS.Business;

namespace FMS.ServiceHosting.WCFSpecific
{
    public class WCFServiceFactory : ServiceFactory
    {
        public static readonly WCFServiceFactory Instance = new WCFServiceFactory();

        public IUnityContainer Container { get; set; }

        private Dictionary<string, ServiceHost> theHoster = new Dictionary<string, ServiceHost>();

        private WCFServiceFactory() { }

        public override ServiceDetails GetServiceInstance(ServiceDetails service)
        {
            //if (service.IsSingleton)
            //{
            //    service.Service = ClassFactory<IService>.GetClassObject(service.AssembleName, service.ClassName);
            //}

            PublishServices(service);

            return service;
        }

        public override void DisposeService(ServiceDetails serviceDetails)
        {
            ServiceHost host;

            if (theHoster.TryGetValue(serviceDetails.Name, out host))
            {
                host.Close();
            }
        }

        private void PublishServices(ServiceDetails sdDetails)
        {
            string address = string.Concat(Services.Address, sdDetails.Name);

            ServiceHost serviceHost = null;

            if (sdDetails.IsSingleton)
            {
                //serviceHost = new ServiceHost(sdDetails.Service, new Uri(address));
            }
            else
            {
                Assembly asmforType = Assembly.Load(sdDetails.AssembleName);
                Type classType = null;
                if (asmforType != null)
                {
                    classType = asmforType.GetType(sdDetails.ClassName);

                }
                if (!sdDetails.IsUnityConfigured)
                {
                    serviceHost = new ServiceHost(classType, new Uri(address));
                }
                else
                {
                    if (Container == null)
                    {
                        throw new InvalidOperationException("Container not configured!!!");
                    }

                    serviceHost = new UnityServiceHost(Container, classType, new Uri(address));
                }
            }

            NetTcpBinding bindingNet = new NetTcpBinding(); //Services.BindingName


            Assembly asm = Assembly.Load(sdDetails.InterfaceAssemblyName);
            Type interfaceType = null;
            if (asm != null)
            {
                interfaceType = asm.GetType(sdDetails.InterfaceName);
            }

            serviceHost.AddServiceEndpoint(interfaceType, bindingNet, "");

            foreach (ServiceEndpoint endPoint in serviceHost.Description.Endpoints)
            {
                endPoint.EndpointBehaviors.Add(new EndPointBehaviorMessageInspector());
            }

            ServiceMetadataBehavior behav = new ServiceMetadataBehavior();
            // behav.HttpGetEnabled = true;
            serviceHost.Description.Behaviors.Add(behav);

            //Adding centralized error processor (it converts all non fault exception to fault SOAP message)
            serviceHost.Description.Behaviors.Add(new ServiceBehaviorErrorHandler());

            // serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            serviceHost.Open();
            serviceHost.Faulted += OnFaulted;

            theHoster[sdDetails.Name] = serviceHost;
        }


        //TODO: not fully implemented
        private void OnFaulted(object sender, EventArgs e)
        {
            Debug.Assert(false);
            //Logger.Error("Service went to faulted state.", sender.ToString());
            ServiceHost host = sender as ServiceHost;
            try
            {

                host.Abort();

                //create new instance and then open
                //host=new Servicehost().......

                host.Open();
            }
            catch (Exception ex)
            {
                //host.Open();
            }
        }
    }
}
