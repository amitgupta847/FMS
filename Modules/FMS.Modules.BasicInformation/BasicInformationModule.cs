using FMS.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.BasicInformation
{
    public class BasicInformationModule : ModuleBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public BasicInformationModule(IUnityContainer container, IRegionManager regionManager) :
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

            RegionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(NavigationButton));
        }

        protected override void RegisterTypes()
        {
            Container.RegisterType<INavigationViewModel, NavigationViewModel>();
            Container.RegisterType<IMessageDialogService, MessageDialogService>();

            Container.RegisterType<IBankDetailViewModel, BankDetailViewModel>();
            Container.RegisterType<IDepositDetailViewModel, DepositsDetailViewModel>();
            Container.RegisterType<IAccountDetailViewModel, AccountDetailViewModel>();

            
            Container.RegisterType<IMainBankViewModel, MainBanksViewViewModel>();
            Container.RegisterType<IMainAccountsViewViewModel, MainAccountsViewViewModel>();
            Container.RegisterType<IMainDepositsViewViewModel, MainDepositsViewViewModel>();

            Container.RegisterTypeForNavigation<MainBanksView>();
            Container.RegisterTypeForNavigation<MainAccountsView>();
            Container.RegisterTypeForNavigation<MainDepositsView>();
            

            //Container.RegisterType<>()
        }
    }
}
