using FMS.Infrastructure;
using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Modules.Infrastructure.Statusbar
{
    public class StatusBarModule : ModuleBase
    {
        public StatusBarModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {

        }

        public override void Initialize()
        {
            var view = Container.Resolve<StatusBarView>();
            RegionManager.Regions[RegionNames.StatusBarRegion].Add(view);
        }

    

        protected override void RegisterTypes()
        {
       
        }
    }
}
