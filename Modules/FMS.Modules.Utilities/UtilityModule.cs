using FMS.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.Utilities
{
  public  class UtilityModule:ModuleBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public UtilityModule(IUnityContainer container, IRegionManager regionManager) :
            base(container, regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public override void Initialize()
        {
            RegisterTypes();

            RegionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(UtilitiesNavigationButton));


            //var view = Container.Resolve<InterestCalculatorView>();
           // RegionManager.Regions[RegionNames.UtilityRegion].Add(view);

            //var view2 = Container.Resolve<PersonView>();
            //RegionManager.Regions[RegionNames.ContentRegion].Add(view2);

            //var vm = _container.Resolve<IPersonViewModel>();
            //_regionManager.Regions[RegionNames.ContentRegion].Add(vm.View);

            //IRegion region = _regionManager.Regions[RegionNames.ContentRegion];

            //var vm = _container.Resolve<IPersonViewModel>();
            //vm.CreatePerson("Bob", "Smith");

            //region.Add(vm.View);
            //region.Activate(vm.View);

            //var vm2 = _container.Resolve<IPersonViewModel>();
            //vm2.CreatePerson("Karl", "Sums");
            //region.Add(vm2.View);

            //var vm3 = _container.Resolve<IPersonViewModel>();
            //vm3.CreatePerson("Jeff", "Lock");
            //region.Add(vm3.View);

        }

        protected override void RegisterTypes()
        {
            Container.RegisterType<IInterestViewModel, InterestCalculatorViewModel>();
            Container.RegisterType<IUtilitiesViewModel, UtilitiesViewModel>();

            Container.RegisterTypeForNavigation<InterestCalculatorView>();


        }
    }
}
