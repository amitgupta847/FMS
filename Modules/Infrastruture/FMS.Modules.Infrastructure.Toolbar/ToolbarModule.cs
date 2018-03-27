using FMS.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.Infrastructure.Toolbar
{
    public class ToolbarModule : ModuleBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public ToolbarModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public override void Initialize()
        {
            //amit not including right now

            //var view = Container.Resolve<ToolbarView>();
           // RegionManager.Regions[RegionNames.ToolbarRegion].Add(view);
        }



        protected override void RegisterTypes()
        {
            //throw new NotImplementedException();
        }
    }
}
