using FMS.Business;

using FMS.Infrastructure;

using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.ServiceConnectivity
{
 
    public class ServiceConnectivityModule : ModuleBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public ServiceConnectivityModule(IUnityContainer container, IRegionManager regionManager) :
            base(container, regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public override void Initialize()
        {
            RegisterTypes();
            //var view = Container.Resolve<BanksView>();
            //RegionManager.Regions[RegionNames.ContentRegion].Add(view);

           // RegionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(NavigationButton));
        }

        protected override void RegisterTypes()
        {
            //Container.RegisterType<IBankRepository, BankRepository>();
            //Container.RegisterType<IAccountRepository, AccountRepository>();
            Container.RegisterType<IFMSService, FMSServiceProxy>();

            //Container.RegisterType<IFMSService, FMSService>(new ContainerControlledLifetimeManager());
            
            
        }
    }
}
