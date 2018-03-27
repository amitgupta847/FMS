using FMS.ServiceHosting.WCFSpecific;
using System;


using System.Collections.Generic;

namespace FMS.ServiceHosting.Configurations
{
    public class Configurations
    {
        public static List<ServiceDetails> GetServicesCofiguration()
        {
            List<ServiceDetails> serviceDetails = new List<ServiceDetails>();

            serviceDetails.Add(new ServiceDetails()
            {
                Name = "FMSService",
                AssembleName = "FMS.Services",
                ClassName = "FMS.Services.FMSService",
                InterfaceAssemblyName = "FMS.Business",
                InterfaceName = "FMS.Business.IFMSService",
                IsSingleton = false,
                IsUnityConfigured=true             
            });

            //serviceDetails.Add(new ServiceDetails()
            //{
            //    Name = Services.AcademicManager,
            //    AssembleName = "SMS.BusinessService.Academics",
            //    ClassName = "SMS.BusinessService.Academics.AcademicManager",
            //    InterfaceAssemblyName = "SMS.BusinessService.Contracts",
            //    InterfaceName = "SMS.BusinessService.Contracts.Academics.IAcademicManager",
            //    IsSingleton = false
            //});

            //serviceDetails.Add(new ServiceDetails()
            //{
            //    Name = Services.NotificationManager,
            //    AssembleName = "SMS.BusinessService.NotificationServer",
            //    ClassName = "SMS.BusinessService.NotificationServer.NotificationService",
            //    InterfaceAssemblyName = "SMS.BusinessService.Contracts",
            //    InterfaceName = "SMS.BusinessService.Contracts.Notification.IServerStock",
            //    IsSingleton = true
            //});

            //serviceDetails.Add(new ServiceDetails()
            //{
            //    Name = Services.SMS_NotificationManager,
            //    AssembleName = "SMS.BusinessService.NotificationServer",
            //    ClassName = "SMS.BusinessService.NotificationServer.NotificationService",
            //    InterfaceAssemblyName = "SMS.BusinessService.Contracts",
            //    InterfaceName = "SMS.BusinessService.Contracts.Notification.IPubilsher",
            //    IsSingleton = true
            //});

            //serviceDetails.Add(new ServiceDetails()
            //{
            //    Name = Services.NotificationBroker,
            //    AssembleName = "SMS.BusinessService.NotificationServer",
            //    ClassName = "SMS.BusinessService.NotificationServer.NotificationService",
            //    InterfaceAssemblyName = "SMS.BusinessService.Contracts",
            //    InterfaceName = "SMS.BusinessService.Contracts.Notification.IPubilsherBroker",
            //    IsSingleton = true
            //});

            return serviceDetails;

        }

    }
}
